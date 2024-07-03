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
    ///     MessageProcessor
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    /// <typeparam name="T">Type</typeparam>
    internal sealed class NetworkServerGateProcessor<TS, T> : INetworkServerGateProcessor<TS, T> where TS : Service, new() where T : struct, INetworkMessage, IMemoryPackable<T>
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Handler
        /// </summary>
        private readonly Action<TS, T> _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="handler">Handler</param>
        public NetworkServerGateProcessor(Dictionary<uint, TS> sessions, Action<TS, T> handler)
        {
            _sessions = sessions;
            _handler = handler;
        }

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">Data</param>
        public void Invoke(uint id, T obj)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return;
            _handler.Invoke(session, obj);
        }
    }
}