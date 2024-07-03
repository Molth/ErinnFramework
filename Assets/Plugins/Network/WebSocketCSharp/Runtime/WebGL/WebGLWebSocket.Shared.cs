//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if !UNITY_WEBGL || UNITY_EDITOR
using System;
#endif

// ReSharper disable UnusedParameter.Global
// ReSharper disable InvocationIsSkipped

namespace Erinn
{
    /// <summary>
    ///     Unity WebGL WebSocket
    /// </summary>
    public sealed partial class WebGLWebSocket
    {
        /// <summary>
        ///     Inner client
        /// </summary>
        private readonly IWebSocketClient _innerClient;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="innerClient">Inner client</param>
        public WebGLWebSocket(IWebSocketClient innerClient) => _innerClient = innerClient;

        /// <summary>
        ///     Connect
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        /// <param name="port">Port</param>
        public void Connect(string ipAddress, ushort port)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var address = $"ws://{ipAddress}:{port}";
            InstanceId = WebGLWebSocketManager.AllocateInstance(address, "arraybuffer");
            Log($"Allocate socket with instanceId: {InstanceId}");
            WebGLWebSocketManager.Add(this);
            Log($"Connect with instanceId: {InstanceId}");
            var code = WebGLWebSocketManager.WebSocketConnect(InstanceId);
            if (code >= 0)
                return;
            HandleOnError(GetErrorMessageFromCode(code));
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        ///     Send data
        /// </summary>
        /// <param name="data">Data</param>
        public void Send(byte[] data)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var length = data.Length;
            Log($"Send, type: 2, size: {length}");
            var code = WebGLWebSocketManager.WebSocketSend(InstanceId, data, length);
            if (code >= 0)
                return;
            HandleOnError(GetErrorMessageFromCode(code));
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public void Close()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Log($"Close with instanceId: {InstanceId}");
            var code = WebGLWebSocketManager.WebSocketClose(InstanceId, 1000, "Normal Closure");
            if (code < 0)
                HandleOnError(GetErrorMessageFromCode(code));
            Log($"Free socket with instanceId: {InstanceId}");
            WebGLWebSocketManager.WebSocketFree(InstanceId);
#else
            throw new NotImplementedException();
#endif
        }
    }
}