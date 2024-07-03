//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ENet;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#pragma warning disable CS8602
#pragma warning disable CS8604
#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed class ENetPunchClientPeer : NetworkClientPeer
    {
        /// <summary>
        ///     Client node
        /// </summary>
        private readonly Host _clientPeer = new();

        /// <summary>
        ///     Message buffer
        /// </summary>
        private readonly byte[] _messageBuffer = new byte[1024];

        /// <summary>
        ///     Server Connection
        /// </summary>
        private Peer _connection;

        /// <summary>
        ///     Event queue
        /// </summary>
        private readonly ConcurrentQueue<ENetClientEvent> _onryoEvents = new();

        /// <summary>
        ///     The data packets to be sent
        /// </summary>
        private readonly ConcurrentQueue<Packet> _outgoing = new();

        /// <summary>
        ///     Host
        /// </summary>
        private Peer _host;

        /// <summary>
        ///     Structure
        /// </summary>
        public ENetPunchClientPeer() : base(new NetworkClientMessageChannel())
        {
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public ENetPunchClientPeer(NetworkClientMessageChannel messageChannel) : base(messageChannel)
        {
        }

        /// <summary>
        ///     Protocol type
        /// </summary>
        public override NetworkProtocolType ProtocolType => NetworkProtocolType.Udp;

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public override bool Start(string ipAddress, ushort port) => throw new NotImplementedException();

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        /// <param name="serviceIPAddress">Service IpAddress</param>
        /// <param name="servicePort">Service Port</param>
        public bool Start(string ipAddress, ushort port, string serviceIPAddress, ushort servicePort)
        {
            if (!StartListening())
                return false;
            Library.Initialize();
            var serviceAddress = new Address { Port = servicePort };
            serviceAddress.SetHost(serviceIPAddress);
            var address = new Address { Port = port };
            address.SetHost(ipAddress);
            _clientPeer.Create(new Address?(), 2, 0, 0U, 0U, 0);
            _host = _clientPeer.Connect(serviceAddress, 0, 3847762548U);
            _host.PingInterval(6000U);
            _host.Timeout(0U, 60000U, 60000U);
            _connection = _clientPeer.Connect(address, 0, 3847762548U);
            _connection.PingInterval(500U);
            _connection.Timeout(0U, 5000U, 5000U);
            new Thread(!Setting.NoDelay ? Tick : TickNoDelay) { IsBackground = true }.Start();
            OnStart(ipAddress, port);
            return true;
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override bool Shutdown() => StopListening();

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public override uint GetRoundTripTime() => Connected ? _connection.RoundTripTime : 0U;

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Send<T>(in T obj)
        {
            if (!Connected || !ENetSerializer.Create(in obj, out var packet))
                return;
            _outgoing.Enqueue(packet);
        }

        /// <summary>
        ///     Publish event
        /// </summary>
        /// <param name="onryoClientEvent">Event</param>
        private void PublishEvent(in ENetClientEvent onryoClientEvent) => _onryoEvents.Enqueue(onryoClientEvent);

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
                _clientPeer.Flush();
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
                _clientPeer.Flush();
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
                if (_clientPeer.CheckEvents(out var netEvent) <= 0)
                {
                    if (_clientPeer.Service(0, out netEvent) <= 0)
                        break;
                    polled = true;
                }

                var id = netEvent.Peer.ID;
                switch (netEvent.Type)
                {
                    case EventType.Receive:
                        var length = netEvent.Packet.Length;
                        if (length > 1024)
                        {
                            netEvent.Packet.Dispose();
                            continue;
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
                            continue;
                        }

                        PublishEvent(new ENetClientEvent(NetworkEventType.Data, networkPacket));
                        continue;
                    case EventType.Connect:
                        if (id == 0U)
                        {
                            if (Connected)
                                continue;
                            var endPoint = new ENetPunchEndPoint(_connection.IP, _connection.Port);
                            if (!ENetSerializer.Create(in endPoint, out var packet))
                                continue;
                            if (_host.Send(0, ref packet))
                                continue;
                            packet.Dispose();
                            continue;
                        }

                        PublishEvent(new ENetClientEvent(NetworkEventType.Connect));
                        continue;
                    case EventType.Disconnect:
                        if (id == 0U)
                            continue;
                        PublishEvent(new ENetClientEvent(NetworkEventType.Disconnect));
                        continue;
                    case EventType.None:
                        continue;
                    case EventType.Timeout:
                        if (id == 0U)
                        {
                            netEvent.Peer.DisconnectNow(0U);
                            continue;
                        }

                        PublishEvent(new ENetClientEvent(NetworkEventType.Disconnect));
                        continue;
                    default:
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
            while (_outgoing.TryDequeue(out var packet))
            {
                if (_connection.Send(0, ref packet))
                    continue;
                packet.Dispose();
            }
        }

        /// <summary>
        ///     Tick over
        /// </summary>
        private void TickOver()
        {
            if (_host.IsSet)
                _host.DisconnectNow(0U);
            if (_connection.IsSet)
                _connection.DisconnectNow(0U);
            _clientPeer.Flush();
            _clientPeer.Dispose();
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
                switch (netEvent.EventType)
                {
                    case NetworkEventType.Data:
                        InvokeHandler(in netEvent.Data);
                        NetworkPacketPool.Return(in netEvent.Data);
                        continue;
                    case NetworkEventType.Connect:
                        OnConnected();
                        continue;
                    case NetworkEventType.Disconnect:
                        OnDisconnected();
                        continue;
                    default:
                        continue;
                }
            }
        }
    }
}