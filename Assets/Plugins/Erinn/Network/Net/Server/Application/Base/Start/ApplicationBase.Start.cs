//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Application base
    /// </summary>
    public abstract partial class ApplicationBase
    {
        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public bool Start(ushort port, uint maxClients = 4095U) => _peer.Start(port, maxClients);

        /// <summary>
        ///     Initialize server
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        public bool Initialize(NetworkProtocolType networkProtocolType) => _peer.Initialize(networkProtocolType);

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="configuration">Server Configuration</param>
        public bool Start(NetworkServerSetting configuration) => _peer.Start(configuration);

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public bool Start(NetworkProtocolType networkProtocolType, ushort port, uint maxClients = 4095U) => _peer.Start(networkProtocolType, port, maxClients);
    }
}