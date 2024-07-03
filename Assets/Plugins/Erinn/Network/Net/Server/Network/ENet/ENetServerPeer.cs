//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ENet;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
using System.Threading;
#endif

#pragma warning disable CS8603
#pragma warning disable CS8604

// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public sealed class ENetServerPeer : NetworkServerPeer
    {
        /// <summary>
        ///     Server EndPoint
        /// </summary>
        private readonly Host _serverPeer = new();

        /// <summary>
        ///     Message buffer
        /// </summary>
        private readonly byte[] _messageBuffer = new byte[1024];

        /// <summary>
        ///     Pending Connections
        /// </summary>
        private readonly ConcurrentHashSet<uint> _pendingConnections = new();

        /// <summary>
        ///     Removed Connections
        /// </summary>
        private readonly ConcurrentHashSet<uint> _removedConnections = new();

        /// <summary>
        ///     Removed Peers
        /// </summary>
        private readonly ConcurrentQueue<Peer> _removedPeers = new();

        /// <summary>
        ///     Client Connections
        /// </summary>
        private readonly ConcurrentDictionary<uint, Peer> _peers = new();

        /// <summary>
        ///     Event queue
        /// </summary>
        private readonly ConcurrentQueue<ENetServerEvent> _onryoEvents = new();

        /// <summary>
        ///     The data packets to be sent
        /// </summary>
        private readonly ConcurrentQueue<ENetPacket> _outgoing = new();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public ENetServerPeer(NetworkServerMessageChannel messageChannel) : base(messageChannel)
        {
        }

        /// <summary>
        ///     Protocol type
        /// </summary>
        public override NetworkProtocolType ProtocolType => NetworkProtocolType.Udp;

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public override bool Start(ushort port, uint maxClients)
        {
            if (!StartListening())
                return false;
            Library.Initialize();
            var address = new Address { Port = port };
            try
            {
                _serverPeer.Create(address, (int)maxClients, 0, 0U, 0U, 0);
            }
            catch
            {
                Log.Warning("Server shutdown");
                Shutdown();
                return false;
            }

            new Thread(!Setting.NoDelay ? Tick : TickNoDelay) { IsBackground = true }.Start();
            OnStart(port, maxClients);
            return true;
        }

        /// <summary>
        ///     Destruction
        /// </summary>
        public override bool Shutdown() => StopListening();

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public override uint GetRoundTripTime(uint id) => _peers.TryGetValue(id, out var peer) ? peer.RoundTripTime : 0U;

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Send<T>(uint id, in T obj)
        {
            if (!_peers.TryGetValue(id, out var peer) || !ENetSerializer.Create(in obj, out var packet))
                return;
            _outgoing.Enqueue(new ENetPacket(peer, packet));
        }

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Broadcast<T>(in T obj) => Broadcast(Connections, in obj);

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="ids">ClientIds</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Broadcast<T>(IEnumerable<uint> ids, in T obj)
        {
            if (!NetworkSerializer.Serialize(in obj, out var payload))
                return;
            foreach (var id in ids)
            {
                if (_peers.TryGetValue(id, out var peer))
                {
                    var packet = ENetSerializer.RawCreate(payload);
                    _outgoing.Enqueue(new ENetPacket(peer, packet));
                }
            }
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <param name="id">ClientId</param>
        public override void Disconnect(uint id)
        {
            if (!_peers.TryRemove(id, out var peer))
                return;
            _removedConnections.Add(id);
            _removedPeers.Enqueue(peer);
        }

        /// <summary>
        ///     Publish event
        /// </summary>
        /// <param name="onryoEvent">Event</param>
        private void PublishEvent(in ENetServerEvent onryoEvent) => _onryoEvents.Enqueue(onryoEvent);

        /// <summary>
        ///     Tick
        /// </summary>
        private void Tick()
        {
            var tick = (int)Setting.Tick;
            while (IsSet)
            {
                TickOutgoing();
                TickIncoming();
                if (!IsSet)
                    break;
                _serverPeer.Flush();
                Thread.Sleep(tick);
            }

            TickOver();
        }

        /// <summary>
        ///     Tick
        /// </summary>
        private void TickNoDelay()
        {
            while (IsSet)
            {
                TickOutgoing();
                TickIncoming();
                if (!IsSet)
                    break;
                _serverPeer.Flush();
            }

            TickOver();
        }

        /// <summary>
        ///     Tick
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TickIncoming()
        {
            var polled = false;
            while (!polled)
            {
                if (_serverPeer.CheckEvents(out var netEvent) <= 0)
                {
                    if (_serverPeer.Service(0, out netEvent) <= 0)
                        break;
                    polled = true;
                }

                var peer = netEvent.Peer;
                var id = peer.ID;
                switch (netEvent.Type)
                {
                    case EventType.Receive:
                        var length = netEvent.Packet.Length;
                        if (length > 1024)
                        {
                            netEvent.Packet.Dispose();
                            goto error;
                        }

                        Marshal.Copy(netEvent.Packet.Data, _messageBuffer, 0, length);
                        netEvent.Packet.Dispose();
                        var payload = _messageBuffer.AsSpan(0, length);
                        var networkPacket = NetworkPacketPool.Rent(length - 4);
                        try
                        {
                            MemoryPackSerializer.Deserialize(payload, ref networkPacket);
                        }
                        catch
                        {
                            NetworkPacketPool.Return(networkPacket);
                            goto error;
                        }

                        PublishEvent(new ENetServerEvent(NetworkEventType.Data, id, networkPacket));
                        continue;
                    case EventType.Connect:
                        var ip = peer.IP;
                        if (CheckBanned(ip))
                        {
                            peer.DisconnectNow(0U);
                            continue;
                        }

                        if (netEvent.Data != 3847762548U || !IPAddress.TryParse(ip, out var ipAddress))
                        {
                            AddToBlacklist(ip);
                            peer.DisconnectNow(0U);
                            continue;
                        }

                        peer.PingInterval(500U);
                        peer.Timeout(0U, 5000U, 5000U);
                        _pendingConnections.Add(id);
                        PublishEvent(new ENetServerEvent(NetworkEventType.Connect, id, new IPEndPoint(ipAddress, peer.Port), peer));
                        continue;
                    case EventType.Disconnect:
                        goto remove;
                    case EventType.None:
                        continue;
                    case EventType.Timeout:
                    default:
                        error:
                        peer.DisconnectNow(0U);
                        remove:
                        if (!_peers.TryRemove(id, out _) && !_pendingConnections.Remove(id) && !_removedConnections.Remove(id))
                            continue;
                        PublishEvent(new ENetServerEvent(NetworkEventType.Disconnect, id));
                        continue;
                }
            }
        }

        /// <summary>
        ///     Tick
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TickOutgoing()
        {
            while (_removedPeers.TryDequeue(out var peer))
                peer.Disconnect(0U);
            while (_outgoing.TryDequeue(out var packet))
                packet.Send();
        }

        /// <summary>
        ///     Tick over
        /// </summary>
        private void TickOver()
        {
            foreach (var peer in _peers.Values)
                peer.DisconnectNow(0U);
            _serverPeer.Flush();
            _serverPeer.Dispose();
            _pendingConnections.Clear();
            _removedConnections.Clear();
            _removedPeers.Clear();
            _peers.Clear();
            while (_onryoEvents.TryDequeue(out var netEvent))
            {
                if (netEvent.EventType == NetworkEventType.Data)
                    NetworkPacketPool.Return(in netEvent.Data);
            }

            while (_outgoing.TryDequeue(out var packet))
                packet.Dispose();
            Library.Deinitialize();
        }

        /// <summary>
        ///     Polling
        /// </summary>
        protected override void Poll()
        {
            while (_onryoEvents.TryDequeue(out var netEvent))
            {
                var id = netEvent.Id;
                switch (netEvent.EventType)
                {
                    case NetworkEventType.Data:
                        InvokeHandler(id, in netEvent.Data);
                        NetworkPacketPool.Return(in netEvent.Data);
                        continue;
                    case NetworkEventType.Connect:
                        _pendingConnections.Remove(id);
                        _peers[id] = netEvent.Connection;
                        OnConnected(id, netEvent.RemoteEndPoint);
                        continue;
                    case NetworkEventType.Disconnect:
                        OnDisconnected(id);
                        continue;
                    default:
                        continue;
                }
            }
        }
    }
}