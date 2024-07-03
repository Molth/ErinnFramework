//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Service Session
    /// </summary>
    public abstract partial class Session
    {
        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="methodInfo">Method information</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="T">Type</typeparam>
        private void RegisterHandler<T>(MethodInfo methodInfo, bool isAllocated) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var warpedHandler = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), this, methodInfo);
            _networkClient.RegisterHandler(warpedHandler, isAllocated);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        private void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T> => _networkClient.UnregisterHandler<T>();

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
    }
}