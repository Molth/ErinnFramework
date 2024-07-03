//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Writer Pool
    /// </summary>
    public static partial class NetworkWriterPool
    {
        /// <summary>
        ///     Pool
        /// </summary>
        private static readonly ConcurrentStack<NetworkWriter> WriterPool = new();

        /// <summary>
        ///     Generates
        /// </summary>
        private static readonly ConcurrentHashSet<int> Generates = new();

        /// <summary>
        ///     Catches
        /// </summary>
        private static readonly ConcurrentHashSet<int> Catches = new();

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Rent()
        {
            if (WriterPool.TryPop(out var writer))
            {
                Catches.Remove(writer.GetHashCode());
                return writer;
            }

            writer = new NetworkWriter(1024);
            Generates.Add(writer.GetHashCode());
            return writer;
        }

        /// <summary>
        ///     Recycle NetworkWriter
        /// </summary>
        /// <param name="writer">NetworkWriter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(in NetworkWriter writer)
        {
            var hashCode = writer.GetHashCode();
            if (!Generates.Contains(hashCode))
                throw new Exception("Writer is not from pool.");
            if (!Catches.Add(hashCode))
                throw new Exception("Writer is already in pool.");
            writer.Flush();
            WriterPool.Push(writer);
        }

        /// <summary>
        ///     Recycle NetworkWriter
        /// </summary>
        /// <param name="writer">NetworkWriter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool TryReturn(in NetworkWriter writer)
        {
            var hashCode = writer.GetHashCode();
            if (!Generates.Contains(hashCode))
                return false;
            if (!Catches.Add(hashCode))
                return false;
            writer.Flush();
            WriterPool.Push(writer);
            return true;
        }
    }
}