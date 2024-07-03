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

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS9074

#if UNITY_2021_3_OR_NEWER || GODOT
using System;

// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable RedundantAssignment
#endif

namespace Erinn
{
    /// <summary>
    ///     Network packet
    /// </summary>
    public struct NetworkPacket : ISettable, IMemoryPackable<NetworkPacket>
    {
        /// <summary>
        ///     Command
        /// </summary>
        public uint Command;

        /// <summary>
        ///     Value
        /// </summary>
        public ArraySegment<byte> Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="payload">Value</param>
        public NetworkPacket(uint command, ArraySegment<byte> payload)
        {
            Command = command;
            Payload = payload;
        }

        /// <summary>
        ///     Is it valid
        /// </summary>
        public bool IsSet => Payload.Array != null;

        /// <summary>
        ///     Empty
        /// </summary>
        public static readonly NetworkPacket Empty = new(0U, ArraySegment<byte>.Empty);

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkPacket value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkPacket value)
#else
        static void IMemoryPackable<NetworkPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkPacket value)
#endif
        {
            writer.WriteUnmanagedWithObjectHeader(2, in value.Command);
            writer.WriteSpan(value.Payload.AsSpan());
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
#if UNITY_2021_3_OR_NEWER || GODOT
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkPacket value)
#else
        static void IMemoryPackable<NetworkPacket>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkPacket value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkPacket), 2, memberCount);
                return;
            }

            var command = 0U;
            var payload = value.Payload;
            if (memberCount == 2)
            {
                reader.ReadUnmanaged(out command);
                reader.ReadBytes(ref payload);
            }
            else
            {
                if (memberCount > 2)
                {
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkPacket), 2, memberCount);
                    return;
                }

                if (memberCount != 0)
                {
                    reader.ReadUnmanaged(out command);
                    if (memberCount != 1)
                        reader.ReadBytes(ref payload);
                }
            }

            value = new NetworkPacket(command, payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkPacket() => MemoryPackFormatterProvider.Register<NetworkPacket>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkPacket>())
                MemoryPackFormatterProvider.Register(new NetworkPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkPacketFormatter : MemoryPackFormatter<NetworkPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkPacket value) => NetworkPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkPacket value) => NetworkPacket.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkPacketFormatter : MemoryPackFormatter<NetworkPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkPacket value) => NetworkPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkPacket value) => NetworkPacket.Deserialize(ref reader, ref value);
        }
#endif
    }
}