//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor interface
    /// </summary>
    internal interface INetworkClientServiceHandler
    {
        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        void Invoke(NetworkBuffer reader);
    }
}