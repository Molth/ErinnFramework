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
    ///     Add room information
    /// </summary>
    internal struct JoinedRoomMessage : IMemoryPackable<JoinedRoomMessage>, INetworkMessage
    {
        /// <summary>
        ///     RoomId
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="roomId">RoomId</param>
        public JoinedRoomMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     Static construction
        /// </summary>
        static JoinedRoomMessage() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinedRoomMessage>())
                MemoryPackFormatterProvider.Register(new JoinedRoomMessageFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinedRoomMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinedRoomMessage>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref JoinedRoomMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref JoinedRoomMessage value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class JoinedRoomMessageFormatter : MemoryPackFormatter<JoinedRoomMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref JoinedRoomMessage value) => JoinedRoomMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref JoinedRoomMessage value) => JoinedRoomMessage.Deserialize(ref reader, ref value);
        }
    }
}