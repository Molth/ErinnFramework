//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2022_3_OR_NEWER || GODOT
using System.Buffers;
#endif
using System.Net;
using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS9074

// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable RedundantAssignment

namespace Erinn
{
    /// <summary>
    ///     EndPoint
    /// </summary>
    public struct ENetPunchEndPoint : INetworkMessage, IMemoryPackable<ENetPunchEndPoint>
    {
        /// <summary>
        ///     Address
        /// </summary>
        public string Address;

        /// <summary>
        ///     Port
        /// </summary>
        public ushort Port;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="port">Port</param>
        public ENetPunchEndPoint(string address, ushort port)
        {
            Address = address;
            Port = port;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="ipEndPoint">IPEndPoint</param>
        public ENetPunchEndPoint(IPEndPoint ipEndPoint)
        {
            Address = ipEndPoint.Address.ToString();
            Port = (ushort)ipEndPoint.Port;
        }

        /// <summary>
        ///     Create message
        /// </summary>
        /// <returns>Message</returns>
        public ENetPunchMessage Create() => new(Address, Port);

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Address.GetHashCode() ^ Port;

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the NetworkConnection value as a string</returns>
        public override string ToString() => Address + ":" + Port;

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if UNITY_2022_3_OR_NEWER || GODOT
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref ENetPunchEndPoint value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref ENetPunchEndPoint value)
#else
        static void IMemoryPackable<ENetPunchEndPoint>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref ENetPunchEndPoint value)
#endif
        {
            writer.WriteObjectHeader(2);
            writer.WriteString(value.Address);
            writer.WriteUnmanaged(in value.Port);
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
#if UNITY_2021_3_OR_NEWER || GODOT
        public static void Deserialize(ref MemoryPackReader reader, ref ENetPunchEndPoint value)
#else
        static void IMemoryPackable<ENetPunchEndPoint>.Deserialize(ref MemoryPackReader reader, scoped ref ENetPunchEndPoint value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new ENetPunchEndPoint();
            }
            else
            {
                string address;
                ushort port;
                if (memberCount == 2)
                {
                    address = reader.ReadString();
                    reader.ReadUnmanaged(out port);
                }
                else
                {
                    if (memberCount > 2)
                    {
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(ENetPunchEndPoint), 2, memberCount);
                        return;
                    }

                    address = null;
                    port = 0;
                    if (memberCount != 0)
                    {
                        address = reader.ReadString();
                        if (memberCount != 1)
                            reader.ReadUnmanaged(out port);
                    }
                }

                value = new ENetPunchEndPoint(address, port);
            }
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static ENetPunchEndPoint() => MemoryPackFormatterProvider.Register<ENetPunchEndPoint>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchEndPoint>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<ENetPunchEndPoint>());
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchEndPoint[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ENetPunchEndPoint>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static ENetPunchEndPoint() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchEndPoint>())
                MemoryPackFormatterProvider.Register(new ENetPunchEndPointFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchEndPoint[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ENetPunchEndPoint>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class ENetPunchEndPointFormatter : MemoryPackFormatter<ENetPunchEndPoint>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref ENetPunchEndPoint value) => ENetPunchEndPoint.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref ENetPunchEndPoint value) => ENetPunchEndPoint.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class ENetPunchEndPointFormatter : MemoryPackFormatter<ENetPunchEndPoint>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref ENetPunchEndPoint value) => ENetPunchEndPoint.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref ENetPunchEndPoint value) => ENetPunchEndPoint.Deserialize(ref reader, ref value);
        }
#endif
    }
}