//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Session interface
    /// </summary>
    internal interface ISession
    {
        /// <summary>
        ///     Call on connected
        /// </summary>
        void OnConnected();

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        void OnDisconnected();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers(NetworkClient networkClient, NetworkClientMessageEndPoint endPoint, Action<uint, NetworkClientServiceHandler> addHandlerCallback, Action<uint, NetworkClientServiceFuncBase> addFuncCallback);

        /// <summary>
        ///     Unregister Command Handler
        /// </summary>
        void UnregisterHandlers(Action<uint> removeHandlerCallback, Action<uint> removeFuncCallback);
    }
}