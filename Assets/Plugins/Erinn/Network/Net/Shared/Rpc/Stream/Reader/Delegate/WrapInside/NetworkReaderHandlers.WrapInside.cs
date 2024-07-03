//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Read
    /// </summary>
    public static partial class NetworkReaderHandlers
    {
        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1>)Delegate.CreateDelegate(typeof(Action<T1>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2>)Delegate.CreateDelegate(typeof(Action<T1, T2>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapInsideHandlerBase<T0> WrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(MethodInfo methodInfo) where T1 : T0
        {
            var action = (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>), methodInfo);
            return new NetworkReaderWrapInsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(action);
        }
    }
}