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
    internal struct RawServerDisconnectPacket : INetworkMessage, IMemoryPackable<RawServerDisconnectPacket>
    {
        /// <summary>
        ///     RemoteEndPoint
        /// </summary>
        public uint RemoteEndPoint;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="remoteEndPoint">RemoteEndPoint</param>
        public RawServerDisconnectPacket(uint remoteEndPoint) => RemoteEndPoint = remoteEndPoint;

        /// <summary>
        ///     Static construction
        /// </summary>
        static RawServerDisconnectPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RawServerDisconnectPacket>())
                MemoryPackFormatterProvider.Register(new RawDisconnectPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RawServerDisconnectPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RawServerDisconnectPacket>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RawServerDisconnectPacket value) => writer.WriteUnmanaged(in value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RawServerDisconnectPacket value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RawDisconnectPacketFormatter : MemoryPackFormatter<RawServerDisconnectPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RawServerDisconnectPacket value) => RawServerDisconnectPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RawServerDisconnectPacket value) => RawServerDisconnectPacket.Deserialize(ref reader, ref value);
        }
    }
}