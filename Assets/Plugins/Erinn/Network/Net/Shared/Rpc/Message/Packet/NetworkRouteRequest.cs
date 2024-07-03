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
    public struct NetworkRouteRequest : INetworkMessage, IMemoryPackable<NetworkRouteRequest>
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
        public NetworkRouteRequest(uint command)
        {
            Command = command;
            Payload = default;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="payload">Value</param>
        public NetworkRouteRequest(uint command, ArraySegment<byte> payload)
        {
            Command = command;
            Payload = payload;
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkRouteRequest value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkRouteRequest value)
#else
        static void IMemoryPackable<NetworkRouteRequest>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkRouteRequest value)
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
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkRouteRequest value)
#else
        static void IMemoryPackable<NetworkRouteRequest>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkRouteRequest value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkRouteRequest), 2, memberCount);
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
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkRouteRequest), 2, memberCount);
                    return;
                }

                if (memberCount != 0)
                {
                    reader.ReadUnmanaged(out command);
                    if (memberCount != 1)
                        reader.ReadBytes(ref payload);
                }
            }

            value = new NetworkRouteRequest(command, payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkRouteRequest() => MemoryPackFormatterProvider.Register<NetworkRouteRequest>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteRequest>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkRouteRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkRouteRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkRouteRequest() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteRequest>())
                MemoryPackFormatterProvider.Register(new NetworkRouteRequestFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkRouteRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkRouteRequestFormatter : MemoryPackFormatter<NetworkRouteRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkRouteRequest value) => NetworkRouteRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkRouteRequest value) => NetworkRouteRequest.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkRouteRequestFormatter : MemoryPackFormatter<NetworkRouteRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkRouteRequest value) => NetworkRouteRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkRouteRequest value) => NetworkRouteRequest.Deserialize(ref reader, ref value);
        }
#endif
    }
}