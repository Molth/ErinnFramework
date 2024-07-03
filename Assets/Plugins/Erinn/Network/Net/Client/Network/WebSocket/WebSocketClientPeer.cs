//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net.WebSockets;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#pragma warning disable CS8604
#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed class WebSocketClientPeer : NetworkClientPeer
    {
        /// <summary>
        ///     Inner WebSocket
        /// </summary>
        private ClientWebSocket _tcpClient;

        /// <summary>
        ///     CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     Packets
        /// </summary>
        private readonly ConcurrentQueue<WebSocketClientEvent> _onryoEvents = new();

        /// <summary>
        ///     Outgoing
        /// </summary>
        private readonly ConcurrentQueue<ArraySegment<byte>> _outgoing = new();

        /// <summary>
        ///     Disconnected
        /// </summary>
        private InterlockedBoolean _disposed;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public WebSocketClientPeer(NetworkClientMessageChannel messageChannel) : base(messageChannel)
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
            Connect(ipAddress, port);
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
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
            while (_onryoEvents.TryDequeue(out var netEvent))
            {
                if (netEvent.EventType == NetworkEventType.Data)
                    NetworkPacketPool.Return(in netEvent.Data);
            }

            while (_outgoing.TryDequeue(out var segment))
                NetworkBytesPool.Return(segment.Array);
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
            var buffer = NetworkBytesPool.Rent(count);
            NetworkSerializer.BlockCopy(in payload, 0, buffer, 0, count);
            _outgoing.Enqueue(new ArraySegment<byte>(buffer, 0, count));
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
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        private async void Connect(string ipAddress, ushort port)
        {
            _disposed.Set(false);
            _tcpClient = new ClientWebSocket();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                var uri = new Uri($"ws://{ipAddress}:{port}");
                await _tcpClient.ConnectAsync(uri, _cancellationTokenSource.Token);
            }
            catch
            {
                OnPeerDisconnected();
                return;
            }

            if (_tcpClient.State != WebSocketState.Open)
            {
                OnPeerDisconnected();
                return;
            }

            new Thread(WriteAsync) { IsBackground = true }.Start();
            new Thread(ReadAsync) { IsBackground = true }.Start();
        }

        /// <summary>
        ///     Enqueue packet
        /// </summary>
        /// <param name="data">Data</param>
        private void PublishEvent(in WebSocketClientEvent data) => _onryoEvents.Enqueue(data);

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
            PublishEvent(new WebSocketClientEvent(NetworkEventType.Disconnect));
        }

        /// <summary>
        ///     Asynchronous read
        /// </summary>
        private void ReadAsync()
        {
            var receiveBuffer = OnryoPool.Rent();
            var memory = receiveBuffer.AsMemory();
            while (_tcpClient != null && _tcpClient.State == WebSocketState.Open)
            {
                try
                {
                    var task = _tcpClient.ReceiveAsync(memory, _cancellationTokenSource.Token).AsTask();
                    task.Wait();
                    var result = task.Result;
                    if (result.MessageType != WebSocketMessageType.Binary)
                    {
                        OnPeerDisconnected();
                        break;
                    }

                    var count = result.Count;
                    var disconnected = false;
                    while (!result.EndOfMessage)
                    {
                        if (count == 1024)
                        {
                            disconnected = true;
                            break;
                        }

                        var remaining = 1024 - count;
                        task = _tcpClient.ReceiveAsync(receiveBuffer.AsMemory(count, remaining), _cancellationTokenSource.Token).AsTask();
                        task.Wait();
                        result = task.Result;
                        if (result.MessageType != WebSocketMessageType.Binary)
                        {
                            disconnected = true;
                            break;
                        }

                        count += result.Count;
                    }

                    if (disconnected)
                    {
                        OnPeerDisconnected();
                        break;
                    }

                    var payload = receiveBuffer.AsSpan(0, count);
                    var networkPacket = NetworkPacketPool.Rent(count - 4);
                    try
                    {
                        MemoryPackSerializer.Deserialize(payload, ref networkPacket);
                    }
                    catch
                    {
                        NetworkPacketPool.Return(in networkPacket);
                        continue;
                    }

                    PublishEvent(new WebSocketClientEvent(NetworkEventType.Data, networkPacket));
                }
                catch
                {
                    OnPeerDisconnected();
                    break;
                }
            }

            OnryoPool.Return(receiveBuffer);
        }

        /// <summary>
        ///     Asynchronous write
        /// </summary>
        private void WriteAsync()
        {
            var headerBuffer = OnryoPool.RentHeader();
            NetworkCrypto.Write(headerBuffer, 3847762548U);
            try
            {
                _tcpClient.SendAsync(new ArraySegment<byte>(headerBuffer, 0, 4), WebSocketMessageType.Binary, true, _cancellationTokenSource.Token).Wait();
            }
            catch
            {
                OnPeerDisconnected();
                return;
            }
            finally
            {
                OnryoPool.ReturnHeader(headerBuffer);
            }

            PublishEvent(new WebSocketClientEvent(NetworkEventType.Connect));
            while (_tcpClient != null && _tcpClient.State == WebSocketState.Open)
            {
                try
                {
                    while (_outgoing.TryDequeue(out var payload))
                    {
                        try
                        {
                            _tcpClient.SendAsync(payload, WebSocketMessageType.Binary, true, _cancellationTokenSource.Token).Wait();
                        }
                        finally
                        {
                            NetworkBytesPool.Return(payload.Array);
                        }
                    }
                }
                catch
                {
                    OnPeerDisconnected();
                    break;
                }
            }
        }
    }
}