//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor interface
    /// </summary>
    internal interface INetworkServerServiceHandler
    {
        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        void Invoke(uint id, NetworkBuffer reader);
    }
}