//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
// ReSharper disable PossibleNullReferenceException
#endif
using System.Reflection;
using MemoryPack;

#pragma warning disable CS8604

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
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        /// <param name="isAllocated">Allocated</param>
        private void RegisterRoute<TRequest, TResponse>(MethodInfo methodInfo, bool isAllocated) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            var warpedHandler = (Func<TRequest, TResponse>)Delegate.CreateDelegate(typeof(Func<TRequest, TResponse>), this, methodInfo);
            _endPoint.RegisterFunc(warpedHandler, isAllocated);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <typeparam name="TRequest">Type</typeparam>
        private void UnregisterRoute<TRequest>() where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> => _endPoint.UnregisterFunc<TRequest>();

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="methodInfoBuffer">Method information buffer</param>
        /// <param name="registerMethodInfo">Add listening method information</param>
        /// <param name="methodInfo">Method</param>
        /// <param name="isAllocated">Allocated</param>
        private void BindRouteMethod(object[] methodInfoBuffer, MethodInfo registerMethodInfo, MethodInfo methodInfo, bool isAllocated)
        {
            methodInfoBuffer[0] = methodInfo;
            methodInfoBuffer[1] = isAllocated;
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            var returnType = methodInfo.ReturnParameter.ParameterType;
            registerMethodInfo.MakeGenericMethod(parameterType, returnType).Invoke(this, methodInfoBuffer);
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="unregisterMethodInfo">Remove listening method information</param>
        /// <param name="methodInfo">Method</param>
        private void FreeRouteMethod(MethodInfo unregisterMethodInfo, MethodInfo methodInfo)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            unregisterMethodInfo.MakeGenericMethod(parameterType).Invoke(this, null);
        }
    }
}