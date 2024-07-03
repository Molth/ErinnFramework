//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Network client interface
    /// </summary>
    public interface INetworkClientPeer : INetworkPeer, INetworkClientMessageChannel
    {
        /// <summary>
        ///     Connected
        /// </summary>
        bool Connected { get; }

        /// <summary>
        ///     Connected callback
        /// </summary>
        event Action OnConnectedCallback;

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        event Action OnDisconnectedCallback;

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        bool Start(string ipAddress, ushort port);

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        uint GetRoundTripTime();

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        void Send<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;
    }
}