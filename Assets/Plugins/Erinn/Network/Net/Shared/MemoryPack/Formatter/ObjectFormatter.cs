//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using MemoryPack.Internal;

#pragma warning disable CS8602
#pragma warning disable CS8632
#pragma warning disable CS9074

// ReSharper disable PossibleNullReferenceException

namespace Erinn
{
    /// <summary>
    ///     Object formatter
    /// </summary>
    [Preserve]
    public sealed class ObjectFormatter : MemoryPackFormatter<object>
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
                ref object? value)
#else
        public override void Serialize(ref MemoryPackWriter writer, ref object? value)
#endif
            => writer.WriteValue(value.GetType(), value);

        /// <summary>
        ///     Deserialize
        /// </summary>
        [Preserve]
        public override void Deserialize(ref MemoryPackReader reader,
#if !UNITY_2021_3_OR_NEWER && !GODOT
            scoped
#endif
                ref object? value) => reader.ReadValue(value.GetType(), ref value);
    }
}