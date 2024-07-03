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
// ReSharper disable RedundantAssignment
#endif

namespace Erinn
{
    /// <summary>
    ///     Network message
    /// </summary>
    public struct NetworkMessageRequest : INetworkMessage, IMemoryPackable<NetworkMessageRequest>
    {
        /// <summary>
        ///     SerialNumber
        /// </summary>
        public uint SerialNumber;

        /// <summary>
        ///     Payload
        /// </summary>
        public NetworkPacket Payload;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="payload">Payload</param>
        public NetworkMessageRequest(uint serialNumber, NetworkPacket payload)
        {
            SerialNumber = serialNumber;
            Payload = payload;
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkMessageRequest value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkMessageRequest value)
#else
        static void IMemoryPackable<NetworkMessageRequest>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkMessageRequest value)
#endif
        {
            writer.WriteUnmanagedWithObjectHeader(2, in value.SerialNumber);
            writer.WritePackable(in value.Payload);
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
#if UNITY_2021_3_OR_NEWER || GODOT
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkMessageRequest value)
#else
        static void IMemoryPackable<NetworkMessageRequest>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkMessageRequest value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkMessageRequest), 2, memberCount);
                return;
            }

            var serialNumber = 0U;
            var payload = value.Payload;
            if (memberCount == 2)
            {
                reader.ReadUnmanaged(out serialNumber);
                reader.ReadPackable(ref payload);
            }
            else
            {
                if (memberCount > 2)
                {
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkMessageRequest), 2, memberCount);
                    return;
                }

                if (memberCount != 0)
                {
                    reader.ReadUnmanaged(out serialNumber);
                    if (memberCount != 1)
                        reader.ReadPackable(ref payload);
                }
            }

            value = new NetworkMessageRequest(serialNumber, payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkMessageRequest() => MemoryPackFormatterProvider.Register<NetworkMessageRequest>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageRequest>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkMessageRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkMessageRequest>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkMessageRequest() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageRequest>())
                MemoryPackFormatterProvider.Register(new NetworkMessageRequestFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkMessageRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkMessageRequestFormatter : MemoryPackFormatter<NetworkMessageRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkMessageRequest value) => NetworkMessageRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkMessageRequest value) => NetworkMessageRequest.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkMessageRequestFormatter : MemoryPackFormatter<NetworkMessageRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkMessageRequest value) => NetworkMessageRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkMessageRequest value) => NetworkMessageRequest.Deserialize(ref reader, ref value);
        }
#endif
    }
}