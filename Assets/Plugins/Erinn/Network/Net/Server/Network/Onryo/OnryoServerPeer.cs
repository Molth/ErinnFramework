//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net.Sockets;
#if UNITY_2021_3_OR_NEWER || GODOT
using System.Collections.Generic;
using System.Threading;
#endif

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public sealed partial class OnryoServerPeer : NetworkServerPeer
    {
        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public OnryoServerPeer(NetworkServerMessageChannel messageChannel) : base(messageChannel)
        {
        }

        /// <summary>
        ///     Protocol type
        /// </summary>
        public override NetworkProtocolType ProtocolType => NetworkProtocolType.Tcp;

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public override bool Start(ushort port, uint maxClients)
        {
            if (!StartListening())
                return false;
            _maxClients = maxClients;
            _pendingConnections.EnsureCapacity((int)maxClients);
            _tcpListener = TcpListener.Create(port);
            var socket = _tcpListener.Server;
            socket.NoDelay = true;
            socket.SendTimeout = 5000;
            socket.ReceiveTimeout = 0;
            try
            {
                _tcpListener.Start();
            }
            catch
            {
                Log.Warning("Server shutdown");
                Shutdown();
                return false;
            }

            new Thread(AcceptAsync) { IsBackground = true }.Start();
            OnStart(port, maxClients);
            return true;
        }

        /// <summary>
        ///     Destruction
        /// </summary>
        public override bool Shutdown()
        {
            if (!StopListening())
                return false;
            _tcpListener?.Stop();
            _tcpListener = null;
            foreach (var connection in _pendingConnections)
                connection.Release();
            foreach (var connection in _peers.Values)
                connection.Release();
            _indexPool.Clear();
            _pendingConnections.Clear();
            _peers.Clear();
            while (_onryoEvents.TryDequeue(out var netEvent))
            {
                if (netEvent.EventType == NetworkEventType.Data)
                    NetworkPacketPool.Return(in netEvent.Data);
            }

            return true;
        }

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public override uint GetRoundTripTime(uint id) => 0U;

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Send<T>(uint id, in T obj)
        {
            if (!_peers.TryGetValue(id, out var connection) || !NetworkSerializer.Serialize(in obj, out var payload))
                return;
            connection.Send(in payload);
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
                    peer.Send(in payload);
            }
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <param name="id">ClientId</param>
        public override void Disconnect(uint id)
        {
            if (!_peers.TryRemove(id, out var connection))
                return;
            connection.Disconnect();
        }

        /// <summary>
        ///     Call on disconnect
        /// </summary>
        internal void OnPeerDisconnected(uint id) => _peers.TryRemove(id, out _);

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
                        _peers[id] = netEvent.Connection;
                        OnConnected(id, netEvent.RemoteEndPoint);
                        continue;
                    case NetworkEventType.Disconnect:
                        netEvent.Connection.Dispose();
                        _indexPool.Return(id);
                        OnDisconnected(id);
                        continue;
                    default:
                        continue;
                }
            }
        }
    }
}