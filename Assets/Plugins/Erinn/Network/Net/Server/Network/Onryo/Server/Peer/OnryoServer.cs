//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

#pragma warning disable CS8604

// ReSharper disable UseCollectionExpression
// ReSharper disable UseNegatedPatternMatching
// ReSharper disable PossibleNullReferenceException

namespace Erinn
{
    /// <summary>
    ///     Server
    /// </summary>
    public sealed partial class OnryoServerPeer
    {
        /// <summary>
        ///     Maximum connection
        /// </summary>
        private uint _maxClients;

        /// <summary>
        ///     Listener
        /// </summary>
        private TcpListener _tcpListener;

        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly ConcurrentUintIndexPool _indexPool = new();

        /// <summary>
        ///     Pending Connections
        /// </summary>
        private readonly ConcurrentHashSet<OnryoConnection> _pendingConnections = new();

        /// <summary>
        ///     Client Connections
        /// </summary>
        private readonly ConcurrentDictionary<uint, OnryoConnection> _peers = new();

        /// <summary>
        ///     Event queue
        /// </summary>
        private readonly ConcurrentQueue<OnryoServerEvent> _onryoEvents = new();

        /// <summary>
        ///     Publish event
        /// </summary>
        /// <param name="onryoEvent">Event</param>
        internal void PublishEvent(in OnryoServerEvent onryoEvent) => _onryoEvents.Enqueue(onryoEvent);

        /// <summary>
        ///     Call on Connect
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="ipEndPoint">RemoteIPEndPoint</param>
        /// <param name="connection">Connection</param>
        internal void OnAuthenticated(uint id, IPEndPoint ipEndPoint, OnryoConnection connection)
        {
            _pendingConnections.Remove(connection);
            PublishEvent(new OnryoServerEvent(NetworkEventType.Connect, id, ipEndPoint, connection));
        }

        /// <summary>
        ///     Call on disconnect
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="connection">Connection</param>
        internal void OnAuthenticatedFailed(uint id, OnryoConnection connection)
        {
            if (!_pendingConnections.Remove(connection))
                return;
            _indexPool.Return(id);
        }

        /// <summary>
        ///     Call on disconnect
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="connection">Connection</param>
        /// <param name="ipEndPoint">RemoteIPEndPoint</param>
        internal void OnAuthenticatedFailed(uint id, OnryoConnection connection, IPEndPoint ipEndPoint)
        {
            if (!_pendingConnections.Remove(connection))
                return;
            AddToBlacklist(ipEndPoint);
            _indexPool.Return(id);
        }

        /// <summary>
        ///     Asynchronous waiting
        /// </summary>
        private void AcceptAsync()
        {
            while (IsSet)
            {
                try
                {
                    var tcpClient = _tcpListener.AcceptTcpClient();
                    if (!tcpClient.Connected)
                        continue;
                    var ipEndPoint = tcpClient.Client.RemoteEndPoint as IPEndPoint;
                    if (CheckBanned(ipEndPoint) || _pendingConnections.Count + _peers.Count >= _maxClients)
                    {
                        tcpClient.Close();
                        continue;
                    }

                    var id = _indexPool.Rent();
                    var connection = new OnryoConnection(this, id, tcpClient, ipEndPoint);
                    _pendingConnections.Add(connection);
                }
                catch
                {
                    //
                }
            }
        }
    }
}