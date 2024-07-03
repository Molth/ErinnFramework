//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Processor
    /// </summary>
    public abstract class NetworkReaderWrapInsideHandlerBase<T0> : INetworkReaderWrapInsideHandler<T0>
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="reader">NetworkReader</param>
        public abstract void Invoke(T0 target, NetworkBuffer reader);
    }
}