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
    ///     Leaving room information
    /// </summary>
    internal struct LeftRoomMessage : IMemoryPackable<LeftRoomMessage>, INetworkMessage
    {
        /// <summary>
        ///     RoomId
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="roomId">RoomId</param>
        public LeftRoomMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     Static construction
        /// </summary>
        static LeftRoomMessage() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<LeftRoomMessage>())
                MemoryPackFormatterProvider.Register(new LeftRoomMessageFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<LeftRoomMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<LeftRoomMessage>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref LeftRoomMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref LeftRoomMessage value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class LeftRoomMessageFormatter : MemoryPackFormatter<LeftRoomMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref LeftRoomMessage value) => LeftRoomMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref LeftRoomMessage value) => LeftRoomMessage.Deserialize(ref reader, ref value);
        }
    }
}