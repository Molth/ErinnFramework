//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#pragma warning disable CS8604
#pragma warning disable CS8618
#pragma warning disable CS8625

// ReSharper disable StringLiteralTypo

namespace Erinn
{
    /// <summary>
    ///     Client Connection
    /// </summary>
    internal sealed class WebSocketConnection
    {
        /// <summary>
        ///     Server
        /// </summary>
        private readonly WebSocketServerPeer _server;

        /// <summary>
        ///     Index
        /// </summary>
        private readonly uint _id;

        /// <summary>
        ///     Client Endpoint
        /// </summary>
        private readonly TcpClient _tcpClient;

        /// <summary>
        ///     RemoteIPEndPoint
        /// </summary>
        private readonly IPEndPoint _ipEndPoint;

        /// <summary>
        ///     The data packets to be sent
        /// </summary>
        private ConcurrentQueue<ArraySegment<byte>> _outgoing;

        /// <summary>
        ///     ManualResetEvent to wake up the send thread
        /// </summary>
        private ManualResetEventSlim _sendPending;

        /// <summary>
        ///     Network stream
        /// </summary>
        private NetworkStream _networkStream;

        /// <summary>
        ///     WebSocket
        /// </summary>
        private WebSocket _webSocket;

        /// <summary>
        ///     CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     Interlocked disconnected instead of volatile
        /// </summary>
        private InterlockedBoolean _disposed;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="id">Index</param>
        /// <param name="tcpClient">TcpClient</param>
        /// <param name="remoteEndPoint">RemoteEndPoint</param>
        public WebSocketConnection(WebSocketServerPeer server, uint id, TcpClient tcpClient, IPEndPoint remoteEndPoint)
        {
            _server = server;
            _id = id;
            _tcpClient = tcpClient;
            _ipEndPoint = remoteEndPoint;
            new Thread(ReadAsync) { IsBackground = true }.Start();
        }

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="payload">Data packet</param>
        public void Send(in ArraySegment<byte> payload)
        {
            var count = payload.Count;
            var buffer = NetworkBytesPool.Rent(count);
            NetworkSerializer.BlockCopy(in payload, 0, buffer, 0, count);
            _outgoing.Enqueue(new ArraySegment<byte>(buffer, 0, count));
            _sendPending.Set();
        }

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose()
        {
            _outgoing = null;
            _sendPending?.Dispose();
            _sendPending = null;
            _networkStream = null;
            _webSocket = null;
            _cancellationTokenSource = null;
        }

        /// <summary>
        ///     Call on disconnect
        /// </summary>
        private void OnDisconnected()
        {
            if (_outgoing != null)
            {
                while (_outgoing.TryDequeue(out var segment))
                    NetworkBytesPool.Return(segment.Array);
            }

            Close();
        }

