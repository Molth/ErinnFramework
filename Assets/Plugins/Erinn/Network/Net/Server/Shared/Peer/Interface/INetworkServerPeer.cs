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
    ///     Network server interface
    /// </summary>
    public interface INetworkServerPeer : INetworkPeer, INetworkServerMessageChannel
    {
        /// <summary>
        ///     Authenticated connections
        /// </summary>
        IEnumerable<uint> Connections { get; }

        /// <summary>
        ///     Connections count
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Connected callback
        /// </summary>
        event Action<uint> OnConnectedCallback;

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        event Action<uint> OnDisconnectedCallback;

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        bool Start(ushort port, uint maxClients);

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        uint GetRoundTripTime(uint id);

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        void Send<T>(uint id, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        void Broadcast<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Send packets
        /// </summary>
        /// <param name="ids">ClientIds</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        void Broadcast<T>(IEnumerable<uint> ids, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Get connection
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="connection">Connection</param>
        /// <returns>Connection</returns>
        bool GetConnection(uint id, out NetworkConnection connection);

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <param name="id">ClientId</param>
        void Disconnect(uint id);
    }
}