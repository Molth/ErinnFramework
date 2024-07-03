//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     Network Hash
    /// </summary>
    public static partial class NetworkHash
    {
        /// <summary>
        ///     Obtain Id32
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained Id32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetId32<T>() where T : struct, INetworkMessage, IMemoryPackable<T> => NetworkHashCache<T>.Id32;
    }
}