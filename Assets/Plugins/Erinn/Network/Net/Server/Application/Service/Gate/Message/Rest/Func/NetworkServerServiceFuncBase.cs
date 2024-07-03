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
    internal abstract class NetworkServerServiceFuncBase : INetworkServerServiceFunc
    {
        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public abstract ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer);
    }
}