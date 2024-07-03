//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using System.Text;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Network Hash
    /// </summary>
    public static partial class NetworkHash
    {
        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(byte[] buffer)
        {
            unsafe
            {
                fixed (byte* pointer = buffer)
                {
                    return Hash32(pointer, buffer.Length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(byte[] buffer, int length)
        {
            unsafe
            {
                fixed (byte* pointer = buffer)
                {
                    return Hash32(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(byte[] buffer, int offset, int length)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[offset])
                {
                    return Hash32(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(ReadOnlySpan<byte> buffer)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[0])
                {
                    return Hash32(pointer, buffer.Length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(ReadOnlySpan<byte> buffer, int length)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[0])
                {
                    return Hash32(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(ReadOnlySpan<byte> buffer, int offset, int length)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[offset])
                {
                    return Hash32(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(string text)
        {
            var count = Encoding.UTF8.GetByteCount(text);
            Span<byte> span = stackalloc byte[count];
            Encoding.UTF8.GetBytes(text, span);
            return Hash32(span, count);
        }

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(Type type) => Hash32(type.FullName);

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32<T>() => Hash32(typeof(T).FullName);

        /// <summary>
        ///     Obtain Hash32
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="length">Length</param>
        /// <param name="seed">Seed</param>
        /// <returns>Obtained Hash32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe uint Hash32(byte* input, int length, uint seed = 0U)
        {
            var num1 = seed + 374761393U;
            if (length >= 16)
            {
                var num2 = (uint)((int)seed - 1640531535 - 2048144777);
                var num3 = seed + 2246822519U;
                var num4 = seed;
                var num5 = seed - 2654435761U;
                var num6 = length >> 4;
                for (var index = 0; index < num6; ++index)
                {
                    var num7 = *(uint*)input;
                    var num8 = *(uint*)(input + 4);
                    var num9 = *(uint*)(input + 8);
                    var num10 = *(uint*)(input + 12);
                    var num11 = num2 + num7 * 2246822519U;
                    num2 = ((num11 << 13) | (num11 >> 19)) * 2654435761U;
                    var num12 = num3 + num8 * 2246822519U;
                    num3 = ((num12 << 13) | (num12 >> 19)) * 2654435761U;
                    var num13 = num4 + num9 * 2246822519U;
                    num4 = ((num13 << 13) | (num13 >> 19)) * 2654435761U;
                    var num14 = num5 + num10 * 2246822519U;
                    num5 = ((num14 << 13) | (num14 >> 19)) * 2654435761U;
                    input += 16;
                }

                num1 = (uint)((((int)num2 << 1) | (int)(num2 >> 31)) + (((int)num3 << 7) | (int)(num3 >> 25)) + (((int)num4 << 12) | (int)(num4 >> 20)) + (((int)num5 << 18) | (int)(num5 >> 14)));
            }

            var num15 = num1 + (uint)length;
            for (length &= 15; length >= 4; length -= 4)
            {
                var num16 = num15 + *(uint*)input * 3266489917U;
                num15 = (uint)((((int)num16 << 17) | (int)(num16 >> 15)) * 668265263);
                input += 4;
            }

            for (; length > 0; --length)
            {
                var num17 = num15 + *input * 374761393U;
                num15 = (uint)((((int)num17 << 11) | (int)(num17 >> 21)) * -1640531535);
                ++input;
            }

            var num18 = (num15 ^ (num15 >> 15)) * 2246822519U;
            var num19 = (num18 ^ (num18 >> 13)) * 3266489917U;
            return num19 ^ (num19 >> 16);
        }
    }
}