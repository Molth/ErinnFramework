//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif
using System.Net;
using System.Runtime.CompilerServices;
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
    ///     IPAddress formatter
    /// </summary>
    [Preserve]
    public sealed class IPAddressFormatter : MemoryPackFormatter<IPAddress>
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
                ref IPAddress? value)
#else
        public override void Serialize(ref MemoryPackWriter writer, ref IPAddress? value)
#endif
        {
            var buffer = NetworkBytesPool.Rent(16);
            try
            {
                value.TryWriteBytes(buffer, out var bytesWritten);
                writer.WriteSpan(buffer.AsSpan(0, bytesWritten));
            }
            finally
            {
                NetworkBytesPool.Return(buffer);
            }
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public override void Deserialize(ref MemoryPackReader reader,
#if !UNITY_2021_3_OR_NEWER && !GODOT
            scoped
#endif
                ref IPAddress? value)
        {
            if (!reader.TryReadCollectionHeader(out var length) || length == 0)
            {
                value = null;
            }
            else
            {
                Span<byte> span = stackalloc byte[length];
                ref var local = ref reader.GetSpanReference(length);
                Unsafe.CopyBlock(ref span[0], ref local, (uint)length);
                value = new IPAddress(span);
                reader.Advance(length);
            }
        }
    }
}