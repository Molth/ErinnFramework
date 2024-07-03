//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Networking.Transport.Relay;

namespace Erinn
{
    /// <summary>
    ///     Online system
    /// </summary>
    public static partial class NetcodeSystem
    {
        /// <summary>
        ///     Set up server relay data
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="allocationIdBytes">AllocationIndex</param>
        /// <param name="keyBytes">AllocationKey</param>
        /// <param name="connectionDataBytes">Server Connection Data</param>
        public static void SetRelayServerData(string ipv4Address, ushort port, byte[] allocationIdBytes, byte[] keyBytes, byte[] connectionDataBytes) => Utp.SetRelayServerData(ipv4Address, port, allocationIdBytes, keyBytes, connectionDataBytes);

        /// <summary>
        ///     Set up server relay data
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="allocationIdBytes">AllocationIndex</param>
        /// <param name="keyBytes">AllocationKey</param>
        /// <param name="connectionDataBytes">Server Connection Data</param>
        /// <param name="hostConnectionDataBytes">Host Connection Data</param>
        public static void SetRelayServerData(string ipv4Address, ushort port, byte[] allocationIdBytes, byte[] keyBytes, byte[] connectionDataBytes, byte[] hostConnectionDataBytes) => Utp.SetRelayServerData(ipv4Address, port, allocationIdBytes, keyBytes, connectionDataBytes, hostConnectionDataBytes);

        /// <summary>
        ///     Set up server relay data
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="allocationIdBytes">AllocationIndex</param>
        /// <param name="keyBytes">AllocationKey</param>
        /// <param name="connectionDataBytes">Server Connection Data</param>
        /// <param name="hostConnectionDataBytes">Host Connection Data</param>
        /// <param name="isSecure">Is it safe</param>
        public static void SetRelayServerData(string ipv4Address, ushort port, byte[] allocationIdBytes, byte[] keyBytes, byte[] connectionDataBytes, byte[] hostConnectionDataBytes, bool isSecure) => Utp.SetRelayServerData(ipv4Address, port, allocationIdBytes, keyBytes, connectionDataBytes, hostConnectionDataBytes, isSecure);

        /// <summary>
        ///     Set up server relay data
        /// </summary>
        /// <param name="serverData">Server data</param>
        public static void SetRelayServerData(RelayServerData serverData) => Utp.SetRelayServerData(serverData);

        /// <summary>
        ///     Set host relay data
        /// </summary>
        /// <param name="ipAddress">Host address</param>
        /// <param name="port">Host Port</param>
        /// <param name="allocationId">AllocationIndex</param>
        /// <param name="key">AllocationKey</param>
        /// <param name="connectionData">Host Connection Data</param>
        public static void SetHostRelayData(string ipAddress, ushort port, byte[] allocationId, byte[] key, byte[] connectionData) => Utp.SetHostRelayData(ipAddress, port, allocationId, key, connectionData);

        /// <summary>
        ///     Set host relay data
        /// </summary>
        /// <param name="ipAddress">Host address</param>
        /// <param name="port">Host Port</param>
        /// <param name="allocationId">AllocationIndex</param>
        /// <param name="key">AllocationKey</param>
        /// <param name="connectionData">Host Connection Data</param>
        /// <param name="isSecure">Is it safe</param>
        public static void SetHostRelayData(string ipAddress, ushort port, byte[] allocationId, byte[] key, byte[] connectionData, bool isSecure) => Utp.SetHostRelayData(ipAddress, port, allocationId, key, connectionData, isSecure);

        /// <summary>
        ///     Set up client relay data
        /// </summary>
        /// <param name="ipAddress">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="allocationId">AllocationIndex</param>
        /// <param name="key">AllocationKey</param>
        /// <param name="connectionData">Client Connection Data</param>
        /// <param name="hostConnectionData">Host Connection Data</param>
        public static void SetClientRelayData(string ipAddress, ushort port, byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData) => Utp.SetClientRelayData(ipAddress, port, allocationId, key, connectionData, hostConnectionData);

        /// <summary>
        ///     Set up client relay data
        /// </summary>
        /// <param name="ipAddress">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="allocationId">AllocationIndex</param>
        /// <param name="key">AllocationKey</param>
        /// <param name="connectionData">Client Connection Data</param>
        /// <param name="hostConnectionData">Host Connection Data</param>
        /// <param name="isSecure">Is it safe</param>
        public static void SetClientRelayData(string ipAddress, ushort port, byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, bool isSecure) => Utp.SetClientRelayData(ipAddress, port, allocationId, key, connectionData, hostConnectionData, isSecure);
    }
}