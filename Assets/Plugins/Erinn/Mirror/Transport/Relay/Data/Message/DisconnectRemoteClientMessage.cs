//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace Erinn
{
    /// <summary>
    ///     Disconnect client connection information
    /// </summary>
    internal struct DisconnectRemoteClientMessage : IMemoryPackable<DisconnectRemoteClientMessage>, INetworkMessage
    {
        /// <summary>
        ///     RoomId
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="roomId">RoomId</param>
        public DisconnectRemoteClientMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     Static construction
        /// </summary>
        static DisconnectRemoteClientMessage() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<DisconnectRemoteClientMessage>())
                MemoryPackFormatterProvider.Register(new DisconnectRemoteClientMessageFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<DisconnectRemoteClientMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<DisconnectRemoteClientMessage>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref DisconnectRemoteClientMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref DisconnectRemoteClientMessage value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class DisconnectRemoteClientMessageFormatter : MemoryPackFormatter<DisconnectRemoteClientMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref DisconnectRemoteClientMessage value) => DisconnectRemoteClientMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref DisconnectRemoteClientMessage value) => DisconnectRemoteClientMessage.Deserialize(ref reader, ref value);
        }
    }
}