//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Net;

namespace Erinn
{
    /// <summary>
    ///     Network Discovery Components
    /// </summary>
    public sealed class NetworkDiscovery : NetworkDiscoveryBase<DiscoveryBroadcastData, DiscoveryResponseData>
    {
        /// <summary>
        ///     Server Name
        /// </summary>
        public string ServerName = Dns.GetHostName();

        /// <summary>
        ///     Single example
        /// </summary>
        public static NetworkDiscovery Singleton { get; private set; }

        /// <summary>
        ///     Call on load
        /// </summary>
        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
        }

        /// <summary>
        ///     Server Discovery Event
        /// </summary>
        public event Action<IPEndPoint, DiscoveryResponseData> OnServerFound;

        /// <summary>
        ///     Client Discovery Event
        /// </summary>
        public event Action<IPEndPoint, DiscoveryBroadcastData> OnClientFount;

        /// <summary>
        ///     Get client broadcast data
        /// </summary>
        /// <returns>Obtained client broadcast data</returns>
        protected override DiscoveryBroadcastData GetClientBroadcast() => new();

        /// <summary>
        ///     Get server response data
        /// </summary>
        /// <returns>Obtained server response data</returns>
        protected override DiscoveryResponseData GetServerResponse() => new()
        {
            Address = NetcodeSystem.Address,
            Port = NetcodeSystem.Port,
            ServerName = ServerName
        };

        /// <summary>
        ///     Client receives server response
        /// </summary>
        /// <param name="sender">Response sender</param>
        /// <param name="response">Response data</param>
        protected override void ClientReceiveResponse(IPEndPoint sender, DiscoveryResponseData response) => OnServerFound?.Invoke(sender, response);

        /// <summary>
        ///     Server processing client response
        /// </summary>
        /// <param name="sender">Broadcast sender</param>
        /// <param name="broadCast">Broadcast data</param>
        /// <param name="response">Return response data</param>
        /// <returns>Do you want to return response data</returns>
        protected override bool ServerProcessBroadcast(IPEndPoint sender, DiscoveryBroadcastData broadCast, out DiscoveryResponseData response)
        {
            OnClientFount?.Invoke(sender, broadCast);
            response = GetServerResponse();
            return true;
        }
    }
}