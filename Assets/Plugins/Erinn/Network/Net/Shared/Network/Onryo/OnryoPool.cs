//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Onryo Buffer pool
    /// </summary>
    public static class OnryoPool
    {
        /// <summary>
        ///     Buffer pool
        /// </summary>
        private static readonly ConcurrentStack<byte[]> BufferPool = new();

        /// <summary>
        ///     Buffer hashCodes
        /// </summary>
        private static readonly ConcurrentHashSet<int> BufferGenerates = new();

        /// <summary>
        ///     Buffer catches
        /// </summary>
        private static readonly ConcurrentHashSet<int> BufferCatches = new();

        /// <summary>
        ///     Buffer pool
        /// </summary>
        private static readonly ConcurrentStack<byte[]> HeaderPool = new();

        /// <summary>
        ///     Header hashCodes
        /// </summary>
        private static readonly ConcurrentHashSet<int> HeaderGenerates = new();

        /// <summary>
        ///     Header catches
        /// </summary>
        private static readonly ConcurrentHashSet<int> HeaderCatches = new();

        /// <summary>
        ///     Rent buffer
        /// </summary>
        /// <returns>Obtained buffer</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Rent()
        {
            if (BufferPool.TryPop(out var buffer))
            {
                BufferCatches.Remove(buffer.GetHashCode());
                return buffer;
            }

            buffer = new byte[1024];
            BufferGenerates.Add(buffer.GetHashCode());
            return buffer;
        }

        /// <summary>
        ///     Return buffer
        /// </summary>
        /// <param name="buffer">Buffer</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(byte[] buffer)
        {
            var hashCode = buffer.GetHashCode();
            if (!BufferGenerates.Contains(hashCode))
                return;
            if (!BufferCatches.Add(hashCode))
                return;
            BufferPool.Push(buffer);
        }

        /// <summary>
        ///     Rent buffer
        /// </summary>
        /// <returns>Obtained buffer</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] RentHeader()
        {
            if (HeaderPool.TryPop(out var buffer))
            {
                HeaderCatches.Remove(buffer.GetHashCode());
                return buffer;
            }

            buffer = new byte[4];
            HeaderGenerates.Add(buffer.GetHashCode());
            return buffer;
        }

        /// <summary>
        ///     Return buffer
        /// </summary>
        /// <param name="buffer">Buffer</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnHeader(byte[] buffer)
        {
            var hashCode = buffer.GetHashCode();
            if (!HeaderGenerates.Contains(hashCode))
                return;
            if (!HeaderCatches.Add(hashCode))
                return;
            HeaderPool.Push(buffer);
        }
    }
}