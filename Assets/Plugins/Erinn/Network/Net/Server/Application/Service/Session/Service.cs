//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Service Session
    /// </summary>
    public abstract partial class Service
    {
        /// <summary>
        ///     Network server
        /// </summary>
        private NetworkServer _networkServer;

        /// <summary>
        ///     EndPoint
        /// </summary>
        private NetworkServerMessageEndPoint _endPoint;

        /// <summary>
        ///     Connected
        /// </summary>
        private volatile bool _connected;

        /// <summary>
        ///     ClientId
        /// </summary>
        protected uint Id { get; private set; }

        /// <summary>
        ///     Connected
        /// </summary>
        protected bool Connected => _connected;

        /// <summary>
        ///     Call on connected
        /// </summary>
        internal void OnConnectedInternal(NetworkServer networkServer, NetworkServerMessageEndPoint endPoint, uint id)
        {
            _networkServer = networkServer;
            _endPoint = endPoint;
            Id = id;
            _connected = true;
            OnConnected();
        }

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        internal void OnDisconnectedInternal()
        {
            _connected = false;
            OnDisconnected();
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        protected void Disconnect()
        {
            if (!Connected)
                return;
            _connected = false;
            _networkServer.Disconnect(Id);
        }

        /// <summary>
        ///     Call on connected
        /// </summary>
        protected abstract void OnConnected();

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        protected abstract void OnDisconnected();
    }
}