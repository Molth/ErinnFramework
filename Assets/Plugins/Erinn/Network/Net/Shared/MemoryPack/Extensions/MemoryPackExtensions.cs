//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static class MemoryPackExtensions
    {
        /// <summary>
        ///     Read bytes
        /// </summary>
        /// <param name="reader">MemoryPackReader</param>
        /// <param name="payload">bytes</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadBytes(this MemoryPackReader reader,
#if !UNITY_2021_3_OR_NEWER && !GODOT
            scoped
#endif
                ref ArraySegment<byte> payload)
        {
            var array = payload.Array;
            if (!reader.TryReadCollectionHeader(out var length) || length == 0)
            {
                payload = array != null ? new ArraySegment<byte>(array, 0, 0) : new ArraySegment<byte>();
            }
            else
            {
                ref var local = ref reader.GetSpanReference(length);
                if (array == null || array.Length < length)
                {
#if !UNITY_2021_3_OR_NEWER && !GODOT
                    array = GC.AllocateUninitializedArray<byte>(length);
#else
                    array = new byte[length];
#endif
                }

                Unsafe.CopyBlock(ref array[0], ref local, Unsafe.As<int, uint>(ref length));
                payload = new ArraySegment<byte>(array, 0, length);
                reader.Advance(length);
            }
        }
    }
}