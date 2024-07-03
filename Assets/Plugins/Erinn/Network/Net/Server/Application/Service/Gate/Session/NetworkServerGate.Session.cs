//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System.Collections.Generic;
#endif

#pragma warning disable CS8601
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Session Manager
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    internal sealed partial class NetworkServerGate<TS>
    {
        /// <summary>
        ///     Session
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions = new();

        /// <summary>
        ///     Call on connected
        /// </summary>
        /// <param name="id">ClientId</param>
        public void OnConnected(uint id)
        {
            var session = new TS();
            _sessions[id] = session;
            session.OnConnectedInternal(_networkServer, _endPoint, id);
        }

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        /// <param name="id">ClientId</param>
        public void OnDisconnected(uint id)
        {
            if (!_sessions.Remove(id, out var session))
                return;
            session.OnDisconnectedInternal();
        }
    }
}