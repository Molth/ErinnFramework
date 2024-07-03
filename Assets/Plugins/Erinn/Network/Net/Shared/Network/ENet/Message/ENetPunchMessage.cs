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
    public struct ENetPunchMessage : INetworkMessage, IMemoryPackable<ENetPunchMessage>
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
        public ENetPunchMessage(string address, ushort port)
        {
            Address = address;
            Port = port;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="ipEndPoint">IPEndPoint</param>
        public ENetPunchMessage(IPEndPoint ipEndPoint)
        {
            Address = ipEndPoint.Address.ToString();
            Port = (ushort)ipEndPoint.Port;
        }

        /// <summary>
        ///     Create message
        /// </summary>
        /// <returns>Message</returns>
        public ENetPunchEndPoint Create() => new(Address, Port);

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
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref ENetPunchMessage value) where TBufferWriter : class, IBufferWriter<byte>
#elif UNITY_2021_3_OR_NEWER
        public static void Serialize(ref MemoryPackWriter writer, ref ENetPunchMessage value)
#else
        static void IMemoryPackable<ENetPunchMessage>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref ENetPunchMessage value)
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
        public static void Deserialize(ref MemoryPackReader reader, ref ENetPunchMessage value)
#else
        static void IMemoryPackable<ENetPunchMessage>.Deserialize(ref MemoryPackReader reader, scoped ref ENetPunchMessage value)
#endif
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new ENetPunchMessage();
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
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(ENetPunchMessage), 2, memberCount);
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

                value = new ENetPunchMessage(address, port);
            }
        }
#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Static construction
        /// </summary>
        static ENetPunchMessage() => MemoryPackFormatterProvider.Register<ENetPunchMessage>();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchMessage>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<ENetPunchMessage>());
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ENetPunchMessage>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#else
        /// <summary>
        ///     Static construction
        /// </summary>
        static ENetPunchMessage() => RegisterFormatter();

        /// <summary>
        ///     Register
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchMessage>())
                MemoryPackFormatterProvider.Register(new ENetPunchMessageFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<ENetPunchMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<ENetPunchMessage>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }
#endif

#if UNITY_2022_3_OR_NEWER || GODOT
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class ENetPunchMessageFormatter : MemoryPackFormatter<ENetPunchMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, ref ENetPunchMessage value) => ENetPunchMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref ENetPunchMessage value) => ENetPunchMessage.Deserialize(ref reader, ref value);
        }
#elif UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class ENetPunchMessageFormatter : MemoryPackFormatter<ENetPunchMessage>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref ENetPunchMessage value) => ENetPunchMessage.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref ENetPunchMessage value) => ENetPunchMessage.Deserialize(ref reader, ref value);
        }
#endif
    }
}