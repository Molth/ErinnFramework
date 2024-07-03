//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
#endif

#pragma warning disable CS8601
#pragma warning disable CS8618
#pragma warning disable CS8625

// ReSharper disable UseCollectionExpression
// ReSharper disable PossibleNullReferenceException

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public abstract partial class NetworkServerPeer : NetworkPeer, INetworkServerPeer
    {
        /// <summary>
        ///     Authenticated connections
        /// </summary>
        private readonly ConcurrentHashSet<uint> _connections = new();

        /// <summary>
        ///     Connection IPEndPoints
        /// </summary>
        private readonly ConcurrentDictionary<uint, NetworkConnection> _ipEndPoints = new();

        /// <summary>
        ///     Authenticated connections
        /// </summary>
        public IEnumerable<uint> Connections => _connections;

        /// <summary>
        ///     Connections count
        /// </summary>
        public int Count => _connections.Count;

        /// <summary>
        ///     Connected callback
        /// </summary>
        public event Action<uint> OnConnectedCallback;

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        public event Action<uint> OnDisconnectedCallback;

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public abstract uint GetRoundTripTime(uint id);

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public abstract void Send<T>(uint id, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public abstract void Broadcast<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="ids">ClientIds</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public abstract void Broadcast<T>(IEnumerable<uint> ids, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Get connection
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="connection">Connection</param>
        /// <returns>Connection</returns>
        public bool GetConnection(uint id, out NetworkConnection connection) => _ipEndPoints.TryGetValue(id, out connection);

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <param name="id">ClientId</param>
        public abstract void Disconnect(uint id);

        /// <summary>
        ///     Destruction
        /// </summary>
        protected bool StopListening()
        {
            if (StopPolling())
            {
                _connections.Clear();
                _ipEndPoints.Clear();
                WriteBlacklist();
                Log.Info("Server shutdown");
            }

            return Reset();
        }

        /// <summary>
        ///     Connected callback
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="ipEndPoint">IPEndPoint</param>
        protected void OnConnected(uint id, IPEndPoint ipEndPoint)
        {
            if (CheckBanned(ipEndPoint.Address))
            {
                Disconnect(id);
                return;
            }

            _connections.Add(id);
            _ipEndPoints[id] = new NetworkConnection(ipEndPoint);
            Log.Info($"Connected[{id}] [{ipEndPoint}]");
            OnConnectedCallback.Invoke(id);
        }

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        /// <param name="id">ClientId</param>
        protected void OnDisconnected(uint id)
        {
            _connections.Remove(id);
            if (!_ipEndPoints.TryRemove(id, out var ipEndPoint))
                return;
            Log.Info($"Disconnected[{id}] [{ipEndPoint}]");
            OnDisconnectedCallback.Invoke(id);
        }
    }
}