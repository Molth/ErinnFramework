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
    public struct NetworkMessageResponse : INetworkMessage, IMemoryPackable<NetworkMessageResponse>
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
        public NetworkMessageResponse(uint serialNumber, NetworkPacket payload)
        {
            SerialNumber = serialNumber;
            Payload = payload;
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkMessageResponse value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref NetworkMessageResponse value)
#else
        static void IMemoryPackable<NetworkMessageResponse>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkMessageResponse value)
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
        public static void Deserialize(ref MemoryPackReader reader, ref NetworkMessageResponse value)
#else
        static void IMemoryPackable<NetworkMessageResponse>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkMessageResponse value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkMessageResponse), 2, memberCount);
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
                    MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkMessageResponse), 2, memberCount);
                    return;
                }

                if (memberCount != 0)
                {
                    reader.ReadUnmanaged(out serialNumber);
                    if (memberCount != 1)
                        reader.ReadPackable(ref payload);
                }
            }

            value = new NetworkMessageResponse(serialNumber, payload);
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkMessageResponse() => MemoryPackFormatterProvider.Register<NetworkMessageResponse>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageResponse>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkMessageResponse>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkMessageResponse>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static NetworkMessageResponse() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageResponse>())
                MemoryPackFormatterProvider.Register(new NetworkMessageResponseFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkMessageResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkMessageResponse>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkMessageResponseFormatter : MemoryPackFormatter<NetworkMessageResponse>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref NetworkMessageResponse value) => NetworkMessageResponse.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkMessageResponse value) => NetworkMessageResponse.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class NetworkMessageResponseFormatter : MemoryPackFormatter<NetworkMessageResponse>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref NetworkMessageResponse value) => NetworkMessageResponse.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref NetworkMessageResponse value) => NetworkMessageResponse.Deserialize(ref reader, ref value);
        }
#endif
    }
}