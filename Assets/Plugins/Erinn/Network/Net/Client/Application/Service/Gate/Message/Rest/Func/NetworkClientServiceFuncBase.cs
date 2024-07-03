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
    internal abstract class NetworkClientServiceFuncBase : INetworkClientServiceFunc
    {
        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public abstract ArraySegment<byte> Invoke(NetworkBuffer reader, NetworkBuffer writer);
    }
}