        /// <summary>
        ///     Call on disconnect
        /// </summary>
        private void OnPeerDisconnected()
        {
            if (!_disposed.Set(true))
                return;
            _server.OnPeerDisconnected(_id);
            OnDisconnected();
            _server.PublishEvent(new WebSocketServerEvent(NetworkEventType.Disconnect, _id, this));
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (!_disposed.Set(true))
                return;
            OnDisconnected();
            _server.PublishEvent(new WebSocketServerEvent(NetworkEventType.Disconnect, _id, this));
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public void Release()
        {
            if (!_disposed.Set(true))
                return;
            OnDisconnected();
            Dispose();
        }

        /// <summary>
        ///     Close
        /// </summary>
        private void Close()
        {
            _cancellationTokenSource?.Cancel();
            _webSocket?.Dispose();
            _tcpClient.Close();
        }

        /// <summary>
        ///     Asynchronous read
        /// </summary>
        private void ReadAsync()
        {
            _tcpClient.NoDelay = true;
            _tcpClient.SendTimeout = 5000;
            _tcpClient.ReceiveTimeout = 0;
            _tcpClient.SendBufferSize = 1024;
            _tcpClient.ReceiveBufferSize = 1024;
            _networkStream = _tcpClient.GetStream();
            var receiveBuffer = OnryoPool.Rent();
            try
            {
                var bytesRead = _networkStream.Read(receiveBuffer, 0, 1024);
                if (bytesRead == 0)
                {
                    HandShakeFailed();
                    return;
                }

                var request = Encoding.UTF8.GetString(receiveBuffer, 0, bytesRead);
                if (!request.Contains("Upgrade: websocket"))
                {
                    AddToBlacklist();
                    return;
                }

                const string magicString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                var secWebSocketKey = request[(request.IndexOf("Sec-WebSocket-Key:", StringComparison.Ordinal) + 19)..].Split('\r')[0].Trim();
                var combined = secWebSocketKey + magicString;
                var acceptKey = NetworkCrypto.SHA1ToBase64String(combined);
                var response = $"HTTP/1.1 101 Switching Protocols\r\nConnection: Upgrade\r\nUpgrade: websocket\r\nSec-WebSocket-Accept: {acceptKey}\r\n\r\n";
                var responseCount = Encoding.UTF8.GetByteCount(response);
                Span<byte> responseBytes = stackalloc byte[responseCount];
                Encoding.UTF8.GetBytes(response, responseBytes);
                _networkStream.Write(responseBytes);
            }
            catch
            {
                HandShakeFailed();
                return;
            }

            _webSocket = WebSocket.CreateFromStream(_networkStream, true, null, TimeSpan.FromMilliseconds(5000));
            _cancellationTokenSource = new CancellationTokenSource();
            ValueWebSocketReceiveResult handShakeResult;
            try
            {
                var task = _webSocket.ReceiveAsync(receiveBuffer.AsMemory(0, 4), _cancellationTokenSource.Token).AsTask();
                task.Wait();
                handShakeResult = task.Result;
            }
            catch
            {
                HandShakeFailed();
                return;
            }

            if (handShakeResult.MessageType == WebSocketMessageType.Close)
            {
                HandShakeFailed();
                return;
            }

            if (handShakeResult.MessageType == WebSocketMessageType.Text || !handShakeResult.EndOfMessage || handShakeResult.Count != 4)
            {
                AddToBlacklist();
                return;
            }

            var cookie = NetworkCrypto.Read(receiveBuffer);
            if (cookie != 3847762548U)
            {
                AddToBlacklist();
                return;
            }

            _outgoing = new ConcurrentQueue<ArraySegment<byte>>();
            _sendPending = new ManualResetEventSlim(false);
            var sendThread = new Thread(WriteAsync) { IsBackground = true };
            sendThread.Start();
            _server.OnAuthenticated(_id, _ipEndPoint, this);
            var memory = receiveBuffer.AsMemory();
            while (_tcpClient.Connected)
            {
                try
                {
                    var task = _webSocket.ReceiveAsync(memory, _cancellationTokenSource.Token).AsTask();
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
                        task = _webSocket.ReceiveAsync(receiveBuffer.AsMemory(count, remaining), _cancellationTokenSource.Token).AsTask();
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
                        throw;
                    }

                    _server.PublishEvent(new WebSocketServerEvent(NetworkEventType.Data, _id, networkPacket));
                }
                catch
                {
                    OnPeerDisconnected();
                    break;
                }
            }

            sendThread.Interrupt();
            OnryoPool.Return(receiveBuffer);
            return;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void HandShakeFailed()
            {
                _server.OnAuthenticatedFailed(_id, this);
                Close();
                OnryoPool.Return(receiveBuffer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void AddToBlacklist()
            {
                _server.OnAuthenticatedFailed(_id, this, _ipEndPoint);
                Close();
                OnryoPool.Return(receiveBuffer);
            }
        }

        /// <summary>
        ///     Asynchronous write
        /// </summary>
        private void WriteAsync()
        {
            while (_tcpClient.Connected)
            {
                try
                {
                    _sendPending.Reset();
                    while (_outgoing.TryDequeue(out var payload))
                    {
                        try
                        {
                            _webSocket.SendAsync(payload, WebSocketMessageType.Binary, true, _cancellationTokenSource.Token).Wait();
                        }
                        finally
                        {
                            NetworkBytesPool.Return(payload.Array);
                        }
                    }

                    _sendPending.Wait();
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