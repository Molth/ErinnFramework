//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

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
        public static NetworkReaderWrapHandlerBase WrapHandler(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action)Delegate.CreateDelegate(typeof(Action), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0>)Delegate.CreateDelegate(typeof(Action<T0>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1>)Delegate.CreateDelegate(typeof(Action<T0, T1>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(action);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <returns>Handler obtained</returns>
        public static NetworkReaderWrapHandlerBase WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(object firstArgument, MethodInfo methodInfo)
        {
            var action = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>), firstArgument, methodInfo);
            return new NetworkReaderWrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(action);
        }
    }
}