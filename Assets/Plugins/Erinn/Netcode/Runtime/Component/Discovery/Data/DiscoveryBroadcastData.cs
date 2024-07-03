//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Client Broadcast Data
    /// </summary>
    public struct DiscoveryBroadcastData : INetworkSerializable
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
        }
    }
}