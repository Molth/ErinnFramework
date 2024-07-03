//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public abstract partial class NetworkServerPeer
    {
        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public abstract bool Start(ushort port, uint maxClients);

        /// <summary>
        ///     Called when starting the server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        protected void OnStart(ushort port, uint maxClients)
        {
            var capacity = (int)maxClients;
            _blacklist.EnsureCapacity(capacity);
            ReadBlacklist();
            _connections.EnsureCapacity(capacity);
            Log.Info($"Server startup: Port[{port}] MaxClients[{maxClients}]] Protocol[{ProtocolType}]");
            StartPolling();
        }
    }
}