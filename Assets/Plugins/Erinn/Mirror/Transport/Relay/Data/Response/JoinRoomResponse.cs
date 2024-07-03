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
    ///     Join room response
    /// </summary>
    internal struct JoinRoomResponse : IMemoryPackable<JoinRoomResponse>, INetworkMessage
    {
        /// <summary>
        ///     Whether successful
        /// </summary>
        public bool Success;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="success">Whether successful</param>
        public JoinRoomResponse(bool success) => Success = success;

        /// <summary>
        ///     Static construction
        /// </summary>
        static JoinRoomResponse() => RegisterFormatter();

        /// <summary>
        ///     Register Serializer
        /// </summary>
        [Preserve]
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomResponse>())
                MemoryPackFormatterProvider.Register(new JoinRoomResponseFormatter());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinRoomResponse>());
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
        public static void Serialize(ref MemoryPackWriter writer, ref JoinRoomResponse value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public static void Deserialize(ref MemoryPackReader reader, ref JoinRoomResponse value) => reader.ReadUnmanaged(out value);

        /// <summary>
        ///     Serializer
        /// </summary>
        [Preserve]
        private sealed class JoinRoomResponseFormatter : MemoryPackFormatter<JoinRoomResponse>
        {
            /// <summary>
            ///     Serialize
            /// </summary>
            [Preserve]
            public override void Serialize(ref MemoryPackWriter writer, ref JoinRoomResponse value) => JoinRoomResponse.Serialize(ref writer, ref value);

            /// <summary>
            ///     Deserialize
            /// </summary>
            [Preserve]
            public override void Deserialize(ref MemoryPackReader reader, ref JoinRoomResponse value) => JoinRoomResponse.Deserialize(ref reader, ref value);
        }
    }
}