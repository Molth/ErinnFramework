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
    ///     Disconnect
    /// </summary>
    internal struct RawClientDisconnectPacket : INetworkMessage, IMemoryPackable<RawClientDisconnectPacket>
    {
        /// <summary>
        ///     RemoteEndPoint
        /// </summary>
        public uint RemoteEndPoint;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="remoteEndPoint">RemoteEndPoint</param>
        public RawClientDisconnectPacket(uint remoteEndPoint) => RemoteEndPoint = remoteEndPoint;

        /// <summary>
        ///     Static construction
        /// </summary>
        static RawClientDisconnectPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RawClientDisconnectPacket>())
                MemoryPackFormatterProvider.Register(new RawClientDisconnectPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RawClientDisconnectPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RawClientDisconnectPacket>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RawClientDisconnectPacket value) => writer.WriteUnmanaged(in value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RawClientDisconnectPacket value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RawClientDisconnectPacketFormatter : MemoryPackFormatter<RawClientDisconnectPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RawClientDisconnectPacket value) => RawClientDisconnectPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RawClientDisconnectPacket value) => RawClientDisconnectPacket.Deserialize(ref reader, ref value);
        }
    }
}