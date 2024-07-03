//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8602
#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed partial class NetworkClient : INetworkClientPeer
    {
        /// <summary>
        ///     Listening
        /// </summary>
        public bool IsListening => _peer.IsListening;

        /// <summary>
        ///     Running
        /// </summary>
        public bool IsRunning => _peer.IsRunning;

        /// <summary>
        ///     Connected
        /// </summary>
        public bool Connected => _peer.Connected;

        /// <summary>
        ///     Protocol type
        /// </summary>
        public NetworkProtocolType ProtocolType => _peer.ProtocolType;

        /// <summary>
        ///     Connected callback
        /// </summary>
        public event Action OnConnectedCallback;

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        public event Action OnDisconnectedCallback;

        /// <summary>
        ///     Close
        /// </summary>
        public bool Shutdown() => _peer != null && _peer.Shutdown();

        /// <summary>
        ///     Update
        /// </summary>
        public void Update() => _peer.Update();

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public uint GetRoundTripTime() => _peer.GetRoundTripTime();

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Send<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.Send(in obj);

        /// <summary>
        ///     Is it valid
        /// </summary>
        public bool IsSet => _peer != null;
    }
}