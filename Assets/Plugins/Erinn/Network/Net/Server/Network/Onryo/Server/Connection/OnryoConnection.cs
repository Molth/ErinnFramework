//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
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
    ///     Client Connection
    /// </summary>
    internal sealed class OnryoConnection
    {
        /// <summary>
        ///     Server
        /// </summary>
        private readonly OnryoServerPeer _server;

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
        ///     Interlocked disconnected instead of volatile
        /// </summary>
        private InterlockedBoolean _disposed;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="id">Index</param>
        /// <param name="tcpClient">summary>TcpClient</param>
        /// <param name="ipEndPoint">RemoteIPEndPoint</param>
        public OnryoConnection(OnryoServerPeer server, uint id, TcpClient tcpClient, IPEndPoint ipEndPoint)
        {
            _server = server;
            _id = id;
            _tcpClient = tcpClient;
            _ipEndPoint = ipEndPoint;
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

            _tcpClient.Close();
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
            _server.PublishEvent(new OnryoServerEvent(NetworkEventType.Disconnect, _id, this));
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (!_disposed.Set(true))
                return;
            OnDisconnected();
            _server.PublishEvent(new OnryoServerEvent(NetworkEventType.Disconnect, _id, this));
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
                _networkStream.RawReadExactly(receiveBuffer, 4);
            }
            catch
            {
                _server.OnAuthenticatedFailed(_id, this);
                _tcpClient.Close();
                OnryoPool.Return(receiveBuffer);
                return;
            }

            var cookie = NetworkCrypto.Read(receiveBuffer);
            if (cookie != 3847762548U)
            {
                _server.OnAuthenticatedFailed(_id, this, _ipEndPoint);
                _tcpClient.Close();
                OnryoPool.Return(receiveBuffer);
                return;
            }

            _outgoing = new ConcurrentQueue<ArraySegment<byte>>();
            _sendPending = new ManualResetEventSlim(false);
            var sendThread = new Thread(WriteAsync) { IsBackground = true };
            sendThread.Start();
            _server.OnAuthenticated(_id, _ipEndPoint, this);
            while (_tcpClient.Connected)
            {
                try
                {
                    _networkStream.RawReadExactly(receiveBuffer, 4);
                    var length = NetworkSerializer.Read(receiveBuffer);
                    if (length <= 1024)
                    {
                        _networkStream.RawReadExactly(receiveBuffer, length);
                        var payload = receiveBuffer.AsSpan(0, length);
                        var networkPacket = NetworkPacketPool.Rent(length - 4);
                        try
                        {
                            MemoryPackSerializer.Deserialize(payload, ref networkPacket);
                        }
                        catch
                        {
                            NetworkPacketPool.Return(in networkPacket);
                            OnPeerDisconnected();
                            break;
                        }

                        _server.PublishEvent(new OnryoServerEvent(NetworkEventType.Data, _id, networkPacket));
                    }
                    else
                    {
                        OnPeerDisconnected();
                        break;
                    }
                }
                catch
                {
                    OnPeerDisconnected();
                    break;
                }
            }

            sendThread.Interrupt();
            OnryoPool.Return(receiveBuffer);
        }

        /// <summary>
        ///     Asynchronous write
        /// </summary>
        private void WriteAsync()
        {
            var sendBuffer = OnryoPool.Rent();
            var headerBuffer = OnryoPool.RentHeader();
            while (_tcpClient.Connected)
            {
                try
                {
                    _sendPending.Reset();
                    _networkStream.RawWrite(sendBuffer, headerBuffer, _outgoing);
                    _sendPending.Wait();
                }
                catch
                {
                    OnPeerDisconnected();
                    break;
                }
            }

            OnryoPool.Return(sendBuffer);
            OnryoPool.ReturnHeader(headerBuffer);
        }
    }
}