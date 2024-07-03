//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System.Collections.Generic;
#endif

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    internal sealed class NetworkServerServiceHandler<TS> : NetworkServerServiceHandlerBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Handler
        /// </summary>
        private readonly NetworkReaderWrapOutsideHandlerBase<TS> _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="handler">Handler</param>
        public NetworkServerServiceHandler(Dictionary<uint, TS> sessions, NetworkReaderWrapOutsideHandlerBase<TS> handler)
        {
            _sessions = sessions;
            _handler = handler;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        public override void Invoke(uint id, NetworkBuffer reader)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return;
            _handler.Invoke(session, reader);
        }
    }
}