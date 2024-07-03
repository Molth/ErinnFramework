//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_WEBGL && !UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace Erinn
{
    /// <summary>
    ///     Unity WebGL WebSocketManager
    /// </summary>
    internal static class WebGLWebSocketManager
    {
        /// <summary>
        ///     Close callback
        /// </summary>
        public delegate void OnCloseCallback(int instanceId, int closeCode, IntPtr reasonPtr);

        /// <summary>
        ///     Error callback
        /// </summary>
        public delegate void OnErrorCallback(int instanceId, IntPtr errorPtr);

        /// <summary>
        ///     Message callback
        /// </summary>
        public delegate void OnMessageCallback(int instanceId, IntPtr msgPtr, int msgSize);

        /// <summary>
        ///     Connection callback
        /// </summary>
        public delegate void OnOpenCallback(int instanceId);

        /// <summary>
        ///     Instance dictionary
        /// </summary>
        private static readonly Dictionary<int, WebGLWebSocket> Instances = new();

        /// <summary>
        ///     Already initialized
        /// </summary>
        private static bool _isInitialized;

        /// <summary>
        ///     Receive buffer
        /// </summary>
        private static readonly byte[] ReceiveBuffer = new byte[1024];

        /// <summary>
        ///     Connect
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebSocketConnect(int instanceId);

        /// <summary>
        ///     Disconnect
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebSocketClose(int instanceId, int code, string reason);

        /// <summary>
        ///     Send
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebSocketSend(int instanceId, byte[] dataPtr, int dataLength);

        /// <summary>
        ///     Get Status
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebSocketGetState(int instanceId);

        /// <summary>
        ///     Instantiation
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebSocketAllocate(string url, string binaryType);

        /// <summary>
        ///     Add protocols
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebSocketAddSubProtocol(int instanceId, string protocol);

        /// <summary>
        ///     Release
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebSocketFree(int instanceId);

        /// <summary>
        ///     Set connection callback
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebSocketSetOnOpen(OnOpenCallback callback);

        /// <summary>
        ///     Set the disconnect callback
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebSocketSetOnClose(OnCloseCallback callback);

        /// <summary>
        ///     Set message callback
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebSocketSetOnMessage(OnMessageCallback callback);

        /// <summary>
        ///     Set error callback
        /// </summary>
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebSocketSetOnError(OnErrorCallback callback);

        /// <summary>
        ///     Initialization
        /// </summary>
        private static void Initialize()
        {
            if (_isInitialized)
                return;
            WebSocketSetOnOpen(DelegateOnOpenEvent);
            WebSocketSetOnMessage(DelegateOnMessageEvent);
            WebSocketSetOnError(DelegateOnErrorEvent);
            WebSocketSetOnClose(DelegateOnCloseEvent);
            _isInitialized = true;
        }

        /// <summary>
        ///     Handle connection callbacks
        /// </summary>
        [MonoPInvokeCallback(typeof(OnOpenCallback))]
        public static void DelegateOnOpenEvent(int instanceId)
        {
            if (!Instances.TryGetValue(instanceId, out var socket))
                return;
            socket.HandleOnOpen();
        }

        /// <summary>
        ///     Handle disconnect callback
        /// </summary>
        [MonoPInvokeCallback(typeof(OnCloseCallback))]
        public static void DelegateOnCloseEvent(int instanceId, int closeCode, IntPtr reasonPtr)
        {
            if (!Instances.TryGetValue(instanceId, out var socket))
                return;
            var reason = Marshal.PtrToStringAuto(reasonPtr);
            socket.HandleOnClose(closeCode, reason);
        }

        /// <summary>
        ///     Processing message callbacks
        /// </summary>
        [MonoPInvokeCallback(typeof(OnMessageCallback))]
        public static void DelegateOnMessageEvent(int instanceId, IntPtr msgPtr, int msgSize)
        {
            if (!Instances.TryGetValue(instanceId, out var socket))
                return;
            Marshal.Copy(msgPtr, ReceiveBuffer, 0, msgSize);
            var rawData = new ArraySegment<byte>(ReceiveBuffer, 0, msgSize);
            socket.HandleOnMessage(in rawData);
        }

        /// <summary>
        ///     Handling error callbacks
        /// </summary>
        [MonoPInvokeCallback(typeof(OnErrorCallback))]
        public static void DelegateOnErrorEvent(int instanceId, IntPtr errorPtr)
        {
            if (!Instances.TryGetValue(instanceId, out var socket))
                return;
            var errorMsg = Marshal.PtrToStringAuto(errorPtr);
            socket.HandleOnError(errorMsg);
        }

        /// <summary>
        ///     Instantiation
        /// </summary>
        public static int AllocateInstance(string address, string binaryType)
        {
            Initialize();
            return WebSocketAllocate(address, binaryType);
        }

        /// <summary>
        ///     Add instance
        /// </summary>
        public static void Add(WebGLWebSocket socket) => Instances.TryAdd(socket.InstanceId, socket);

        /// <summary>
        ///     Remove instance
        /// </summary>
        public static void Remove(int instanceId) => Instances.Remove(instanceId);
    }
}
#endif