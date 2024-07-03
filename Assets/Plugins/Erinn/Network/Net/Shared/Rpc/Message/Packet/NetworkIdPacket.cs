//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;
#if UNITY_2022_3_OR_NEWER || GODOT
using System.Buffers;
#endif

#pragma warning disable CS9074

#if UNITY_2021_3_OR_NEWER || GODOT
// ReSharper disable MemberHidesStaticFromOuterClass
#endif

namespace Erinn
{
    /// <summary>
    ///     Network Id packet
    /// </summary>
    public struct NetworkIdPacket : INetworkMessage, IMemoryPackable<NetworkIdPacket>
    {
        /// <summary>
        ///     Id
        /// </summary>
        public uint Id;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="id">Id</param>
        public NetworkIdPacket(uint id) => Id = id;

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkIdPacket value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkIdPacket value)
#else
        static void IMemoryPackable<NetworkIdPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkIdPacket value)
#endif
            => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
#if UNITY_2021_3_OR_NEWER || GODOT
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkIdPacket value)
#else
        static void IMemoryPackable<NetworkIdPacket>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkIdPacket value)
#endif
            => reader.ReadUnmanaged(out value);
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkIdPacket() => MemoryPackFormatterProvider.Register<NetworkIdPacket>();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkIdPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkIdPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkIdPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkIdPacket>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkIdPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkIdPacket>())
                MemoryPackFormatterProvider.Register(new NetworkIdPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkIdPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkIdPacket>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkIdPacketFormatter : MemoryPackFormatter<NetworkIdPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkIdPacket value) => NetworkIdPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkIdPacket value) => NetworkIdPacket.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkIdPacketFormatter : MemoryPackFormatter<NetworkIdPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkIdPacket value) => NetworkIdPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkIdPacket value) => NetworkIdPacket.Deserialize(ref reader, ref value);
        }
#endif
    }
}