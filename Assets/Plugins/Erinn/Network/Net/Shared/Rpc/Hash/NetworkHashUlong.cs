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
        ///     Obtain Hash64
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(byte[] buffer)
        {
            unsafe
            {
                fixed (byte* pointer = buffer)
                {
                    return Hash64(pointer, buffer.Length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(byte[] buffer, int length)
        {
            unsafe
            {
                fixed (byte* pointer = buffer)
                {
                    return Hash64(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(byte[] buffer, int offset, int length)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[offset])
                {
                    return Hash64(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(ReadOnlySpan<byte> buffer)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[0])
                {
                    return Hash64(pointer, buffer.Length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(ReadOnlySpan<byte> buffer, int length)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[0])
                {
                    return Hash64(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="buffer">Buffer zone</param>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(ReadOnlySpan<byte> buffer, int offset, int length)
        {
            unsafe
            {
                fixed (byte* pointer = &buffer[offset])
                {
                    return Hash64(pointer, length);
                }
            }
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(string text)
        {
            var count = Encoding.UTF8.GetByteCount(text);
            Span<byte> span = stackalloc byte[count];
            Encoding.UTF8.GetBytes(text, span);
            return Hash64(span, count);
        }

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64(Type type) => Hash64(type.FullName);

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hash64<T>() => Hash64(typeof(T).FullName);

        /// <summary>
        ///     Obtain Hash64
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="length">Length</param>
        /// <param name="seed">Seed</param>
        /// <returns>Obtained Hash64</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong Hash64(byte* input, int length, ulong seed = 0UL)
        {
            var num1 = seed + 2870177450012600261UL;
            if (length >= 32)
            {
                var num2 = (ulong)((long)seed + -7046029288634856825L + -4417276706812531889L);
                var num3 = seed + 14029467366897019727UL;
                var num4 = seed;
                var num5 = seed - 11400714785074694791UL;
                var num6 = length >> 5;
                for (var index = 0; index < num6; ++index)
                {
                    var num7 = (ulong)*(long*)input;
                    var num8 = (ulong)*(long*)(input + 8);
                    var num9 = (ulong)*(long*)(input + 16);
                    var num10 = (ulong)*(long*)(input + 24);
                    var num11 = num2 + num7 * 14029467366897019727UL;
                    num2 = ((num11 << 31) | (num11 >> 33)) * 11400714785074694791UL;
                    var num12 = num3 + num8 * 14029467366897019727UL;
                    num3 = ((num12 << 31) | (num12 >> 33)) * 11400714785074694791UL;
                    var num13 = num4 + num9 * 14029467366897019727UL;
                    num4 = ((num13 << 31) | (num13 >> 33)) * 11400714785074694791UL;
                    var num14 = num5 + num10 * 14029467366897019727UL;
                    num5 = ((num14 << 31) | (num14 >> 33)) * 11400714785074694791UL;
                    input += 32;
                }

                var num15 = (ulong)((((long)num2 << 1) | (long)(num2 >> 63)) + (((long)num3 << 7) | (long)(num3 >> 57)) + (((long)num4 << 12) | (long)(num4 >> 52)) + (((long)num5 << 18) | (long)(num5 >> 46)));
                var num16 = num2 * 14029467366897019727UL;
                var num17 = ((num16 << 31) | (num16 >> 33)) * 11400714785074694791UL;
                var num18 = (ulong)((long)(num15 ^ num17) * -7046029288634856825L + -8796714831421723037L);
                var num19 = num3 * 14029467366897019727UL;
                var num20 = ((num19 << 31) | (num19 >> 33)) * 11400714785074694791UL;
                var num21 = (ulong)((long)(num18 ^ num20) * -7046029288634856825L + -8796714831421723037L);
                var num22 = num4 * 14029467366897019727UL;
                var num23 = ((num22 << 31) | (num22 >> 33)) * 11400714785074694791UL;
                var num24 = (ulong)((long)(num21 ^ num23) * -7046029288634856825L + -8796714831421723037L);
                var num25 = num5 * 14029467366897019727UL;
                var num26 = ((num25 << 31) | (num25 >> 33)) * 11400714785074694791UL;
                num1 = (ulong)((long)(num24 ^ num26) * -7046029288634856825L + -8796714831421723037L);
            }

            var num27 = num1 + (ulong)length;
            for (length &= 31; length >= 8; length -= 8)
            {
                var num28 = (ulong)(*(long*)input * -4417276706812531889L);
                var num29 = (ulong)((((long)num28 << 31) | (long)(num28 >> 33)) * -7046029288634856825L);
                var num30 = num27 ^ num29;
                num27 = (ulong)((((long)num30 << 27) | (long)(num30 >> 37)) * -7046029288634856825L + -8796714831421723037L);
                input += 8;
            }

            if (length >= 4)
            {
                var num31 = num27 ^ (*(uint*)input * 11400714785074694791UL);
                num27 = (ulong)((((long)num31 << 23) | (long)(num31 >> 41)) * -4417276706812531889L + 1609587929392839161L);
                input += 4;
                length -= 4;
            }

            for (; length > 0; --length)
            {
                var num32 = num27 ^ (*input * 2870177450012600261UL);
                num27 = (ulong)((((long)num32 << 11) | (long)(num32 >> 53)) * -7046029288634856825L);
                ++input;
            }

            var num33 = (num27 ^ (num27 >> 33)) * 14029467366897019727UL;
            var num34 = (num33 ^ (num33 >> 29)) * 1609587929392839161UL;
            return num34 ^ (num34 >> 32);
        }
    }
}