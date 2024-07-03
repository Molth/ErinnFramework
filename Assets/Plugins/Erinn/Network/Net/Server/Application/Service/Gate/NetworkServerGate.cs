//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Session Manager
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    internal sealed partial class NetworkServerGate<TS> : INetworkServerGate where TS : Service, new()
    {
        /// <summary>
        ///     Network server
        /// </summary>
        private readonly NetworkServer _networkServer;

        /// <summary>
        ///     EndPoint
        /// </summary>
        private readonly NetworkServerMessageEndPoint _endPoint;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkServer">Network server</param>
        /// <param name="endPoint">EndPoint</param>
        public NetworkServerGate(NetworkServer networkServer, NetworkServerMessageEndPoint endPoint)
        {
            _networkServer = networkServer;
            _endPoint = endPoint;
        }
    }
}