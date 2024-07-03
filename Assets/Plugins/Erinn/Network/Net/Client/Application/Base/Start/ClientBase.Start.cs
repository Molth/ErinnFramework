//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Client Application base
    /// </summary>
    public abstract partial class ClientBase
    {
        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public bool Start(string ipAddress, ushort port) => _peer.Start(ipAddress, port);

        /// <summary>
        ///     Initialize client
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        public bool Initialize(NetworkProtocolType networkProtocolType) => _peer.Initialize(networkProtocolType);

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="configuration">Client Configuration</param>
        public bool Start(NetworkClientSetting configuration) => _peer.Start(configuration);

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public bool Start(NetworkProtocolType networkProtocolType, string ipAddress, ushort port) => _peer.Start(networkProtocolType, ipAddress, port);
    }
}