//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal abstract class NetworkServerServiceHandlerBase : INetworkServerServiceHandler
    {
        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        public abstract void Invoke(uint id, NetworkBuffer reader);
    }
}