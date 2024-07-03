//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
#if !UNITY_2021_3_OR_NEWER
using System.Numerics;
#endif

namespace Erinn
{
    /// <summary>
    ///     Utility methods for intrinsic bit-twiddling operations.
    ///     The methods use hardware intrinsics when available on the underlying platform,
    ///     otherwise they use optimized software fallbacks.
    /// </summary>
    public static class NetworkBitOperations
    {
        /// <summary>
        ///     Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPow2(int value) => (value & (value - 1)) == 0 && value > 0;

        /// <summary>
        ///     Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPow2(uint value) => IsPow2(Unsafe.As<uint, int>(ref value));

        /// <summary>
        ///     Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPow2(long value) => (value & (value - 1L)) == 0L && value > 0L;

        /// <summary>
        ///     Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPow2(ulong value) => IsPow2(Unsafe.As<ulong, long>(ref value));

        /// <summary>Round the given integral value up to a power of 2.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The smallest power of 2 which is greater than or equal to <paramref name="value" />.
        ///     If <paramref name="value" /> is 0 or the result overflows, returns 0.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RoundUpToPowerOf2(int value) => RoundUpToPowerOf2(Unsafe.As<int, uint>(ref value));

        /// <summary>Round the given integral value up to a power of 2.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The smallest power of 2 which is greater than or equal to <paramref name="value" />.
        ///     If <paramref name="value" /> is 0 or the result overflows, returns 0.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RoundUpToPowerOf2(uint value)
        {
            var shift = 32 - LeadingZeroCount(value - 1U);
            return (uint)((1 ^ (shift >> 5)) << shift);
        }

        /// <summary>
        ///     Round the given integral value up to a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The smallest power of 2 which is greater than or equal to <paramref name="value" />.
        ///     If <paramref name="value" /> is 0 or the result overflows, returns 0.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RoundUpToPowerOf2(long value) => RoundUpToPowerOf2(Unsafe.As<long, ulong>(ref value));

        /// <summary>
        ///     Round the given integral value up to a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The smallest power of 2 which is greater than or equal to <paramref name="value" />.
        ///     If <paramref name="value" /> is 0 or the result overflows, returns 0.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RoundUpToPowerOf2(ulong value)
        {
            var shift = 64 - LeadingZeroCount(value - 1UL);
            return (ulong)((1L ^ (shift >> 6)) << shift);
        }

