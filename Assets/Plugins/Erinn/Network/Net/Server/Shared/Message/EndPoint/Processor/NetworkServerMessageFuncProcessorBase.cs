//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal abstract class NetworkServerMessageFuncProcessorBase : INetworkServerMessageFuncProcessor
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="bytes">Payload</param>
        public abstract void Invoke(uint id, uint serialNumber, in ArraySegment<byte> bytes);

        /// <summary>
        ///     Release
        /// </summary>
        public abstract void Dispose();
    }
}