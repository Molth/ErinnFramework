//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif
using System.Runtime.CompilerServices;

#pragma warning disable CS8632

// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Network array pool
    /// </summary>
    public static class NetworkBytesPool
    {
        /// <summary>
        ///     Buckets
        /// </summary>
        private static readonly NetworkBytesPoolBucket[] Buckets;

        /// <summary>
        ///     Structure
        /// </summary>
        static NetworkBytesPool()
        {
            var buckets = new NetworkBytesPoolBucket[7];
            for (var i = 0; i < 7; ++i)
                buckets[i] = new NetworkBytesPoolBucket(16 << i);
            Buckets = buckets;
        }

        /// <summary>
        ///     Rent array
        /// </summary>
        /// <param name="minimumLength">Minimum array length</param>
        /// <returns>Array</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Rent(int minimumLength)
        {
            if (minimumLength < 0)
                throw new InvalidOperationException(nameof(minimumLength));
            if (minimumLength == 0)
                return Array.Empty<byte>();
            var index = SelectBucketIndex(minimumLength);
            return index < 7 ? Buckets[index].Rent() : new byte[minimumLength];
        }

        /// <summary>
        ///     Return array
        /// </summary>
        /// <param name="array">Array</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(byte[] array)
        {
            if (array == null)
                return;
            var length = array.Length;
            if (length == 0)
                return;
            var bucket = SelectBucketIndex(length);
            var haveBucket = bucket < 7;
            if (!haveBucket)
                return;
            Buckets[bucket].Return(length, array);
        }

        /// <summary>
        ///     Select bucket index
        /// </summary>
        /// <param name="bufferSize">Buffer size</param>
        /// <returns>Bucket index</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int SelectBucketIndex(int bufferSize) => ArrayPoolUtilities.SelectBucketIndex(bufferSize);
    }
}