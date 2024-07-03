//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Server response data
    /// </summary>
    public struct DiscoveryResponseData : INetworkSerializable
    {
        /// <summary>
        ///     Address
        /// </summary>
        public string Address;

        /// <summary>
        ///     Port
        /// </summary>
        public ushort Port;

        /// <summary>
        ///     Server Name
        /// </summary>
        public string ServerName;

        /// <summary>
        ///     Serialize
        /// </summary>
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Address);
            serializer.SerializeValue(ref Port);
            serializer.SerializeValue(ref ServerName);
        }
    }
}