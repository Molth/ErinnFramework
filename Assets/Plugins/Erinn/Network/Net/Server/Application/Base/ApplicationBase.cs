//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
#endif

namespace Erinn
{
    /// <summary>
    ///     Application base
    /// </summary>
    public abstract partial class ApplicationBase : INetworkServerPeer, IServerRPCListener, IDisposable
    {
        /// <summary>
        ///     Is it valid
        /// </summary>
        private bool _isSet;

        /// <summary>
        ///     Listening
        /// </summary>
        public bool IsListening => _peer.IsListening;

        /// <summary>
        ///     Running
        /// </summary>
        public bool IsRunning => _peer.IsRunning;

        /// <summary>
        ///     Protocol type
        /// </summary>
        public NetworkProtocolType ProtocolType => _peer.ProtocolType;

        /// <summary>
        ///     Authenticated connections
        /// </summary>
        public IEnumerable<uint> Connections => _peer.Connections;

        /// <summary>
        ///     Connections count
        /// </summary>
        public int Count => _peer.Count;

        /// <summary>
        ///     Connected callback
        /// </summary>
        public event Action<uint> OnConnectedCallback
        {
            add => _peer.OnConnectedCallback += value;
            remove => _peer.OnConnectedCallback -= value;
        }

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        public event Action<uint> OnDisconnectedCallback
        {
            add => _peer.OnDisconnectedCallback += value;
            remove => _peer.OnDisconnectedCallback -= value;
        }

        /// <summary>
        ///     Close
        /// </summary>
        public bool Shutdown() => _peer.Shutdown();

        /// <summary>
        ///     Update
        /// </summary>
        public void Update() => _peer.Update();

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public uint GetRoundTripTime(uint id) => _peer.GetRoundTripTime(id);

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Send<T>(uint id, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.Send(id, in obj);

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Broadcast<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.Broadcast(in obj);

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="ids">ClientIds</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Broadcast<T>(IEnumerable<uint> ids, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.Broadcast(ids, in obj);

        /// <summary>
        ///     Get connection
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="connection">Connection</param>
        /// <returns>Connection</returns>
        public bool GetConnection(uint id, out NetworkConnection connection) => _peer.GetConnection(id, out connection);

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <param name="id">ClientId</param>
        public void Disconnect(uint id) => _peer.Disconnect(id);

        /// <summary>
        ///     Is it valid
        /// </summary>
        public bool IsSet => _isSet && _peer.IsSet;
    }
}