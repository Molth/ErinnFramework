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
    ///     MessageProcessor interface
    /// </summary>
    internal interface INetworkClientMessageProcessor : IDisposable
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="bytes">Payload</param>
        void Invoke(in ArraySegment<byte> bytes);
    }
}