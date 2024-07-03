//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network events
    /// </summary>
    public static partial class NetcodeEvents
    {
        /// <summary>
        ///     Network Manager
        /// </summary>
        private static NetworkManager NetManager => NetworkManager.Singleton;

        /// <summary>
        ///     Client Connection Event
        /// </summary>
        public static event Action<ulong> OnClientConnectedCallback
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnClientConnectedCallback += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnClientConnectedCallback -= value;
            }
        }

        /// <summary>
        ///     Client disconnect event
        /// </summary>
        public static event Action<ulong> OnClientDisconnectCallback
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnClientDisconnectCallback += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnClientDisconnectCallback -= value;
            }
        }

        /// <summary>
        ///     Transmission failure event
        /// </summary>
        public static event Action OnTransportFailure
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnTransportFailure += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnTransportFailure -= value;
            }
        }
    }
}