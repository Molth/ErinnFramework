//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static partial class NetworkSerializer
    {
        /// <summary>
        ///     Network buffer
        /// </summary>
        [ThreadStatic] private static NetworkBuffer? _networkBuffer;

        /// <summary>
        ///     Network writer
        /// </summary>
        [ThreadStatic] private static NetworkBuffer? _networkWriter;
    }
}