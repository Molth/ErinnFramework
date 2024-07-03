//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace Erinn
{
    /// <summary>
    ///     Join room request
    /// </summary>
    internal struct JoinRoomRequest : IMemoryPackable<JoinRoomRequest>, INetworkMessage
    {
        /// <summary>
        ///     HostId
        /// </summary>
        public uint HostId;

        /// <summary>
        ///     Password
        /// </summary>
        public uint Password;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="hostId">HostId</param>
        /// <param name="password">Password</param>
        public JoinRoomRequest(uint hostId, uint password)
        {
            HostId = hostId;
            Password = password;
        }

        /// <summary>
        ///     Static construction
        /// </summary>
        static JoinRoomRequest() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomRequest>())
                MemoryPackFormatterProvider.Register(new JoinRoomRequestFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinRoomRequest>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref JoinRoomRequest value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref JoinRoomRequest value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class JoinRoomRequestFormatter : MemoryPackFormatter<JoinRoomRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref JoinRoomRequest value) => JoinRoomRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref JoinRoomRequest value) => JoinRoomRequest.Deserialize(ref reader, ref value);
        }
    }
}