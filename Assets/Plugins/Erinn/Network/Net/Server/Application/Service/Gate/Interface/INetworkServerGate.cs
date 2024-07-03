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
    ///     Session Manager interface
    /// </summary>
    internal interface INetworkServerGate
    {
        /// <summary>
        ///     Call on connected
        /// </summary>
        /// <param name="id">ClientId</param>
        void OnConnected(uint id);

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        /// <param name="id">ClientId</param>
        void OnDisconnected(uint id);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers(Action<uint, NetworkServerServiceHandlerBase> addHandlerCallback, Action<uint, NetworkServerServiceFuncBase> addFuncCallback);

        /// <summary>
        ///     Unregister Command Handler
        /// </summary>
        void UnregisterHandlers(Action<uint> removeHandlerCallback, Action<uint> removeFuncCallback);
    }
}