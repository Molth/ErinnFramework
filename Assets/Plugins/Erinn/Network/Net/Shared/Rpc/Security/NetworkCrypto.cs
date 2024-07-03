//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
#endif

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Erinn
{
    /// <summary>
    ///     Network secure
    /// </summary>
    public static class NetworkCrypto
    {
#if UNITY_2021_3_OR_NEWER || GODOT
        /// <summary>
        ///     Hash pool
        /// </summary>
        private static readonly ConcurrentStack<SHA1> HashPool = new();
#endif
        /// <summary>
        ///     Random number generator
        /// </summary>
        private static readonly RandomNumberGenerator SecureRandom = RandomNumberGenerator.Create();

        /// <summary>
        ///     Get hash string representation
        /// </summary>
        /// <param name="str">Value</param>
        /// <returns>Hash string representation</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SHA1ToBase64String(string str)
        {
            var count = Encoding.UTF8.GetByteCount(str);
            if (count > 64)
                throw new ArgumentOutOfRangeException(str);
            Span<byte> src = stackalloc byte[count];
            Encoding.UTF8.GetBytes(str, src);
            Span<byte> dst = stackalloc byte[20];
#if !UNITY_2021_3_OR_NEWER && !GODOT
            var hashCount = SHA1.HashData(src, dst);
#else
            if (!HashPool.TryPop(out var hashAlgorithm))
                hashAlgorithm = SHA1.Create();
            hashAlgorithm.TryComputeHash(src, dst, out var hashCount);
            HashPool.Push(hashAlgorithm);
#endif
            return Convert.ToBase64String(dst[..hashCount]);
        }

        /// <summary>
        ///     Get bytes
        /// </summary>
        /// <param name="cookie">Cookie</param>
        /// <returns>Bytes</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetBytes(uint cookie)
        {
            var bytes = new byte[4];
            Write(bytes, cookie);
            return bytes;
        }

        /// <summary>
        ///     Generate cookie
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Generate()
        {
            Span<byte> src = stackalloc byte[4];
            SecureRandom.GetBytes(src);
            return Read(src);
        }

        /// <summary>
        ///     Generate cookie
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<uint, uint> Generates()
        {
            Span<byte> src = stackalloc byte[4];
            SecureRandom.GetBytes(src);
            var key = Read(src);
            var value = NetworkHash.Hash32(src);
            return new KeyValuePair<uint, uint>(key, value);
        }

        /// <summary>
        ///     Generate cookie value
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetValue(uint key)
        {
            Span<byte> src = stackalloc byte[4];
            Write(src, key);
            var value = NetworkHash.Hash32(src);
            return value;
        }

        /// <summary>
        ///     Write cookie
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="cookie">Cookie</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write(byte[] destination, uint cookie)
        {
            fixed (byte* ptr = &destination[0])
            {
                *(uint*)ptr = cookie;
            }
        }

        /// <summary>
        ///     Write cookie
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="offset">Offset</param>
        /// <param name="cookie">Cookie</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write(byte[] destination, int offset, uint cookie)
        {
            fixed (byte* ptr = &destination[offset])
            {
                *(uint*)ptr = cookie;
            }
        }

        /// <summary>
        ///     Read cookie
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Cookie</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Read(byte[] bytes)
        {
            fixed (byte* ptr = &bytes[0])
            {
                return *(uint*)ptr;
            }
        }

        /// <summary>
        ///     Read cookie
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <param name="offset">Offset</param>
        /// <returns>Cookie</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Read(byte[] bytes, int offset)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                return *(uint*)ptr;
            }
        }

        /// <summary>
        ///     Write cookie
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="cookie">Cookie</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write(ReadOnlySpan<byte> destination, uint cookie)
        {
            fixed (byte* ptr = &destination[0])
            {
                *(uint*)ptr = cookie;
            }
        }

        /// <summary>
        ///     Write cookie
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="offset">Offset</param>
        /// <param name="cookie">Cookie</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write(ReadOnlySpan<byte> destination, int offset, uint cookie)
        {
            fixed (byte* ptr = &destination[offset])
            {
                *(uint*)ptr = cookie;
            }
        }

        /// <summary>
        ///     Read cookie
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Cookie</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Read(ReadOnlySpan<byte> bytes)
        {
            fixed (byte* ptr = &bytes[0])
            {
                return *(uint*)ptr;
            }
        }

        /// <summary>
        ///     Read cookie
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <param name="offset">Offset</param>
        /// <returns>Cookie</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Read(ReadOnlySpan<byte> bytes, int offset)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                return *(uint*)ptr;
            }
        }
    }
}