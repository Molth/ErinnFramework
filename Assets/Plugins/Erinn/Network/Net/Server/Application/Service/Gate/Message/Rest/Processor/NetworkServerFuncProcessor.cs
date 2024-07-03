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
    /// <typeparam name="TRequest">Type</typeparam>
    /// <typeparam name="TResponse">Type</typeparam>
    internal sealed class NetworkServerFuncProcessor<TS, TRequest, TResponse> : INetworkServerFuncProcessor<TRequest, TResponse> where TS : Service, new() where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Handler
        /// </summary>
        private readonly Func<TS, TRequest, TResponse> _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="handler">Handler</param>
        public NetworkServerFuncProcessor(Dictionary<uint, TS> sessions, Func<TS, TRequest, TResponse> handler)
        {
            _sessions = sessions;
            _handler = handler;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">Request</param>
        public TResponse Invoke(uint id, TRequest obj) => !_sessions.TryGetValue(id, out var session) ? default : _handler.Invoke(session, obj);
    }
}