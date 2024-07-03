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
    public struct NetworkRouteResponse : INetworkMessage, IMemoryPackable<NetworkRouteResponse>
    {
        /// <summary>
        ///     Load
        /// </summary>
        public ArraySegment<byte> Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="payload">Load</param>
        public NetworkRouteResponse(ArraySegment<byte> payload) => Payload = payload;

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkRouteResponse value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkRouteResponse value)
#else
        static void IMemoryPackable<NetworkRouteResponse>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkRouteResponse value)
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
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkRouteResponse value)
#else
        static void IMemoryPackable<NetworkRouteResponse>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkRouteResponse value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkRouteResponse), 1, memberCount);
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
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkRouteResponse), 1, memberCount);
                    return;
                }

                if (memberCount != 0)
                    reader.ReadBytes(ref payload);
            }

            value = new NetworkRouteResponse(payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkRouteResponse() => MemoryPackFormatterProvider.Register<NetworkRouteResponse>();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteResponse>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkRouteResponse>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkRouteResponse>());
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
        static NetworkRouteResponse() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteResponse>())
                MemoryPackFormatterProvider.Register(new NetworkRouteResponseFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkRouteResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkRouteResponse>());
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
        private sealed class NetworkRouteResponseFormatter : MemoryPackFormatter<NetworkRouteResponse>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkRouteResponse value) => NetworkRouteResponse.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkRouteResponse value) => NetworkRouteResponse.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkRouteResponseFormatter : MemoryPackFormatter<NetworkRouteResponse>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkRouteResponse value) => NetworkRouteResponse.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkRouteResponse value) => NetworkRouteResponse.Deserialize(ref reader, ref value);
        }
#endif
    }
}