//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Processor interface
    /// </summary>
    public interface INetworkReaderWrapHandler
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        void Invoke(NetworkBuffer reader);
    }
}