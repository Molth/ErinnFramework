//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Net;
using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Online system
    /// </summary>
    public static partial class NetcodeSystem
    {
        /// <summary>
        ///     Host Name
        /// </summary>
        public static string HostName => Dns.GetHostName();

        /// <summary>
        ///     Local Ipv4 Address
        /// </summary>
        public static string LocalIpv4
        {
            get
            {
                try
                {
                    foreach (var hostAddress in Dns.GetHostAddresses(HostName))
                        if ((int)hostAddress.AddressFamily == 2)
                            return hostAddress.ToString();
                }
                catch (Exception e)
                {
                    Log.Info(e);
                }

                return "127.0.0.1";
            }
        }

        /// <summary>
        ///     Local Ipv6 Address
        /// </summary>
        public static string LocalIpv6
        {
            get
            {
                try
                {
                    foreach (var hostAddress in Dns.GetHostAddresses(HostName))
                        if ((int)hostAddress.AddressFamily == 23)
                            return hostAddress.ToString();
                }
                catch (Exception e)
                {
                    Log.Info(e);
                }

                return "::1";
            }
        }

        /// <summary>
        ///     Network configuration
        /// </summary>
        private static NetworkConfig Config => NetManager.NetworkConfig;

        /// <summary>
        ///     Set Protocol Version
        /// </summary>
        /// <param name="protocolVersion">Protocol version</param>
        public static void SetProtocolVersion(ushort protocolVersion) => Config.ProtocolVersion = protocolVersion;

        /// <summary>
        ///     Set upRpcHash capacity
        /// </summary>
        /// <param name="rpcHashSize">RpcHash capacity</param>
        public static void SetRpcHashSize(HashSize rpcHashSize) => Config.RpcHashSize = rpcHashSize;

        /// <summary>
        ///     Set transmission frequency
        /// </summary>
        /// <param name="tickRate">Transmission frequency</param>
        public static void SetTickRate(uint tickRate) => Config.TickRate = tickRate;

        /// <summary>
        ///     Set to enable network logging
        /// </summary>
        /// <param name="enableNetworkLogs">Enable network logging</param>
        public static void SetEnableNetworkLogs(bool enableNetworkLogs) => Config.EnableNetworkLogs = enableNetworkLogs;

        /// <summary>
        ///     Set mandatory identical prefabricated components
        /// </summary>
        /// <param name="forceSamePrefabs">Mandatory identical prefabricated components</param>
        public static void SetForceSamePrefabs(bool forceSamePrefabs) => Config.ForceSamePrefabs = forceSamePrefabs;

        /// <summary>
        ///     Add online prefabricated components
        /// </summary>
        /// <param name="networkPrefab">Online prefabricated components</param>
        public static bool AddNetworkPrefab(NetworkPrefab networkPrefab) => Config.Prefabs.Add(networkPrefab);

        /// <summary>
        ///     Remove online prefabricated components
        /// </summary>
        /// <param name="networkPrefab">Online prefabricated components</param>
        public static void RemoveNetworkPrefab(NetworkPrefab networkPrefab) => Config.Prefabs.Remove(networkPrefab);

        /// <summary>
        ///     Check the online prefabricated components
        /// </summary>
        /// <param name="networkPrefab">Online prefabricated components</param>
        public static bool CheckNetworkPrefab(NetworkPrefab networkPrefab) => Config.Prefabs.Contains(networkPrefab);

        /// <summary>
        ///     Set player prefabricated parts
        /// </summary>
        /// <param name="playerPrefab">Player prefabricated parts</param>
        public static void SetPlayerPrefab(GameObject playerPrefab)
        {
            if (playerPrefab != null)
                Config.PlayerPrefab = playerPrefab;
        }

        /// <summary>
        ///     Set up server to enable connection approval
        /// </summary>
        /// <param name="connectionApproval">Enable connection approval</param>
        public static void SetConnectionApproval(bool connectionApproval) => Config.ConnectionApproval = connectionApproval;

        /// <summary>
        ///     Set server connection approval callback event
        /// </summary>
        /// <param name="callback">Connection approval callback event</param>
        public static void SetConnectionApprovalCallback(Action<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse> callback) => NetManager.ConnectionApprovalCallback = callback;

        /// <summary>
        ///     Set server connection approval timeout
        /// </summary>
        /// <param name="timeout">Second</param>
        public static void SetConnectionApprovalTimeout(int timeout) => Config.ClientConnectionBufferTimeout = timeout;

        /// <summary>
        ///     Set up networkIdWhether to recycle
        /// </summary>
        /// <param name="recycleNetworkIds">NetworkIdWhether to recycle</param>
        public static void SetRecycleNetworkIds(bool recycleNetworkIds) => Config.RecycleNetworkIds = recycleNetworkIds;

        /// <summary>
        ///     Set up networkIdRecycling time
        /// </summary>
        /// <param name="time">Second</param>
        public static void SetNetworkIdRecycleDelay(float time) => Config.NetworkIdRecycleDelay = time;

        /// <summary>
        ///     Set usage time to resynchronize
        /// </summary>
        /// <param name="enableTimeResync">Use time to resynchronize</param>
        public static void SetEnableTimeResync(bool enableTimeResync) => Config.EnableTimeResync = enableTimeResync;

        /// <summary>
        ///     Set time resynchronization interval
        /// </summary>
        /// <param name="timeResyncInterval">Time Resynchronization Interval</param>
        public static void SetTimeResyncInterval(int timeResyncInterval) => Config.TimeResyncInterval = timeResyncInterval;

        /// <summary>
        ///     Set upNetworkVariableIs the length required to be safe
        /// </summary>
        /// <param name="ensureNetworkVariableLengthSafety">NetworkVariableIs the length required to be safe</param>
        public static void SetEnsureNetworkVariableLengthSafety(bool ensureNetworkVariableLengthSafety) => Config.EnsureNetworkVariableLengthSafety = ensureNetworkVariableLengthSafety;

        /// <summary>
        ///     Set whether to enable or notSceneManagement
        /// </summary>
        /// <param name="enableSceneManagement">Is it enabledSceneManagement</param>
        public static void SetEnableSceneManagement(bool enableSceneManagement) => Config.EnableSceneManagement = enableSceneManagement;

        /// <summary>
        ///     Set loading scene timeout
        /// </summary>
        /// <param name="loadSceneTimeout">Second</param>
        public static void SetLoadSceneTimeout(int loadSceneTimeout) => Config.LoadSceneTimeOut = loadSceneTimeout;

        /// <summary>
        ///     Set generation timeout
        /// </summary>
        /// <param name="spawnTimeout">Second</param>
        public static void SetSpawnTimeout(float spawnTimeout) => Config.SpawnTimeout = spawnTimeout;

        /// <summary>
        ///     Set maximumPayloadCapacity
        /// </summary>
        /// <param name="maxPayloadSize">MaximumPayloadCapacity</param>
        public static void SetMaxPayloadSize(int maxPayloadSize) => Utp.MaxPayloadSize = maxPayloadSize;

        /// <summary>
        ///     Set maximumPacketQueueCapacity
        /// </summary>
        /// <param name="maxPacketQueueSize">MaximumPacketQueueCapacity</param>
        public static void SetMaxPacketQueueSize(int maxPacketQueueSize) => Utp.MaxPacketQueueSize = maxPacketQueueSize;

        /// <summary>
        ///     Set maximumSendQueueCapacity
        /// </summary>
        /// <param name="maxSendQueueSize">MaximumSendQueueCapacity</param>
        public static void SetMaxSendQueueSize(int maxSendQueueSize) => Utp.MaxSendQueueSize = maxSendQueueSize;

        /// <summary>
        ///     Set the maximum number of connection attempts
        /// </summary>
        /// <param name="maxConnectAttempts">Maximum Connection Attempts</param>
        public static void SetMaxConnectAttempts(int maxConnectAttempts) => Utp.MaxConnectAttempts = maxConnectAttempts;
    }
}