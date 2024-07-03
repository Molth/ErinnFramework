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
    ///     Client Application base
    /// </summary>
    public abstract partial class ClientBase
    {
        /// <summary>
        ///     Network client
        /// </summary>
        private readonly NetworkClient _peer = new();

        /// <summary>
        ///     EndPoint
        /// </summary>
        private readonly NetworkClientMessageEndPoint _endPoint;

        /// <summary>
        ///     Structure
        /// </summary>
        protected ClientBase()
        {
            var type = GetType();
            _peer.RegisterHandlers(this, type);
            _peer.OnConnectedCallback += OnConnected;
            _peer.OnDisconnectedCallback += OnDisconnected;
            _endPoint = new NetworkClientMessageEndPoint(_peer);
            _endPoint.RegisterFuncs(type);
            _serviceChannel = new NetworkClientServiceChannel(_peer, _endPoint);
            _isSet = true;
        }

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose()
        {
            _isSet = false;
            _peer.Destroy();
            ClearServices();
            OnDispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release
        /// </summary>
        protected virtual void OnDispose()
        {
        }
    }
}