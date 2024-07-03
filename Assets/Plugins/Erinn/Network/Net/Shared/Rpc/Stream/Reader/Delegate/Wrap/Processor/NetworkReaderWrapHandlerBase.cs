//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Processor
    /// </summary>
    public abstract class NetworkReaderWrapHandlerBase : INetworkReaderWrapHandler
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        public abstract void Invoke(NetworkBuffer reader);
    }
}