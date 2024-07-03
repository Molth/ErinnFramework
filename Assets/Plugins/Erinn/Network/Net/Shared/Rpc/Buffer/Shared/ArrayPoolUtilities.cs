//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
#if !UNITY_2021_3_OR_NEWER
using System.Numerics;
#endif

// ReSharper disable RedundantExplicitArraySize

namespace Erinn
{
    /// <summary>
    ///     ArrayPool utilities
    /// </summary>
    internal static class ArrayPoolUtilities
    {
        /// <summary>
        ///     Select bucket index
        /// </summary>
        /// <param name="bufferSize">Buffer size</param>
        /// <returns>Bucket index</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SelectBucketIndex(int bufferSize)
        {
#if !UNITY_2021_3_OR_NEWER
            var value = (bufferSize - 1) | 15;
            return BitOperations.Log2(Unsafe.As<int, uint>(ref value)) - 3;
#else
            var value = (bufferSize - 1) | 15 | 1;
            var count = 0;
            if ((value & -65536) == 0)
            {
                count += 16;
                value <<= 16;
            }

            if ((value & -16777216) == 0)
            {
                count += 8;
                value <<= 8;
            }

            if ((value & -268435456) == 0)
            {
                count += 4;
                value <<= 4;
            }

            if ((value & -1073741824) == 0)
            {
                count += 2;
                value <<= 2;
            }

            if ((value & -2147483648) == 0)
                ++count;
            return (31 ^ count) - 3;
#endif
        }
    }
}