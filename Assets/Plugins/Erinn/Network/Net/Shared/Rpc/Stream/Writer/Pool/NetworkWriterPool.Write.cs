//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Erinn
{
    /// <summary>
    ///     Writer Pool
    /// </summary>
    public static partial class NetworkWriterPool
    {
        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0>(in T0 arg0)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1>(in T0 arg0, in T1 arg1)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2>(in T0 arg0, in T1 arg1, in T2 arg2)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12, in T13 arg13)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12, in T13 arg13, in T14 arg14)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13, in arg14);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkWriter Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12, in T13 arg13, in T14 arg14, in T15 arg15)
        {
            var writer = Rent();
            try
            {
                writer.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13, in arg14, in arg15);
            }
            catch
            {
                Return(writer);
                throw;
            }

            return writer;
        }
    }
}