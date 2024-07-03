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
    internal struct RelayServerPacket : IMemoryPackable<RelayServerPacket>, INetworkMessage
    {
        /// <summary>
        ///     Payload
        /// </summary>
        public ArraySegment<byte> Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="payload">Payload</param>
        public RelayServerPacket(ArraySegment<byte> payload) => Payload = payload;

        /// <summary>
        ///     Static construction
        /// </summary>
        static RelayServerPacket() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RelayServerPacket>())
                MemoryPackFormatterProvider.Register(new RelayServerPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RelayServerPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RelayServerPacket>());
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
        public static void Serialize(ref MemoryPackWriter writer, ref RelayServerPacket value)
        {
            writer.WriteObjectHeader(1);
            writer.WriteSpan(value.Payload.AsSpan());
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RelayServerPacket value)
        {
            if (!reader.TryReadObjectHeader(out var num))
            {
                value = new RelayServerPacket();
            }
            else
            {
                var payload = value.Payload;
                if (num == 1)
                {
                    reader.ReadBytes(ref payload);
                }
                else
                {
                    if (num > 1)
                    {
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(RelayServerPacket), 1, num);
                        return;
                    }

                    if (num != 0)
                        reader.ReadBytes(ref payload);
                }

                value = new RelayServerPacket(payload);
            }
        }

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RelayServerPacketFormatter : MemoryPackFormatter<RelayServerPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RelayServerPacket value) => RelayServerPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RelayServerPacket value) => RelayServerPacket.Deserialize(ref reader, ref value);
        }
    }
}