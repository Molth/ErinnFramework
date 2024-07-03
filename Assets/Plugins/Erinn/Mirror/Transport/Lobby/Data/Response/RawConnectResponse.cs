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
    ///     Connect
    /// </summary>
    internal struct RawConnectResponse : INetworkMessage, IMemoryPackable<RawConnectResponse>
    {
        /// <summary>
        ///     RemoteEndPoint
        /// </summary>
        public uint RemoteEndPoint;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="remoteEndPoint">RemoteEndPoint</param>
        public RawConnectResponse(uint remoteEndPoint) => RemoteEndPoint = remoteEndPoint;

        /// <summary>
        ///     Static construction
        /// </summary>
        static RawConnectResponse() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RawConnectResponse>())
                MemoryPackFormatterProvider.Register(new RawConnectPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RawConnectResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RawConnectResponse>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RawConnectResponse value) => writer.WriteUnmanaged(in value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RawConnectResponse value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RawConnectPacketFormatter : MemoryPackFormatter<RawConnectResponse>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RawConnectResponse value) => RawConnectResponse.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RawConnectResponse value) => RawConnectResponse.Deserialize(ref reader, ref value);
        }
    }
}