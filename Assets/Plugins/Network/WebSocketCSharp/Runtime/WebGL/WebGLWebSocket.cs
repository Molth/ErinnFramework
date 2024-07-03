//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_WEBGL && !UNITY_EDITOR
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
#endif

// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable PossibleNullReferenceException
// ReSharper disable InvocationIsSkipped

namespace Erinn
{
    /// <summary>
    ///     Unity WebGL WebSocket
    /// </summary>
    public sealed partial class WebGLWebSocket
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        /// <summary>
        ///     Instance index
        /// </summary>
        internal int InstanceId { get; private set; }

        /// <summary>
        ///     Connection callback
        /// </summary>
        internal void HandleOnOpen()
        {
            Log("OnOpen");
            _innerClient.OnOpen();
        }

        /// <summary>
        ///     Close callback
        /// </summary>
        /// <param name="code">Event code</param>
        /// <param name="reason">Reason</param>
        internal void HandleOnClose(int code, string reason)
        {
            Log($"OnClose, code: {code}, reason: {reason}");
            WebGLWebSocketManager.Remove(InstanceId);
            WebGLWebSocketManager.Remove(InstanceId);
            _innerClient.OnClose();
        }

        /// <summary>
        ///     Message callback
        /// </summary>
        /// <param name="rawData">Data</param>
        internal void HandleOnMessage(in ArraySegment<byte> rawData)
        {
            Log($"OnMessage, type: 2, size: {4 + rawData.Count}");
            _innerClient.OnMessage(in rawData);
        }

        /// <summary>
        ///     Error callback
        /// </summary>
        /// <param name="msg">News</param>
        internal void HandleOnError(string msg) => Log("OnError, error: " + msg);

        /// <summary>
        ///     Get messages
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <returns>Received messages</returns>
        internal static string GetErrorMessageFromCode(int errorCode) => errorCode switch
        {
            -1 => "WebSocket instance not found.",
            -2 => "WebSocket is already connected or in connecting state.",
            -3 => "WebSocket is not connected.",
            -4 => "WebSocket is already closing.",
            -5 => "WebSocket is already closed.",
            -6 => "WebSocket is not in open state.",
            -7 => "Cannot close WebSocket. An invalid code was specified or reason is too long.",
            _ => $"Unknown error code {errorCode}."
        };

        /// <summary>
        ///     Printing
        /// </summary>
        /// <param name="msg">News</param>
        [Conditional("UNITY_WEB_SOCKET_LOG")]
        private static void Log(string msg) => Debug.Log("[UnityWebSocket]" + $"[{DateTime.Now.TimeOfDay}]" + $" {msg}");
#endif
    }
}