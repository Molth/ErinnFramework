//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8604

// ReSharper disable RedundantArrayCreationExpression
// ReSharper disable UseCollectionExpression
// ReSharper disable NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract

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
        private void RegisterHandlers(Type type, Action<uint, NetworkClientServiceHandler> addHandlerCallback, Action<uint, NetworkClientServiceFuncBase> addFuncCallback)
        {
            var notifyRpcMethodInfoBuffer = new object[2];
            var registerNotifyRpcMethodInfo = typeof(Session).GetMethod(nameof(RegisterHandler), BindingFlags.Instance | BindingFlags.NonPublic);
            var routeMethodInfoBuffer = new object[2];
            var registerRouteMethodInfo = typeof(Session).GetMethod(nameof(RegisterRoute), BindingFlags.Instance | BindingFlags.NonPublic);
            var flag = false;
            object[] methodInfoBuffer = null;
            var funcFlag = false;
            object[] funcMethodInfoBuffer = null;
            Type[] typeBuffer1 = null;
            Type[] typeBuffer2 = null;
            Type[] typeBuffer3 = null;
            Type[] typeBuffer4 = null;
            Type[] typeBuffer5 = null;
            Type[] typeBuffer6 = null;
            Type[] typeBuffer7 = null;
            Type[] typeBuffer8 = null;
            Type[] typeBuffer9 = null;
            Type[] typeBuffer10 = null;
            Type[] typeBuffer11 = null;
            Type[] typeBuffer12 = null;
            Type[] typeBuffer13 = null;
            Type[] typeBuffer14 = null;
            Type[] typeBuffer15 = null;
            Type[] typeBuffer16 = null;
            Type[] typeBuffer17 = null;
            MethodInfo registerMethodInfo1 = null;
            MethodInfo registerMethodInfo2 = null;
            MethodInfo registerMethodInfo3 = null;
            MethodInfo registerMethodInfo4 = null;
            MethodInfo registerMethodInfo5 = null;
            MethodInfo registerMethodInfo6 = null;
            MethodInfo registerMethodInfo7 = null;
            MethodInfo registerMethodInfo8 = null;
            MethodInfo registerMethodInfo9 = null;
            MethodInfo registerMethodInfo10 = null;
            MethodInfo registerMethodInfo11 = null;
            MethodInfo registerMethodInfo12 = null;
            MethodInfo registerMethodInfo13 = null;
            MethodInfo registerMethodInfo14 = null;
            MethodInfo registerMethodInfo15 = null;
            MethodInfo registerMethodInfo16 = null;
            MethodInfo funcMethodInfo1 = null;
            MethodInfo funcMethodInfo2 = null;
            MethodInfo funcMethodInfo3 = null;
            MethodInfo funcMethodInfo4 = null;
            MethodInfo funcMethodInfo5 = null;
            MethodInfo funcMethodInfo6 = null;
            MethodInfo funcMethodInfo7 = null;
            MethodInfo funcMethodInfo8 = null;
            MethodInfo funcMethodInfo9 = null;
            MethodInfo funcMethodInfo10 = null;
            MethodInfo funcMethodInfo11 = null;
            MethodInfo funcMethodInfo12 = null;
            MethodInfo funcMethodInfo13 = null;
            MethodInfo funcMethodInfo14 = null;
            MethodInfo funcMethodInfo15 = null;
            MethodInfo funcMethodInfo16 = null;
            MethodInfo funcMethodInfo17 = null;
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var notifyRpcAttribute = methodInfo.GetCustomAttribute<NotifyRPCAttribute>();
                if (notifyRpcAttribute != null)
                {
                    BindMethod(notifyRpcMethodInfoBuffer, registerNotifyRpcMethodInfo, methodInfo, notifyRpcAttribute.Allocated);
                    continue;
                }

                var routeAttribute = methodInfo.GetCustomAttribute<RequireRPCAttribute>();
                if (routeAttribute != null)
                {
                    BindRouteMethod(routeMethodInfoBuffer, registerRouteMethodInfo, methodInfo, routeAttribute.Allocated);
                    continue;
                }

                var rpcAttribute = methodInfo.GetCustomAttribute<NotifyAttribute>();
                if (rpcAttribute != null)
                {
                    if (!flag)
                    {
                        flag = true;
                        methodInfoBuffer = new object[3];
                        methodInfoBuffer[0] = addHandlerCallback;
                    }

                    var parameters = methodInfo.GetParameters();
                    var length = parameters.Length;
                    if (length == 0)
                    {
                        RegisterHandler0(addHandlerCallback, rpcAttribute.Command, methodInfo);
                        continue;
                    }

                    Type[] typeBuffer = null;
                    MethodInfo registerMethodInfo = null;
                    switch (length)
                    {
                        case 1:
                            typeBuffer1 ??= new Type[1];
                            registerMethodInfo1 ??= typeof(Session).GetMethod(nameof(RegisterHandler1), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer1;
                            registerMethodInfo = registerMethodInfo1;
                            break;
                        case 2:
                            typeBuffer2 ??= new Type[2];
                            registerMethodInfo2 ??= typeof(Session).GetMethod(nameof(RegisterHandler2), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer2;
                            registerMethodInfo = registerMethodInfo2;
                            break;
                        case 3:
                            typeBuffer3 ??= new Type[3];
                            registerMethodInfo3 ??= typeof(Session).GetMethod(nameof(RegisterHandler3), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer3;
                            registerMethodInfo = registerMethodInfo3;
                            break;
                        case 4:
                            typeBuffer4 ??= new Type[4];
                            registerMethodInfo4 ??= typeof(Session).GetMethod(nameof(RegisterHandler4), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer4;
                            registerMethodInfo = registerMethodInfo4;
                            break;
                        case 5:
                            typeBuffer5 ??= new Type[5];
                            registerMethodInfo5 ??= typeof(Session).GetMethod(nameof(RegisterHandler5), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer5;
                            registerMethodInfo = registerMethodInfo5;
                            break;
                        case 6:
                            typeBuffer6 ??= new Type[6];
                            registerMethodInfo6 ??= typeof(Session).GetMethod(nameof(RegisterHandler6), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer6;
                            registerMethodInfo = registerMethodInfo6;
                            break;
                        case 7:
                            typeBuffer7 ??= new Type[7];
                            registerMethodInfo7 ??= typeof(Session).GetMethod(nameof(RegisterHandler7), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer7;
                            registerMethodInfo = registerMethodInfo7;
                            break;
                        case 8:
                            typeBuffer8 ??= new Type[8];
                            registerMethodInfo8 ??= typeof(Session).GetMethod(nameof(RegisterHandler8), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer8;
                            registerMethodInfo = registerMethodInfo8;
                            break;
                        case 9:
                            typeBuffer9 ??= new Type[9];
                            registerMethodInfo9 ??= typeof(Session).GetMethod(nameof(RegisterHandler9), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer9;
                            registerMethodInfo = registerMethodInfo9;
                            break;
                        case 10:
                            typeBuffer10 ??= new Type[10];
                            registerMethodInfo10 ??= typeof(Session).GetMethod(nameof(RegisterHandler10), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer10;
                            registerMethodInfo = registerMethodInfo10;
                            break;
                        case 11:
                            typeBuffer11 ??= new Type[11];
                            registerMethodInfo11 ??= typeof(Session).GetMethod(nameof(RegisterHandler11), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer11;
                            registerMethodInfo = registerMethodInfo11;
                            break;
                        case 12:
                            typeBuffer12 ??= new Type[12];
                            registerMethodInfo12 ??= typeof(Session).GetMethod(nameof(RegisterHandler12), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer12;
                            registerMethodInfo = registerMethodInfo12;
                            break;
                        case 13:
                            typeBuffer13 ??= new Type[13];
                            registerMethodInfo13 ??= typeof(Session).GetMethod(nameof(RegisterHandler13), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer13;
                            registerMethodInfo = registerMethodInfo13;
                            break;
                        case 14:
                            typeBuffer14 ??= new Type[14];
                            registerMethodInfo14 ??= typeof(Session).GetMethod(nameof(RegisterHandler14), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer14;
                            registerMethodInfo = registerMethodInfo14;
                            break;
                        case 15:
                            typeBuffer15 ??= new Type[15];
                            registerMethodInfo15 ??= typeof(Session).GetMethod(nameof(RegisterHandler15), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer15;
                            registerMethodInfo = registerMethodInfo15;
                            break;
                        case 16:
                            typeBuffer16 ??= new Type[16];
                            registerMethodInfo16 ??= typeof(Session).GetMethod(nameof(RegisterHandler16), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer16;
                            registerMethodInfo = registerMethodInfo16;
                            break;
                    }

                    BindMethod(typeBuffer, methodInfoBuffer, length, registerMethodInfo, parameters, rpcAttribute.Command, methodInfo);
                    continue;
                }

                var funcAttribute = methodInfo.GetCustomAttribute<RequireAttribute>();
                if (funcAttribute != null)
                {
                    if (!funcFlag)
                    {
                        funcFlag = true;
                        funcMethodInfoBuffer = new object[3];
                        funcMethodInfoBuffer[0] = addFuncCallback;
                    }

                    var returnType = methodInfo.ReturnType;
                    var parameters = methodInfo.GetParameters();
                    var length = parameters.Length;
                    Type[] typeBuffer = null;
                    MethodInfo funcMethodInfo = null;
                    switch (length)
                    {
                        case 0:
                            typeBuffer1 ??= new Type[1];
                            funcMethodInfo1 ??= typeof(Session).GetMethod(nameof(RegisterFunc0), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer1;
                            funcMethodInfo = funcMethodInfo1;
                            break;
                        case 1:
                            typeBuffer2 ??= new Type[2];
                            funcMethodInfo2 ??= typeof(Session).GetMethod(nameof(RegisterFunc1), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer2;
                            funcMethodInfo = funcMethodInfo2;
                            break;
                        case 2:
                            typeBuffer3 ??= new Type[3];
                            funcMethodInfo3 ??= typeof(Session).GetMethod(nameof(RegisterFunc2), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer3;
                            funcMethodInfo = funcMethodInfo3;
                            break;
                        case 3:
                            typeBuffer4 ??= new Type[4];
                            funcMethodInfo4 ??= typeof(Session).GetMethod(nameof(RegisterFunc3), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer4;
                            funcMethodInfo = funcMethodInfo4;
                            break;
                        case 4:
                            typeBuffer5 ??= new Type[5];
                            funcMethodInfo5 ??= typeof(Session).GetMethod(nameof(RegisterFunc4), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer5;
                            funcMethodInfo = funcMethodInfo5;
                            break;
                        case 5:
                            typeBuffer6 ??= new Type[6];
                            funcMethodInfo6 ??= typeof(Session).GetMethod(nameof(RegisterFunc5), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer6;
                            funcMethodInfo = funcMethodInfo6;
                            break;
                        case 6:
                            typeBuffer7 ??= new Type[7];
                            funcMethodInfo7 ??= typeof(Session).GetMethod(nameof(RegisterFunc6), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer7;
                            funcMethodInfo = funcMethodInfo7;
                            break;
                        case 7:
                            typeBuffer8 ??= new Type[8];
                            funcMethodInfo8 ??= typeof(Session).GetMethod(nameof(RegisterFunc7), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer8;
                            funcMethodInfo = funcMethodInfo8;
                            break;
                        case 8:
                            typeBuffer9 ??= new Type[9];
                            funcMethodInfo9 ??= typeof(Session).GetMethod(nameof(RegisterFunc8), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer9;
                            funcMethodInfo = funcMethodInfo9;
                            break;
                        case 9:
                            typeBuffer10 ??= new Type[10];
                            funcMethodInfo10 ??= typeof(Session).GetMethod(nameof(RegisterFunc9), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer10;
                            funcMethodInfo = funcMethodInfo10;
                            break;
                        case 10:
                            typeBuffer11 ??= new Type[11];
                            funcMethodInfo11 ??= typeof(Session).GetMethod(nameof(RegisterFunc10), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer11;
                            funcMethodInfo = funcMethodInfo11;
                            break;
                        case 11:
                            typeBuffer12 ??= new Type[12];
                            funcMethodInfo12 ??= typeof(Session).GetMethod(nameof(RegisterFunc11), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer12;
                            funcMethodInfo = funcMethodInfo12;
                            break;
                        case 12:
                            typeBuffer13 ??= new Type[13];
                            funcMethodInfo13 ??= typeof(Session).GetMethod(nameof(RegisterFunc12), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer13;
                            funcMethodInfo = funcMethodInfo13;
                            break;
                        case 13:
                            typeBuffer14 ??= new Type[14];
                            funcMethodInfo14 ??= typeof(Session).GetMethod(nameof(RegisterFunc13), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer14;
                            funcMethodInfo = funcMethodInfo14;
                            break;
                        case 14:
                            typeBuffer15 ??= new Type[15];
                            funcMethodInfo15 ??= typeof(Session).GetMethod(nameof(RegisterFunc14), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer15;
                            funcMethodInfo = funcMethodInfo15;
                            break;
                        case 15:
                            typeBuffer16 ??= new Type[16];
                            funcMethodInfo16 ??= typeof(Session).GetMethod(nameof(RegisterFunc15), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer16;
                            funcMethodInfo = funcMethodInfo16;
                            break;
                        case 16:
                            typeBuffer17 ??= new Type[17];
                            funcMethodInfo17 ??= typeof(Session).GetMethod(nameof(RegisterFunc16), BindingFlags.Instance | BindingFlags.NonPublic);
                            typeBuffer = typeBuffer17;
                            funcMethodInfo = funcMethodInfo17;
                            break;
                    }

                    BindFuncMethod(returnType, typeBuffer, funcMethodInfoBuffer, length, funcMethodInfo, parameters, funcAttribute.Command, methodInfo);
                }
            }
        }

        /// <summary>
        ///     Unregister Command Handler
        /// </summary>
        private void UnregisterHandlers(Type type, Action<uint> removeHandlerCallback, Action<uint> removeFuncCallback)
        {
            var unregisterNotifyRpcMethodInfo = typeof(Session).GetMethod(nameof(UnregisterHandler), BindingFlags.Instance | BindingFlags.NonPublic);
            var unregisterRouteMethodInfo = typeof(Session).GetMethod(nameof(UnregisterRoute), BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var notifyRpcAttribute = methodInfo.GetCustomAttribute<NotifyRPCAttribute>();
                if (notifyRpcAttribute != null)
                {
                    FreeMethod(unregisterNotifyRpcMethodInfo, methodInfo);
                    continue;
                }

                var routeAttribute = methodInfo.GetCustomAttribute<RequireRPCAttribute>();
                if (routeAttribute != null)
                {
                    FreeRouteMethod(unregisterRouteMethodInfo, methodInfo);
                    continue;
                }

                var rpcAttribute = methodInfo.GetCustomAttribute<NotifyAttribute>();
                if (rpcAttribute != null)
                {
                    removeHandlerCallback.Invoke(rpcAttribute.Command);
                    continue;
                }

                var funcAttribute = methodInfo.GetCustomAttribute<RequireAttribute>();
                if (funcAttribute != null)
                    removeHandlerCallback.Invoke(funcAttribute.Command);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler0(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler1<T0>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler2<T0, T1>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler3<T0, T1, T2>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler4<T0, T1, T2, T3>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler5<T0, T1, T2, T3, T4>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler6<T0, T1, T2, T3, T4, T5>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler7<T0, T1, T2, T3, T4, T5, T6>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler8<T0, T1, T2, T3, T4, T5, T6, T7>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler11<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler12<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler13<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler14<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler15<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        private void RegisterHandler16<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<uint, NetworkClientServiceHandler> addHandlerCallback, uint command, MethodInfo methodInfo)
        {
            var warpedHandler1 = NetworkReaderHandlers.WrapHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this, methodInfo);
            var warpedHandler2 = new NetworkClientServiceHandler(warpedHandler1);
            addHandlerCallback.Invoke(command, warpedHandler2);
        }

        /// <summary>
        ///     Binding method
        /// </summary>
        /// <param name="typeBuffer">Type buffer</param>
        /// <param name="methodInfoBuffer">Method information buffer</param>
        /// <param name="length">Register MethodInfos Count</param>
        /// <param name="registerMethodInfo">Register MethodInfos</param>
        /// <param name="parameters">ParameterInfos</param>
        /// <param name="command">Command</param>
        /// <param name="methodInfo">Method information</param>
        private void BindMethod(Type[] typeBuffer, object[] methodInfoBuffer, int length, MethodInfo registerMethodInfo, ParameterInfo[] parameters, uint command, MethodInfo methodInfo)
        {
            for (var i = 0; i < length; ++i)
                typeBuffer[i] = parameters[i].ParameterType;
            methodInfoBuffer[1] = command;
            methodInfoBuffer[2] = methodInfo;
            registerMethodInfo.MakeGenericMethod(typeBuffer).Invoke(this, methodInfoBuffer);
            Array.Clear(typeBuffer, 0, length);
            Array.Clear(methodInfoBuffer, 1, 2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc0<T0>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0>)Delegate.CreateDelegate(typeof(Func<T0>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc1<T0, T1>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1>)Delegate.CreateDelegate(typeof(Func<T0, T1>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc2<T0, T1, T2>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc3<T0, T1, T2, T3>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc4<T0, T1, T2, T3, T4>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc5<T0, T1, T2, T3, T4, T5>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc6<T0, T1, T2, T3, T4, T5, T6>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc7<T0, T1, T2, T3, T4, T5, T6, T7>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc8<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc9<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc11<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc12<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc13<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc14<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc15<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Register Command Func
        /// </summary>
        private void RegisterFunc16<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<uint, NetworkClientServiceFuncBase> addFuncCallback, uint command, MethodInfo methodInfo)
        {
            var warpedFunc1 = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>), this, methodInfo);
            var warpedFunc2 = new NetworkClientServiceFunc<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(warpedFunc1);
            addFuncCallback.Invoke(command, warpedFunc2);
        }

        /// <summary>
        ///     Binding method
        /// </summary>
        /// <param name="returnType">ReturnType</param>
        /// <param name="typeBuffer">Type buffer</param>
        /// <param name="methodInfoBuffer">Method information buffer</param>
        /// <param name="length">Register MethodInfos Count</param>
        /// <param name="registerMethodInfo">Register MethodInfos</param>
        /// <param name="parameters">ParameterInfos</param>
        /// <param name="command">Command</param>
        /// <param name="methodInfo">Method information</param>
        private void BindFuncMethod(Type returnType, Type[] typeBuffer, object[] methodInfoBuffer, int length, MethodInfo registerMethodInfo, ParameterInfo[] parameters, uint command, MethodInfo methodInfo)
        {
            for (var i = 0; i < length; ++i)
                typeBuffer[i] = parameters[i].ParameterType;
            typeBuffer[length] = returnType;
            methodInfoBuffer[1] = command;
            methodInfoBuffer[2] = methodInfo;
            registerMethodInfo.MakeGenericMethod(typeBuffer).Invoke(this, methodInfoBuffer);
            Array.Clear(typeBuffer, 0, length);
            Array.Clear(methodInfoBuffer, 1, 2);
        }
    }
}