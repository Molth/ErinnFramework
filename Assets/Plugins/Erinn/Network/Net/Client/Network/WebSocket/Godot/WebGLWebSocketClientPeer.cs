//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if GODOT
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Godot;
using MemoryPack;

#pragma warning disable CS8604
#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed class WebGLWebSocketClientPeer : NetworkClientPeer
    {
        /// <summary>
        ///     Inner WebSocket
        /// </summary>
        private WebSocketPeer _tcpClient;

        /// <summary>
        ///     State
        /// </summary>
        private WebSocketPeer.State _state;

        /// <summary>
        ///     Outgoing
        /// </summary>
        private readonly ConcurrentQueue<byte[]> _outgoing = new();

        /// <summary>
        ///     Disconnected
        /// </summary>
        private InterlockedBoolean _disposed;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public WebGLWebSocketClientPeer(NetworkClientMessageChannel messageChannel) : base(messageChannel)
        {
        }

        /// <summary>
        ///     Protocol type
        /// </summary>
        public override NetworkProtocolType ProtocolType => NetworkProtocolType.WebSocket;

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public override bool Start(string ipAddress, ushort port)
        {
            if (!StartListening())
                return false;
            _disposed.Set(false);
            _tcpClient = new WebSocketPeer();
            _state = WebSocketPeer.State.Connecting;
            var error = _tcpClient.ConnectToUrl($"ws://{ipAddress}:{port}");
            if (error != Error.Ok)
            {
                OnPeerDisconnected();
                return false;
            }

            OnStart(ipAddress, port);
            return true;
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override bool Shutdown()
        {
            if (!StopListening())
                return false;
            _disposed.Set(true);
            _state = WebSocketPeer.State.Connecting;
            Disconnect();
            return true;
        }

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public override uint GetRoundTripTime() => 0U;

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Send<T>(in T obj)
        {
            if (!Connected || !NetworkSerializer.Serialize(in obj, out var payload))
                return;
            var count = payload.Count;
            var buffer = new byte[count];
            NetworkSerializer.BlockCopy(payload, 0, buffer, 0, count);
            _outgoing.Enqueue(buffer);
        }

        /// <summary>
        ///     Polling
        /// </summary>
        protected override void Poll()
        {
            TickOutgoing();
            TickIncoming();
            _tcpClient.Poll();
            var state = _tcpClient.GetReadyState();
            if (_state != state)
            {
                _state = state;
                switch (state)
                {
                    case WebSocketPeer.State.Connecting:
                        break;
                    case WebSocketPeer.State.Open:
                        _tcpClient.PutPacket(NetworkCrypto.GetBytes(3847762548U));
                        OnConnected();
                        break;
                    case WebSocketPeer.State.Closing:
                        break;
                    case WebSocketPeer.State.Closed:
                        OnDisconnected();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///     Tick
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TickIncoming()
        {
            while (_tcpClient.GetAvailablePacketCount() > 0)
            {
                var payload = _tcpClient.GetPacket();
                var networkPacket = NetworkPacketPool.Rent(payload.Length - 4);
                try
                {
                    MemoryPackSerializer.Deserialize(payload, ref networkPacket);
                }
                catch
                {
                    NetworkPacketPool.Return(networkPacket);
                    continue;
                }

                InvokeHandler(in networkPacket);
                NetworkPacketPool.Return(networkPacket);
            }
        }

        /// <summary>
        ///     Tick
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TickOutgoing()
        {
            while (_outgoing.TryDequeue(out var payload))
                _tcpClient.PutPacket(payload);
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        private void Disconnect()
        {
            _tcpClient?.Dispose();
            _tcpClient = null;
        }

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        private void OnPeerDisconnected()
        {
            if (!_disposed.Set(true))
                return;
            _outgoing.Clear();
            Disconnect();
            OnDisconnected();
        }
    }
}
#endif