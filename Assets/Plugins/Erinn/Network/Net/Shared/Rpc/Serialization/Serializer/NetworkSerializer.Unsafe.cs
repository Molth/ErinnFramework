//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
// ReSharper disable PossibleNullReferenceException
#endif
using System.Runtime.CompilerServices;

#pragma warning disable CS8602
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static partial class NetworkSerializer
    {
        /// <summary>
        ///     Write int value
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="value">Value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write(byte[] destination, int value)
        {
            fixed (byte* ptr = &destination[0])
            {
                *(int*)ptr = value;
            }
        }

        /// <summary>
        ///     Write int value
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="offset">Offset</param>
        /// <param name="value">Value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write(byte[] destination, int offset, int value)
        {
            fixed (byte* ptr = &destination[offset])
            {
                *(int*)ptr = value;
            }
        }

        /// <summary>
        ///     Read int value
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Int value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Read(byte[] bytes)
        {
            fixed (byte* ptr = &bytes[0])
            {
                return *(int*)ptr;
            }
        }

        /// <summary>
        ///     Read int value
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <param name="offset">Offset</param>
        /// <returns>Int value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int Read(byte[] bytes, int offset)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                return *(int*)ptr;
            }
        }

        /// <summary>
        ///     Copy bytes
        /// </summary>
        /// <param name="src">Source</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dst">Destination</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BlockCopy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
            fixed (byte* destination = &dst[dstOffset])
            {
                fixed (byte* source = &src[srcOffset])
                {
                    var byteCount = count;
                    Unsafe.CopyBlock(destination, source, (uint)byteCount);
                }
            }
        }

        /// <summary>
        ///     Copy bytes
        /// </summary>
        /// <param name="src">Source</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dst">Destination</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopy(byte[] src, int srcOffset, in ArraySegment<byte> dst, int dstOffset, int count) => BlockCopy(src, srcOffset, dst.Array, dst.Offset + dstOffset, count);

        /// <summary>
        ///     Copy bytes
        /// </summary>
        /// <param name="src">Source</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dst">Destination</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopy(in ArraySegment<byte> src, int srcOffset, byte[] dst, int dstOffset, int count) => BlockCopy(src.Array, src.Offset + srcOffset, dst, dstOffset, count);

        /// <summary>
        ///     Copy bytes
        /// </summary>
        /// <param name="src">Source</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dst">Destination</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopy(in ArraySegment<byte> src, int srcOffset, in ArraySegment<byte> dst, int dstOffset, int count) => BlockCopy(src.Array, src.Offset + srcOffset, dst.Array, dst.Offset + dstOffset, count);
    }
}