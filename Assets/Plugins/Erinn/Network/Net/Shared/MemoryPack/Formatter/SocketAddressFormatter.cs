//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using MemoryPack;
using MemoryPack.Internal;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8602
#pragma warning disable CS8632
#pragma warning disable CS9074

// ReSharper disable PossibleNullReferenceException

namespace Erinn
{
    /// <summary>
    ///     SocketAddress formatter
    /// </summary>
    [Preserve]
    public sealed class SocketAddressFormatter : MemoryPackFormatter<SocketAddress>
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
                ref SocketAddress? value)
#else
        public override void Serialize(ref MemoryPackWriter writer, ref SocketAddress? value)
#endif
        {
            writer.WriteUnmanaged(value.Family);
            var buffer = NetworkBytesPool.Rent(16);
            try
            {
                for (var i = 0; i < value.Size; ++i)
                    buffer[i] = value[i];
                writer.WriteSpan(buffer.AsSpan(0, value.Size));
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
                ref SocketAddress? value)
        {
            var family = reader.ReadUnmanaged<AddressFamily>();
            if (!reader.TryReadCollectionHeader(out var length) || length == 0)
            {
                value = null;
            }
            else
            {
                value = new SocketAddress(family, length);
                Span<byte> span = stackalloc byte[length];
                ref var local = ref reader.GetSpanReference(length);
                Unsafe.CopyBlock(ref span[0], ref local, (uint)length);
                for (var i = 0; i < length; ++i)
                    value[i] = span[i];
                reader.Advance(length);
            }
        }
    }
}