//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     WebSocketClient interface
    /// </summary>
    public interface IWebSocketClient
    {
        /// <summary>
        ///     Open callback
        /// </summary>
        void OnOpen();

        /// <summary>
        ///     Close callback
        /// </summary>
        void OnClose();

        /// <summary>
        ///     Message callback
        /// </summary>
        void OnMessage(in ArraySegment<byte> packet);
    }
}