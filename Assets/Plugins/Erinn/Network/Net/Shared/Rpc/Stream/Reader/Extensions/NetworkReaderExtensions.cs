//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Erinn
{
    /// <summary>
    ///     NetworkReader extensions
    /// </summary>
    public static class NetworkReaderExtensions
    {
        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0>(this NetworkReader reader, out T0 arg0) => arg0 = reader.Read<T0>();

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1>(this NetworkReader reader, out T0 arg0, out T1 arg1)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9, out T10 arg10)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
            arg10 = reader.Read<T10>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9, out T10 arg10, out T11 arg11)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
            arg10 = reader.Read<T10>();
            arg11 = reader.Read<T11>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9, out T10 arg10, out T11 arg11, out T12 arg12)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
            arg10 = reader.Read<T10>();
            arg11 = reader.Read<T11>();
            arg12 = reader.Read<T12>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9, out T10 arg10, out T11 arg11, out T12 arg12, out T13 arg13)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
            arg10 = reader.Read<T10>();
            arg11 = reader.Read<T11>();
            arg12 = reader.Read<T12>();
            arg13 = reader.Read<T13>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9, out T10 arg10, out T11 arg11, out T12 arg12, out T13 arg13, out T14 arg14)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
            arg10 = reader.Read<T10>();
            arg11 = reader.Read<T11>();
            arg12 = reader.Read<T12>();
            arg13 = reader.Read<T13>();
            arg14 = reader.Read<T14>();
        }

        /// <summary>
        ///     Read
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this NetworkReader reader, out T0 arg0, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, out T5 arg5, out T6 arg6, out T7 arg7, out T8 arg8, out T9 arg9, out T10 arg10, out T11 arg11, out T12 arg12, out T13 arg13, out T14 arg14, out T15 arg15)
        {
            arg0 = reader.Read<T0>();
            arg1 = reader.Read<T1>();
            arg2 = reader.Read<T2>();
            arg3 = reader.Read<T3>();
            arg4 = reader.Read<T4>();
            arg5 = reader.Read<T5>();
            arg6 = reader.Read<T6>();
            arg7 = reader.Read<T7>();
            arg8 = reader.Read<T8>();
            arg9 = reader.Read<T9>();
            arg10 = reader.Read<T10>();
            arg11 = reader.Read<T11>();
            arg12 = reader.Read<T12>();
            arg13 = reader.Read<T13>();
            arg14 = reader.Read<T14>();
            arg15 = reader.Read<T15>();
        }
    }
}