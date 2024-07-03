//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Processor interface
    /// </summary>
    public interface INetworkReaderWrapOutsideHandler<in T0>
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="reader">NetworkReader</param>
        void Invoke(T0 target, NetworkBuffer reader);
    }
}