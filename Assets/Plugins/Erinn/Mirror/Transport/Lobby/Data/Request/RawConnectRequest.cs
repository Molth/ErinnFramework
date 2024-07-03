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
    internal struct RawConnectRequest : INetworkMessage, IMemoryPackable<RawConnectRequest>
    {
        /// <summary>
        ///     RemoteEndPoint
        /// </summary>
        public uint RemoteEndPoint;

        /// <summary>
        ///     Password
        /// </summary>
        public uint Password;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="remoteEndPoint">RemoteEndPoint</param>
        /// <param name="password">Password</param>
        public RawConnectRequest(uint remoteEndPoint, uint password)
        {
            RemoteEndPoint = remoteEndPoint;
            Password = password;
        }

        /// <summary>
        ///     Static construction
        /// </summary>
        static RawConnectRequest() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RawConnectRequest>())
                MemoryPackFormatterProvider.Register(new RawConnectPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<RawConnectRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RawConnectRequest>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref RawConnectRequest value) => writer.WriteUnmanaged(in value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref RawConnectRequest value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class RawConnectPacketFormatter : MemoryPackFormatter<RawConnectRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref RawConnectRequest value) => RawConnectRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref RawConnectRequest value) => RawConnectRequest.Deserialize(ref reader, ref value);
        }
    }
}