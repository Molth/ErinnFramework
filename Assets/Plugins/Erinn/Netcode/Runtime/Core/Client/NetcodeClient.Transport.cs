//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using Unity.Netcode.Transports.UTP;

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public static partial class NetcodeClient
    {
        /// <summary>
        ///     UnityTransmitters
        /// </summary>
        private static UnityTransport Utp => (UnityTransport)NetManager.NetworkConfig.NetworkTransport;

        /// <summary>
        ///     Round-trip delay
        /// </summary>
        public static ulong Rtt => Utp.GetCurrentRtt(0UL);

        /// <summary>
        ///     Delay
        /// </summary>
        public static ulong Ping => Rtt * 1000L;

        /// <summary>
        ///     Set connection verification data
        /// </summary>
        /// <param name="data">Connection verification data</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SetApprovalData<T>(T data) where T : struct, IMemoryPackable<T> => NetManager.NetworkConfig.ConnectionData = NetcodeSerializer.Serialize(data);

        /// <summary>
        ///     Set up client encrypted verification data
        /// </summary>
        /// <param name="serverCommonName">The common name of the server</param>
        public static void SetClientSecrets(string serverCommonName) => Utp.SetClientSecrets(serverCommonName);

        /// <summary>
        ///     Set up client encrypted verification data
        /// </summary>
        /// <param name="serverCommonName">The common name of the server</param>
        /// <param name="caCertificate">Server'sCACertificate</param>
        public static void SetClientSecrets(string serverCommonName, string caCertificate) => Utp.SetClientSecrets(serverCommonName, caCertificate);
    }
}