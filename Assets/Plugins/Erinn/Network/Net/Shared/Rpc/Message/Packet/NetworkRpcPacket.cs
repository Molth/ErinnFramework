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
    ///     Network rpc packet
    /// </summary>
    public struct NetworkRpcPacket : INetworkMessage, IMemoryPackable<NetworkRpcPacket>
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
        public NetworkRpcPacket(uint command)
        {
            Command = command;
            Payload = default;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="payload">Value</param>
        public NetworkRpcPacket(uint command, ArraySegment<byte> payload)
        {
            Command = command;
            Payload = payload;
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkRpcPacket value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkRpcPacket value)
#else
        static void IMemoryPackable<NetworkRpcPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkRpcPacket value)
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
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkRpcPacket value)
#else
        static void IMemoryPackable<NetworkRpcPacket>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkRpcPacket value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkRpcPacket), 2, memberCount);
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
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkRpcPacket), 2, memberCount);
                    return;
                }

                if (memberCount != 0)
                {
                    reader.ReadUnmanaged(out command);
                    if (memberCount != 1)
                        reader.ReadBytes(ref payload);
                }
            }

            value = new NetworkRpcPacket(command, payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkRpcPacket() => MemoryPackFormatterProvider.Register<NetworkRpcPacket>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRpcPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkRpcPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRpcPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkRpcPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkRpcPacket() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRpcPacket>())
                MemoryPackFormatterProvider.Register(new NetworkRpcPacketFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRpcPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkRpcPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkRpcPacketFormatter : MemoryPackFormatter<NetworkRpcPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkRpcPacket value) => NetworkRpcPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkRpcPacket value) => NetworkRpcPacket.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkRpcPacketFormatter : MemoryPackFormatter<NetworkRpcPacket>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkRpcPacket value) => NetworkRpcPacket.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkRpcPacket value) => NetworkRpcPacket.Deserialize(ref reader, ref value);
        }
#endif
    }
}