        /// <summary>
        ///     Returns the number of leading zeros in the specified 32-bit signed integer.
        ///     Note that the method ensures at least one bit is set to avoid special handling for zero values.
        /// </summary>
        /// <param name="value">The 32-bit signed integer value.</param>
        /// <returns>The number of leading zeros in the specified value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeadingZeroCount(int value)
#if !UNITY_2021_3_OR_NEWER
            => BitOperations.LeadingZeroCount(Unsafe.As<int, uint>(ref value));
#else
        {
            var count = 0;
            if ((value & -65536) == 0)
            {
                count = 16;
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
            return count;
        }
#endif

        /// <summary>
        ///     Returns the number of leading zeros in the specified 32-bit unsigned integer.
        ///     Note that the method ensures at least one bit is set to avoid special handling for zero values.
        /// </summary>
        /// <param name="value">The 32-bit unsigned integer value.</param>
        /// <returns>The number of leading zeros in the specified value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeadingZeroCount(uint value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.LeadingZeroCount(value);
#else
            LeadingZeroCount(Unsafe.As<uint, int>(ref value));
#endif

        /// <summary>
        ///     Returns the number of leading zeros in the specified 64-bit signed integer.
        ///     Note that the method ensures at least one bit is set to avoid special handling for zero values.
        /// </summary>
        /// <param name="value">The 64-bit signed integer value.</param>
        /// <returns>The number of leading zeros in the specified value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeadingZeroCount(long value)
#if !UNITY_2021_3_OR_NEWER
            => BitOperations.LeadingZeroCount(Unsafe.As<long, ulong>(ref value));
#else
        {
            var count = 0;
            if ((value & -4294967296L) == 0L)
            {
                count = 32;
                value <<= 32;
            }

            if ((value & -281474976710656L) == 0L)
            {
                count += 16;
                value <<= 16;
            }

            if ((value & -72057594037927936L) == 0L)
            {
                count += 8;
                value <<= 8;
            }

            if ((value & -1152921504606846976L) == 0L)
            {
                count += 4;
                value <<= 4;
            }

            if ((value & -4611686018427387904L) == 0L)
            {
                count += 2;
                value <<= 2;
            }

            if ((value & -9223372036854775808) == 0L)
                ++count;
            return count;
        }
#endif

        /// <summary>
        ///     Returns the number of leading zeros in the specified 64-bit unsigned integer.
        ///     Note that the method ensures at least one bit is set to avoid special handling for zero values.
        /// </summary>
        /// <param name="value">The 64-bit unsigned integer value.</param>
        /// <returns>The number of leading zeros in the specified value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeadingZeroCount(ulong value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.LeadingZeroCount(value);
#else
            LeadingZeroCount(Unsafe.As<ulong, long>(ref value));
#endif

        /// <summary>
        ///     Returns the integer (floor) log of the specified value, base 2.
        ///     Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2(int value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.Log2(Unsafe.As<int, uint>(ref value));
#else
            31 ^ LeadingZeroCount(value | 1);
#endif

        /// <summary>
        ///     Returns the integer (floor) log of the specified value, base 2.
        ///     Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2(uint value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.Log2(value);
#else
            31 ^ LeadingZeroCount(value | 1);
#endif

        /// <summary>
        ///     Returns the integer (floor) log of the specified value, base 2.
        ///     Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2(long value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.Log2(Unsafe.As<long, ulong>(ref value));
#else
            63 ^ LeadingZeroCount(value | 1);
#endif

        /// <summary>
        ///     Returns the integer (floor) log of the specified value, base 2.
        ///     Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2(ulong value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.Log2(value);
#else
            63 ^ LeadingZeroCount(value | 1);
#endif

        /// <summary>
        ///     Returns the population count (number of bits set) of a mask.
        ///     Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(int value)
#if !UNITY_2021_3_OR_NEWER
            => PopCount(Unsafe.As<int, uint>(ref value));
#else
        {
            value -= (value >> 1) & 1431655765;
            value = (value & 858993459) + ((value >> 2) & 858993459);
            value = (((value + (value >> 4)) & 252645135) * 16843009) >> 24;
            return value;
        }
#endif

        /// <summary>
        ///     Returns the population count (number of bits set) of a mask.
        ///     Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(uint value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.PopCount(value);
#else
            PopCount(Unsafe.As<uint, int>(ref value));
#endif

        /// <summary>
        ///     Returns the population count (number of bits set) of a mask.
        ///     Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(long value)
#if !UNITY_2021_3_OR_NEWER
            => PopCount(Unsafe.As<long, ulong>(ref value));
#else
        {
            value -= (value >> 1) & 6148914691236517205L;
            value = (value & 3689348814741910323L) + ((value >> 2) & 3689348814741910323L);
            value = (((value + (value >> 4)) & 1085102592571150095L) * 72340172838076673L) >> 56;
            return (int)value;
        }
#endif

        /// <summary>
        ///     Returns the population count (number of bits set) of a mask.
        ///     Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(ulong value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.PopCount(value);
#else
            PopCount(Unsafe.As<ulong, long>(ref value));
#endif

        /// <summary>
        ///     Count the number of trailing zero bits in an integer value.
        ///     Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TrailingZeroCount(int value)
#if !UNITY_2021_3_OR_NEWER
            => BitOperations.TrailingZeroCount(Unsafe.As<int, uint>(ref value));
#else
        {
            var count = 0;
            if ((value & 65535) == 0)
            {
                count = 16;
                value >>= 16;
            }

            if ((value & 255) == 0)
            {
                count += 8;
                value >>= 8;
            }

            if ((value & 15) == 0)
            {
                count += 4;
                value >>= 4;
            }

            if ((value & 3) == 0)
            {
                count += 2;
                value >>= 2;
            }

            if ((value & 1) == 0)
                ++count;
            return count;
        }
#endif

        /// <summary>
        ///     Count the number of trailing zero bits in an integer value.
        ///     Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TrailingZeroCount(uint value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.TrailingZeroCount(value);
#else
            TrailingZeroCount(Unsafe.As<uint, int>(ref value));
#endif

        /// <summary>
        ///     Count the number of trailing zero bits in a mask.
        ///     Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TrailingZeroCount(long value)
#if !UNITY_2021_3_OR_NEWER
            => TrailingZeroCount(Unsafe.As<long, ulong>(ref value));
#else
        {
            var count = 0;
            if ((value & 4294967295L) == 0L)
            {
                count = 32;
                value >>= 32;
            }

            if ((value & 65535L) == 0L)
            {
                count += 16;
                value >>= 16;
            }

            if ((value & 255L) == 0L)
            {
                count += 8;
                value >>= 8;
            }

            if ((value & 15L) == 0L)
            {
                count += 4;
                value >>= 4;
            }

            if ((value & 3L) == 0L)
            {
                count += 2;
                value >>= 2;
            }

            if ((value & 1L) == 0L)
                ++count;
            return count;
        }
#endif

        /// <summary>
        ///     Count the number of trailing zero bits in a mask.
        ///     Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TrailingZeroCount(ulong value) =>
#if !UNITY_2021_3_OR_NEWER
            BitOperations.TrailingZeroCount(value);
#else
            TrailingZeroCount(Unsafe.As<ulong, long>(ref value));
#endif

        /// <summary>
        ///     Rotates the specified value left by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..31] is treated as congruent mod 32.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RotateLeft(int value, int offset) => RotateLeft(Unsafe.As<int, uint>(ref value), offset);

        /// <summary>
        ///     Rotates the specified value left by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..31] is treated as congruent mod 32.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RotateLeft(uint value, int offset) => (value << offset) | (value >> (32 - offset));

        /// <summary>
        ///     Rotates the specified value left by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..63] is treated as congruent mod 64.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RotateLeft(long value, int offset) => RotateLeft(Unsafe.As<long, ulong>(ref value), offset);

        /// <summary>
        ///     Rotates the specified value left by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..63] is treated as congruent mod 64.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RotateLeft(ulong value, int offset) => (value << offset) | (value >> (64 - offset));

        /// <summary>
        ///     Rotates the specified value right by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..31] is treated as congruent mod 32.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RotateRight(int value, int offset) => RotateRight(Unsafe.As<int, uint>(ref value), offset);

        /// <summary>
        ///     Rotates the specified value right by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..31] is treated as congruent mod 32.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RotateRight(uint value, int offset) => (value >> offset) | (value << (32 - offset));

        /// <summary>
        ///     Rotates the specified value right by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..63] is treated as congruent mod 64.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RotateRight(long value, int offset) => RotateRight(Unsafe.As<long, ulong>(ref value), offset);

        /// <summary>
        ///     Rotates the specified value right by the specified number of bits.
        ///     Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">
        ///     The number of bits to rotate by.
        ///     Any value outside the range [0..63] is treated as congruent mod 64.
        /// </param>
        /// <returns>The rotated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RotateRight(ulong value, int offset) => (value >> offset) | (value << (64 - offset));

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, byte data) =>
#if !UNITY_2021_3_OR_NEWER && !GODOT
            BitOperations.Crc32C(crc, data);
#else
            NetworkCrc32Fallback.Crc32C(crc, data);
#endif

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, ushort data) =>
#if !UNITY_2021_3_OR_NEWER && !GODOT
            BitOperations.Crc32C(crc, data);
#else
            NetworkCrc32Fallback.Crc32C(crc, data);
#endif

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, uint data) =>
#if !UNITY_2021_3_OR_NEWER && !GODOT
            BitOperations.Crc32C(crc, data);
