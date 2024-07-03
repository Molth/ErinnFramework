//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8601
#pragma warning disable CS8604
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Session Manager
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    internal sealed partial class NetworkServerGate<TS>
    {
        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="methodInfo">Method information</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="T">Type</typeparam>
        private void RegisterHandler<T>(MethodInfo methodInfo, bool isAllocated) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var warpedHandler1 = (Action<TS, T>)Delegate.CreateDelegate(typeof(Action<TS, T>), methodInfo);
            var warpedHandler2 = WrapHandler(warpedHandler1);
            _networkServer.RegisterHandler(warpedHandler2, isAllocated);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        private void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T> => _networkServer.UnregisterHandler<T>();

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="methodInfoBuffer">Method information buffer</param>
        /// <param name="registerMethodInfo">Add listening method information</param>
        /// <param name="methodInfo">Method</param>
        /// <param name="isAllocated">Allocated</param>
        private void BindMethod(object[] methodInfoBuffer, MethodInfo registerMethodInfo, MethodInfo methodInfo, bool isAllocated)
        {
            methodInfoBuffer[0] = methodInfo;
            methodInfoBuffer[1] = isAllocated;
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            registerMethodInfo.MakeGenericMethod(parameterType).Invoke(this, methodInfoBuffer);
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="unregisterMethodInfo">Remove listening method information</param>
        /// <param name="methodInfo">Method</param>
        private void FreeMethod(MethodInfo unregisterMethodInfo, MethodInfo methodInfo)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            unregisterMethodInfo.MakeGenericMethod(parameterType).Invoke(this, null);
        }

        /// <summary>
        ///     Get handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Handler obtained</returns>
        private Action<uint, T> WrapHandler<T>(Action<TS, T> handler) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var warpedHandler = new NetworkServerGateProcessor<TS, T>(_sessions, handler);
            return warpedHandler.Invoke;
        }
    }
}