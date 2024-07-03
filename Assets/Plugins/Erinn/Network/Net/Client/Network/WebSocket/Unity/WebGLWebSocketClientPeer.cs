//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Concurrent;
using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed class WebGLWebSocketClientPeer : NetworkClientPeer, IWebSocketClient
    {
        /// <summary>
        ///     Client Endpoint
        /// </summary>
        private readonly WebGLWebSocket _tcpClient;

        /// <summary>
        ///     Packets
        /// </summary>
        private readonly ConcurrentQueue<WebSocketClientEvent> _packets = new();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public WebGLWebSocketClientPeer(NetworkClientMessageChannel messageChannel) : base(messageChannel) => _tcpClient = new WebGLWebSocket(this);

        /// <summary>
        ///     Protocol type
        /// </summary>
        public override NetworkProtocolType ProtocolType => NetworkProtocolType.WebSocket;

        /// <summary>
        ///     Open callback
        /// </summary>
        void IWebSocketClient.OnOpen()
        {
            var array = NetworkCrypto.GetBytes(3847762548U);
            _tcpClient.Send(array);
            PublishEvent(new WebSocketClientEvent(NetworkEventType.Connect));
        }

        /// <summary>
        ///     Close callback
        /// </summary>
        void IWebSocketClient.OnClose() => PublishEvent(new WebSocketClientEvent(NetworkEventType.Disconnect));

        /// <summary>
        ///     Message callback
        /// </summary>
        void IWebSocketClient.OnMessage(in ArraySegment<byte> payload)
        {
            var networkPacket = NetworkPacketPool.Rent(payload.Count - 4);
            try
            {
                MemoryPackSerializer.Deserialize(payload, ref networkPacket);
            }
            catch
            {
                NetworkPacketPool.Return(in networkPacket);
                return;
            }

            PublishEvent(new WebSocketClientEvent(NetworkEventType.Data, networkPacket));
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public override bool Start(string ipAddress, ushort port)
        {
            if (!StartListening())
                return false;
            _tcpClient.Connect(ipAddress, port);
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
            _tcpClient.Close();
            foreach (var netEvent in _packets)
            {
                if (netEvent.EventType == NetworkEventType.Data)
                    NetworkPacketPool.Return(in netEvent.Data);
            }

            _packets.Clear();
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
            var length = payload.Count;
            var array = new byte[length];
            NetworkSerializer.BlockCopy(payload, 0, array, 0, length);
            _tcpClient.Send(array);
        }

        /// <summary>
        ///     Polling
        /// </summary>
        protected override void Poll()
        {
            while (_packets.TryDequeue(out var netEvent))
            {
                switch (netEvent.EventType)
                {
                    case NetworkEventType.Data:
                        InvokeHandler(netEvent.Data);
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

        /// <summary>
        ///     Enqueue packet
        /// </summary>
        /// <param name="data">Data</param>
        private void PublishEvent(in WebSocketClientEvent data) => _packets.Enqueue(data);
    }
}
#endif