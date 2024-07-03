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
    ///     Reader Pool
    /// </summary>
    public static class NetworkReaderPool
    {
        /// <summary>
        ///     Pool
        /// </summary>
        private static readonly ConcurrentStack<NetworkReader> ReaderPool = new();

        /// <summary>
        ///     Generates
        /// </summary>
        private static readonly ConcurrentHashSet<int> Generates = new();

        /// <summary>
        ///     Catches
        /// </summary>
        private static readonly ConcurrentHashSet<int> Catches = new();

        /// <summary>
        ///     Obtain NetworkReader
        /// </summary>
        /// <param name="segment">Data sharding</param>
        /// <returns>Obtained NetworkReader</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkReader Rent(in ArraySegment<byte> segment)
        {
            if (ReaderPool.TryPop(out var reader))
            {
                reader.SetBuffer(segment);
                reader.SetPosition(segment.Count);
                Catches.Remove(reader.GetHashCode());
                return reader;
            }

            reader = new NetworkReader(segment);
            reader.SetPosition(segment.Count);
            Generates.Add(reader.GetHashCode());
            return reader;
        }

        /// <summary>
        ///     Recycle NetworkReader
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(in NetworkReader reader)
        {
            var hashCode = reader.GetHashCode();
            if (!Generates.Contains(hashCode))
                throw new Exception("Reader is not from pool.");
            if (!Catches.Add(hashCode))
                throw new Exception("Reader is already in pool.");
            reader.Flush();
            ReaderPool.Push(reader);
        }

        /// <summary>
        ///     Recycle NetworkReader
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool TryReturn(in NetworkReader reader)
        {
            var hashCode = reader.GetHashCode();
            if (!Generates.Contains(hashCode))
                return false;
            if (!Catches.Add(hashCode))
                return false;
            reader.Flush();
            ReaderPool.Push(reader);
            return true;
        }
    }
}