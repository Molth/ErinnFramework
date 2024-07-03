//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public abstract partial class NetworkClientPeer
    {
        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">ServerIpAddress</param>
        /// <param name="port">Server Port</param>
        public abstract bool Start(string ipAddress, ushort port);

        /// <summary>
        ///     Called when starting the client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        protected void OnStart(string ipAddress, ushort port)
        {
            Log.Info($"Client startup: Address[{ipAddress}] Port[{port}] Protocol[{ProtocolType}]");
            StartPolling();
        }
    }
}