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
    ///     Create room request
    /// </summary>
    internal struct CreateRoomRequest : IMemoryPackable<CreateRoomRequest>, INetworkMessage
    {
        /// <summary>
        ///     Password
        /// </summary>
        public uint Password;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="password">Password</param>
        public CreateRoomRequest(uint password) => Password = password;

        /// <summary>
        ///     Static construction
        /// </summary>
        static CreateRoomRequest() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<CreateRoomRequest>())
                MemoryPackFormatterProvider.Register(new CreateRoomRequestFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<CreateRoomRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<CreateRoomRequest>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref CreateRoomRequest value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref CreateRoomRequest value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class CreateRoomRequestFormatter : MemoryPackFormatter<CreateRoomRequest>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref CreateRoomRequest value) => CreateRoomRequest.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref CreateRoomRequest value) => CreateRoomRequest.Deserialize(ref reader, ref value);
        }
    }
}