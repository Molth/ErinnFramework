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
    ///     Add room confirmation information
    /// </summary>
    internal struct JoinRoomAckMessage : IMemoryPackable<JoinRoomAckMessage>, INetworkMessage
    {
        /// <summary>
        ///     RoomId
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="roomId">RoomId</param>
        public JoinRoomAckMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     Static construction
        /// </summary>
        static JoinRoomAckMessage() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomAckMessage>())
                MemoryPackFormatterProvider.Register(new JoinRoomAckMessageFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomAckMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinRoomAckMessage>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref JoinRoomAckMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref JoinRoomAckMessage value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class JoinRoomAckMessageFormatter : MemoryPackFormatter<JoinRoomAckMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref JoinRoomAckMessage value) => JoinRoomAckMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref JoinRoomAckMessage value) => JoinRoomAckMessage.Deserialize(ref reader, ref value);
        }
    }
}