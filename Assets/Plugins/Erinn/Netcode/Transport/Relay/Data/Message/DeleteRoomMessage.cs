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
    ///     Delete room information
    /// </summary>
    internal struct DeleteRoomMessage : IMemoryPackable<DeleteRoomMessage>, INetworkMessage
    {
        /// <summary>
        ///     Static construction
        /// </summary>
        static DeleteRoomMessage() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<DeleteRoomMessage>())
                MemoryPackFormatterProvider.Register(new DeleteRoomMessageFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<DeleteRoomMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<DeleteRoomMessage>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref DeleteRoomMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref DeleteRoomMessage value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class DeleteRoomMessageFormatter : MemoryPackFormatter<DeleteRoomMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref DeleteRoomMessage value) => DeleteRoomMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref DeleteRoomMessage value) => DeleteRoomMessage.Deserialize(ref reader, ref value);
        }
    }
}