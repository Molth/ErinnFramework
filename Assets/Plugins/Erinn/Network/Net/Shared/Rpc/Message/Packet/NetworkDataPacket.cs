//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2022_3_OR_NEWER || GODOT
using System.Buffers;
#endif

using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS9074

#if UNITY_2021_3_OR_NEWER || GODOT
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable RedundantAssignment
#endif

namespace Erinn
{
    /// <summary>
    ///     Network data packet
    /// </summary>
    public struct NetworkDataPacket : INetworkMessage, IMemoryPackable<NetworkDataPacket>
    {
        /// <summary>
        ///     Load
        /// </summary>
        public ArraySegment<byte> Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="payload">Load</param>
        public NetworkDataPacket(ArraySegment<byte> payload) => Payload = payload;

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkDataPacket value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkDataPacket value)
#else
        static void IMemoryPackable<NetworkDataPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkDataPacket value)
#endif
        {
            writer.WriteObjectHeader(1);
            writer.WriteSpan(value.Payload.AsSpan());
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
#if UNITY_2021_3_OR_NEWER || GODOT
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkDataPacket value)
#else
        static void IMemoryPackable<NetworkDataPacket>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkDataPacket value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkDataPacket), 1, memberCount);
                return;
            }

            var payload = value.Payload;
            if (memberCount == 1)
            {
                reader.ReadBytes(ref payload);
            }
            else
            {
                if (memberCount > 1)
                {
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkDataPacket), 1, memberCount);
                    return;
                }

                if (memberCount != 0)
                    reader.ReadBytes(ref payload);
            }

            value = new NetworkDataPacket(payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkDataPacket() => MemoryPackFormatterProvider.Register<NetworkDataPacket>();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkDataPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkDataPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkDataPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkDataPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>>())
                MemoryPackFormatterProvider.Register(new ArraySegmentFormatter<byte>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ArraySegment<byte>>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkDataPacket() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkDataPacket>())
                MemoryPackFormatterProvider.Register(new NetworkDataPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkDataPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkDataPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>>())
                MemoryPackFormatterProvider.Register(new ArraySegmentFormatter<byte>());
            if (!MemoryPackFormatterProvider.IsRegistered<ArraySegment<byte>[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ArraySegment<byte>>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkDataPacketFormatter : MemoryPackFormatter<NetworkDataPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkDataPacket value) => NetworkDataPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkDataPacket value) => NetworkDataPacket.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkDataPacketFormatter : MemoryPackFormatter<NetworkDataPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkDataPacket value) => NetworkDataPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkDataPacket value) => NetworkDataPacket.Deserialize(ref reader, ref value);
        }
#endif
    }
}