#else
            NetworkCrc32Fallback.Crc32C(crc, data);
#endif

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, ulong data) =>
#if !UNITY_2021_3_OR_NEWER && !GODOT
            BitOperations.Crc32C(crc, data);
#else
            Crc32C(Crc32C(crc, (uint)data), (uint)(data >> 32));
#endif

        /// <summary>
        ///     Reset the lowest significant bit in the given value
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ResetLowestSetBit(int value) => ResetLowestSetBit(Unsafe.As<int, uint>(ref value));

        /// <summary>
        ///     Reset the lowest significant bit in the given value
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ResetLowestSetBit(uint value) => value & (value - 1U);

        /// <summary>
        ///     Reset specific bit in the given value
        ///     Reset the lowest significant bit in the given value
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ResetLowestSetBit(long value) => ResetLowestSetBit(Unsafe.As<long, ulong>(ref value));

        /// <summary>
        ///     Reset specific bit in the given value
        ///     Reset the lowest significant bit in the given value
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ResetLowestSetBit(ulong value) => value & (value - 1UL);

        /// <summary>
        ///     Flip the bit at a specific position in a given value.
        ///     Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">
        ///     The zero-based index of the bit to flip.
        ///     Any value outside the range [0..31] is treated as congruent mod 32.
        /// </param>
        /// <returns>The new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint FlipBit(int value, int index) => FlipBit(Unsafe.As<int, uint>(ref value), index);

        /// <summary>
        ///     Flip the bit at a specific position in a given value.
        ///     Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">
        ///     The zero-based index of the bit to flip.
        ///     Any value outside the range [0..31] is treated as congruent mod 32.
        /// </param>
        /// <returns>The new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint FlipBit(uint value, int index) => value ^ (uint)(1 << index);

        /// <summary>
        ///     Flip the bit at a specific position in a given value.
        ///     Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
        ///     ///
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">
        ///     The zero-based index of the bit to flip.
        ///     Any value outside the range [0..63] is treated as congruent mod 64.
        /// </param>
        /// <returns>The new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong FlipBit(long value, int index) => FlipBit(Unsafe.As<long, ulong>(ref value), index);

        /// <summary>
        ///     Flip the bit at a specific position in a given value.
        ///     Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
        ///     ///
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">
        ///     The zero-based index of the bit to flip.
        ///     Any value outside the range [0..63] is treated as congruent mod 64.
        /// </param>
        /// <returns>The new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong FlipBit(ulong value, int index) => value ^ (ulong)(1L << index);
    }
}