//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     NetworkMessage extensions
    /// </summary>
    public static class NetworkMessageExtensions
    {
        /// <summary>
        ///     Release
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Release<T>(this T obj) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return;
            NetworkMessagePool<T>.Return(in obj);
        }
    }
}