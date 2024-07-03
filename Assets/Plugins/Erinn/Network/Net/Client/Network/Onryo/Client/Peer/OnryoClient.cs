//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net.Sockets;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Client
    /// </summary>
    public sealed partial class OnryoClientPeer
    {
        /// <summary>
        ///     Client Endpoint
        /// </summary>
        private TcpClient _tcpClient;

        /// <summary>
        ///     Event
        /// </summary>
        private readonly ConcurrentQueue<OnryoClientEvent> _onryoEvents = new();

        /// <summary>
        ///     The data packets to be sent
        /// </summary>
        private readonly ConcurrentQueue<ArraySegment<byte>> _outgoing = new();

        /// <summary>
        ///     Disconnected
        /// </summary>
        private InterlockedBoolean _disposed;

        /// <summary>
        ///     Network stream
        /// </summary>
        private NetworkStream _networkStream;

        /// <summary>
        ///     Publish event
        /// </summary>
        /// <param name="onryoClientEvent">Event</param>
        private void PublishEvent(in OnryoClientEvent onryoClientEvent) => _onryoEvents.Enqueue(onryoClientEvent);

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        private async void Connect(string ipAddress, ushort port)
        {
            _disposed.Set(false);
            _tcpClient = new TcpClient();
            try
            {
                await _tcpClient.ConnectAsync(ipAddress, port);
            }
            catch
            {
                OnPeerDisconnected();
                return;
            }

            if (!_tcpClient.Connected)
            {
                OnPeerDisconnected();
                return;
            }

            _tcpClient.NoDelay = true;
            _tcpClient.SendTimeout = 5000;
            _tcpClient.ReceiveTimeout = 0;
            _tcpClient.SendBufferSize = 1024;
            _tcpClient.ReceiveBufferSize = 1024;
            _networkStream = _tcpClient.GetStream();
            new Thread(WriteAsync) { IsBackground = true }.Start();
            new Thread(ReadAsync) { IsBackground = true }.Start();
        }

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        private void OnPeerDisconnected()
        {
            if (!_disposed.Set(true))
                return;
            _outgoing.Clear();
            _tcpClient.Close();
            PublishEvent(new OnryoClientEvent(NetworkEventType.Disconnect));
        }

        /// <summary>
        ///     Asynchronous read
        /// </summary>
        private void ReadAsync()
        {
            var receiveBuffer = OnryoPool.Rent();
            while (_tcpClient != null && _tcpClient.Connected)
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
                            continue;
                        }

                        PublishEvent(new OnryoClientEvent(NetworkEventType.Data, networkPacket));
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

            OnryoPool.Return(receiveBuffer);
        }

        /// <summary>
        ///     Asynchronous write
        /// </summary>
        private void WriteAsync()
        {
            var sendBuffer = OnryoPool.Rent();
            try
            {
                NetworkCrypto.Write(sendBuffer, 3847762548U);
                _networkStream.Write(sendBuffer, 0, 4);
            }
            catch
            {
                OnPeerDisconnected();
                OnryoPool.Return(sendBuffer);
                return;
            }

            PublishEvent(new OnryoClientEvent(NetworkEventType.Connect));
            var headerBuffer = OnryoPool.RentHeader();
            while (_tcpClient != null && _tcpClient.Connected)
            {
                try
                {
                    _networkStream.RawWrite(sendBuffer, headerBuffer, _outgoing);
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