//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace Erinn
{
    /// <summary>
    ///     Transfer Data Packet
    /// </summary>
    internal struct RelayPacket : IMemoryPackable<RelayPacket>, INetworkMessage
    {
        /// <summary>
        ///     RoomId
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     Payload
        /// </summary>
        public ArraySegment<byte> Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="roomId">RoomId</param>
        /// <param name="payload">Payload</param>
        public RelayPacket(uint roomId, ArraySegment<byte> payload)
        {
            RoomId = roomId;
            Payload = payload;
        }

        /// <summary>
        ///     Static construction
        /// </summary>
        static RelayPacket() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RelayPacket>())
                MemoryPackFormatterProvider.Register(new RelayPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RelayPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RelayPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>>())
                MemoryPackFormatterProvider.Register(new ArraySegmentFormatter<byte>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ArraySegment<byte>>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RelayPacket value)
        {
            writer.WriteUnmanagedWithObjectHeader(2, value.RoomId);
            writer.WriteSpan(value.Payload.AsSpan());
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RelayPacket value)
        {
            if (!reader.TryReadObjectHeader(out var num))
            {
                value = new RelayPacket();
            }
            else
            {
                var roomId = value.RoomId;
                var payload = value.Payload;
                if (num == 2)
                {
                    reader.ReadUnmanaged(out roomId);
                    reader.ReadBytes(ref payload);
                }
                else
                {
                    if (num > 2)
                    {
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(RelayPacket), 2, num);
                        return;
                    }

                    if (num != 0)
                    {
                        reader.ReadUnmanaged(out roomId);
                        if (num != 1)
                            reader.ReadBytes(ref payload);
                    }
                }

                value = new RelayPacket(roomId, payload);
            }
        }

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RelayPacketFormatter : MemoryPackFormatter<RelayPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RelayPacket value) => RelayPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RelayPacket value) => RelayPacket.Deserialize(ref reader, ref value);
        }
    }
}