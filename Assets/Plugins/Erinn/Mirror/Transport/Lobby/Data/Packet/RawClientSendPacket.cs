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
    internal struct RawClientSendPacket : INetworkMessage, IMemoryPackable<RawClientSendPacket>
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
        public RawClientSendPacket(uint remoteEndPoint, ArraySegment<byte> payload)
        {
            RemoteEndPoint = remoteEndPoint;
            Payload = payload;
        }

        /// <summary>
        ///     Static construction
        /// </summary>
        static RawClientSendPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RawClientSendPacket>())
                MemoryPackFormatterProvider.Register(new RawClientSendPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RawClientSendPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RawClientSendPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>>())
                MemoryPackFormatterProvider.Register(new ArraySegmentFormatter<byte>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RawClientSendPacket value)
        {
            writer.WriteUnmanagedWithObjectHeader(2, in value.RemoteEndPoint);
            writer.WriteSpan(value.Payload.AsSpan());
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RawClientSendPacket value)
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new RawClientSendPacket();
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
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(RawClientSendPacket), 2, memberCount);
                        return;
                    }

                    if (memberCount != 0)
                    {
                        reader.ReadUnmanaged(out remoteEndPoint);
                        if (memberCount != 1)
                            reader.ReadBytes(ref payload);
                    }
                }

                value = new RawClientSendPacket(remoteEndPoint, payload);
            }
        }

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RawClientSendPacketFormatter : MemoryPackFormatter<RawClientSendPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RawClientSendPacket value) => RawClientSendPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RawClientSendPacket value) => RawClientSendPacket.Deserialize(ref reader, ref value);
        }
    }
}