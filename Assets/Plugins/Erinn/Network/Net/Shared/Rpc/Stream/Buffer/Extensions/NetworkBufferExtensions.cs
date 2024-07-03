//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     NetworkBuffer extensions
    /// </summary>
    public static class NetworkBufferExtensions
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Serialize(this INetworkSerialize networkSerialize)
        {
            var writer = NetworkWriterPool.Rent();
            try
            {
                networkSerialize.Serialize(writer);
                return writer.ToArray();
            }
            finally
            {
                NetworkWriterPool.Return(writer);
            }
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deserialize(this INetworkSerialize networkSerialize, ArraySegment<byte> segment)
        {
            var reader = NetworkReaderPool.Rent(segment);
            try
            {
                networkSerialize.Deserialize(reader);
            }
            finally
            {
                NetworkReaderPool.Return(reader);
            }
        }
    }
}