//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;
using MemoryPack;
using MemoryPack.Internal;

#pragma warning disable CS8602
#pragma warning disable CS8604
#pragma warning disable CS8632
#pragma warning disable CS9074

// ReSharper disable PossibleNullReferenceException

namespace Erinn
{
    /// <summary>
    ///     IPEndPoint formatter
    /// </summary>
    [Preserve]
    public sealed class IPEndPointFormatter : MemoryPackFormatter<IPEndPoint>
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        [Preserve]
#if !UNITY_2021_3_OR_NEWER || UNITY_2022_3_OR_NEWER
        public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,
#if !UNITY_2022_3_OR_NEWER && !GODOT
            scoped
#endif
                ref IPEndPoint? value)
#else
        public override void Serialize(ref MemoryPackWriter writer, ref IPEndPoint? value)
#endif
        {
            writer.WriteUnmanaged(value.Port);
            writer.WriteValue(value.Address);
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public override void Deserialize(ref MemoryPackReader reader,
#if !UNITY_2021_3_OR_NEWER && !GODOT
            scoped
#endif
                ref IPEndPoint? value)
        {
            var port = reader.ReadUnmanaged<int>();
            var address = reader.ReadValue<IPAddress>();
            value = address == null ? null : new IPEndPoint(address, port);
        }
    }
}