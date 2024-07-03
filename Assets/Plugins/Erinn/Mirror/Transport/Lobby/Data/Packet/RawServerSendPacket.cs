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
    ///     Send
    /// </summary>
    internal struct RawServerSendPacket : INetworkMessage, IMemoryPackable<RawServerSendPacket>
    {
        /// <summary>
        ///     RemoteEndPoint
        /// </summary>
        public uint RemoteEndPoint;

        /// <summary>
        ///     Payload
        /// </summary>
        public ArraySegment<byte> Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="remoteEndPoint">RemoteEndPoint</param>
        /// <param name="payload">Payload</param>
        public RawServerSendPacket(uint remoteEndPoint, ArraySegment<byte> payload)
        {
            RemoteEndPoint = remoteEndPoint;
            Payload = payload;
        }

        /// <summary>
        ///     Static construction
        /// </summary>
        static RawServerSendPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RawServerSendPacket>())
                MemoryPackFormatterProvider.Register(new RawSendPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RawServerSendPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RawServerSendPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>>())
                MemoryPackFormatterProvider.Register(new ArraySegmentFormatter<byte>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RawServerSendPacket value)
        {
            writer.WriteUnmanagedWithObjectHeader(2, in value.RemoteEndPoint);
            writer.WriteSpan(value.Payload.AsSpan());
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RawServerSendPacket value)
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new RawServerSendPacket();
            }
            else
            {
                var remoteEndPoint = value.RemoteEndPoint;
                var payload = value.Payload;
                if (memberCount == 2)
                {
                    reader.ReadUnmanaged(out remoteEndPoint);
                    reader.ReadBytes(ref payload);
                }
                else
                {
                    if (memberCount > 2)
                    {
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(RawServerSendPacket), 2, memberCount);
                        return;
                    }

                    if (memberCount != 0)
                    {
                        reader.ReadUnmanaged(out remoteEndPoint);
                        if (memberCount != 1)
                            reader.ReadBytes(ref payload);
                    }
                }

                value = new RawServerSendPacket(remoteEndPoint, payload);
            }
        }

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RawSendPacketFormatter : MemoryPackFormatter<RawServerSendPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RawServerSendPacket value) => RawServerSendPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RawServerSendPacket value) => RawServerSendPacket.Deserialize(ref reader, ref value);
        }
    }
}