//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode.Transports.UTP;

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public static partial class NetcodeServer
    {
        /// <summary>
        ///     Unity Transmitter
        /// </summary>
        private static UnityTransport Utp => (UnityTransport)NetManager.NetworkConfig.NetworkTransport;

        /// <summary>
        ///     Round-trip delay
        /// </summary>
        /// <param name="clientId">ClientId</param>
        public static ulong Rtt(ulong clientId) => Utp.GetCurrentRtt(clientId);

        /// <summary>
        ///     Ping
        /// </summary>
        /// <param name="clientId">ClientId</param>
        public static ulong Ping(ulong clientId) => Rtt(clientId) * 1000L;

        /// <summary>
        ///     Start transmission
        /// </summary>
        public static void StartTransport()
        {
            if (IsServer)
                Utp.StartServer();
        }

        /// <summary>
        ///     Stop transmission
        /// </summary>
        public static void StopTransport()
        {
            if (!IsServer)
                return;
            if (ConnectedClientsCount == 0)
            {
                Utp.Shutdown();
                return;
            }

            var keys = MathV.ToList(NetManager.ConnectedClients.Keys);
            if (keys[0] == 0)
                for (var index = 1; index < keys.Count; ++index)
                    NetManager.DisconnectClient(keys[index]);
            else
                for (var index = 0; index < keys.Count; ++index)
                    NetManager.DisconnectClient(keys[index]);
            Utp.Shutdown();
        }

        /// <summary>
        ///     Set up server encryption verification data
        /// </summary>
        /// <param name="serverCertificate">The public certificate of the server(PEMFormat)</param>
        /// <param name="serverPrivateKey">The private key of the server(PEMFormat)</param>
        public static void SetServerSecrets(string serverCertificate, string serverPrivateKey) => Utp.SetServerSecrets(serverCertificate, serverPrivateKey);
    }
}