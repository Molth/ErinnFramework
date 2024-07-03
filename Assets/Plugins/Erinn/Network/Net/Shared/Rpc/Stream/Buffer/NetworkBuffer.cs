//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Buffers;
using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;

// ReSharper disable PossibleNullReferenceException
#endif

#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8603

namespace Erinn
{
    /// <summary>
    ///     Buffer zone
    /// </summary>
    public sealed class NetworkBuffer : INetworkBuffer, INetworkReader, INetworkWriter, IBufferWriter<byte>
    {
        /// <summary>
        ///     Buffer zone
        /// </summary>
        private ArraySegment<byte> _buffer;

        /// <summary>
        ///     Structure
        /// </summary>
        public NetworkBuffer() => _buffer = new byte[1024];

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        public NetworkBuffer(int capacity) => _buffer = new byte[capacity];

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="segment">Buffer zone</param>
        public NetworkBuffer(ArraySegment<byte> segment) => SetBuffer(segment);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        public NetworkBuffer(byte[] bytes) => SetBuffer(bytes);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="count">Length</param>
        public NetworkBuffer(byte[] bytes, int count) => SetBuffer(bytes, count);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Length</param>
        public NetworkBuffer(byte[] bytes, int offset, int count) => SetBuffer(bytes, offset, count);

        /// <summary>
        ///     Get Memory
        /// </summary>
        /// <param name="sizeHint">SizeHint</param>
        /// <returns>Memory</returns>
        public Memory<byte> GetMemory(int sizeHint = 0) => _buffer.AsMemory(Position, sizeHint);

        /// <summary>
        ///     Get Span
        /// </summary>
        /// <param name="sizeHint">SizeHint</param>
        /// <returns>Span</returns>
        public Span<byte> GetSpan(int sizeHint = 0) => _buffer.AsSpan(Position, sizeHint);

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        public void Advance(int count)
        {
            var newPosition = Position + count;
            if (newPosition < 0)
                MemoryPackSerializationException.ThrowInvalidRange(count, Position);
            var capacity = Capacity;
            if (capacity < count)
                MemoryPackSerializationException.ThrowInvalidRange(count, capacity);
            Position = newPosition;
        }

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        public void AdvancePosition(int count) => Position += count;

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        public void AdvanceLength(int count) => Length += count;

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose() => Flush();

        /// <summary>
        ///     Position
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        ///     Length
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        ///     Empty
        /// </summary>
        public bool IsEmpty => Position <= Length;

        /// <summary>
        ///     Surplus
        /// </summary>
        public int Remaining => Position - Length;

        /// <summary>
        ///     Capacity
        /// </summary>
        public int Capacity => _buffer.Count - Position;

        /// <summary>
        ///     Offset
        /// </summary>
        public int Offset => _buffer.Offset;

        /// <summary>
        ///     Count
        /// </summary>
        public int Size => _buffer.Count;

        /// <summary>
        ///     Can read
        /// </summary>
        public bool CanRead => true;

        /// <summary>
        ///     Can write
        /// </summary>
        public bool CanWrite => true;

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="segment">Buffer zone</param>
        public void SetBuffer(ArraySegment<byte> segment) => _buffer = segment;

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        public void SetBuffer(byte[] bytes) => _buffer = bytes;

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="count">Length</param>
        public void SetBuffer(byte[] bytes, int count) => _buffer = new ArraySegment<byte>(bytes, 0, count);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Length</param>
        public void SetBuffer(byte[] bytes, int offset, int count) => _buffer = new ArraySegment<byte>(bytes, offset, count);

        /// <summary>
        ///     Set position
        /// </summary>
        /// <param name="position">Position</param>
        public void SetPosition(int position) => Position = position;

        /// <summary>
        ///     Set length
        /// </summary>
        /// <param name="length">Length</param>
        public void SetLength(int length) => Length = length;

        /// <summary>
        ///     Release
        /// </summary>
        public void Flush()
        {
            Position = 0;
            Length = 0;
        }

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst)
        {
            if (dst == null)
                return -1;
            var array = _buffer.Array;
            if (array == null)
                return -1;
            var position = Position;
            if (position > dst.Length)
                return -1;
            NetworkSerializer.BlockCopy(array, _buffer.Offset, dst, 0, position);
            return position;
        }

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst, int srcOffset)
        {
            if (dst == null)
                return -1;
            var array = _buffer.Array;
            if (array == null || srcOffset < 0 || srcOffset > Position)
                return -1;
            var count = Position - srcOffset;
            if (count > dst.Length)
                return -1;
            NetworkSerializer.BlockCopy(array, _buffer.Offset + srcOffset, dst, 0, count);
            return count;
        }

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="count">Count</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst, int srcOffset, int count)
        {
            if (dst == null)
                return -1;
            var array = _buffer.Array;
            if (array == null || srcOffset < 0 || count < 0 || count > dst.Length || srcOffset + count > Position)
                return -1;
            NetworkSerializer.BlockCopy(array, _buffer.Offset + srcOffset, dst, 0, count);
            return count;
        }

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst, int srcOffset, int dstOffset, int count)
        {
            if (dst == null)
                return -1;
            var array = _buffer.Array;
            if (array == null || srcOffset < 0 || dstOffset < 0 || count < 0 || srcOffset + count > Position || dstOffset + count > dst.Length)
                return -1;
            NetworkSerializer.BlockCopy(array, _buffer.Offset + srcOffset, dst, dstOffset, count);
            return count;
        }

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        public Span<byte> AsSpan() => _buffer.AsSpan(0, Position);

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        public Memory<byte> AsMemory() => _buffer.AsMemory(0, Position);

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        public ArraySegment<byte> ToArraySegment() => _buffer[..Position];

        /// <summary>
        ///     Get array
        /// </summary>
        /// <returns>Obtained array</returns>
        public byte[] ToArray()
        {
            var array = _buffer.Array;
            if (array == null)
                throw new NullReferenceException("Buffer is null.");
            var length = Position;
#if !UNITY_2021_3_OR_NEWER && !GODOT
            var dst = GC.AllocateUninitializedArray<byte>(length);
#else
            var dst = new byte[length];
#endif
            NetworkSerializer.BlockCopy(array, _buffer.Offset, dst, 0, length);
            return dst;
        }

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Read<T>()
        {
            if (Remaining <= 0)
                MemoryPackSerializationException.ThrowSequenceReachedEnd();
            var obj = default(T);
            Length += MemoryPackSerializer.Deserialize(_buffer.AsSpan(Length), ref obj);
            return obj;
        }

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Read<T>(ref T obj)
        {
            if (Remaining <= 0)
                MemoryPackSerializationException.ThrowSequenceReachedEnd();
            Length += MemoryPackSerializer.Deserialize(_buffer.AsSpan(Length), ref obj);
        }

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T ReadUnsafe<T>() where T : unmanaged
        {
            fixed (byte* ptr = &_buffer.Array[_buffer.Offset + Length])
            {
                var result = *(T*)ptr;
                Length += Unsafe.SizeOf<T>();
                return result;
            }
        }

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ReadUnsafe<T>(ref T result) where T : unmanaged
        {
            fixed (byte* ptr = &_buffer.Array[_buffer.Offset + Length])
            {
                result = *(T*)ptr;
                Length += Unsafe.SizeOf<T>();
            }
        }

        /// <summary>
        ///     Write
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(in T obj) => MemoryPackSerializer.Serialize(this, in obj);

        /// <summary>
        ///     Write
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void WriteUnsafe<T>(in T obj) where T : unmanaged
        {
            fixed (byte* ptr = &_buffer.Array[_buffer.Offset + Position])
            {
                *(T*)ptr = obj;
                Position += Unsafe.SizeOf<T>();
            }
        }

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        public NetworkWriter ToWriter() => new(this);

        /// <summary>
        ///     Obtain NetworkReader
        /// </summary>
        /// <returns>Obtained NetworkReader</returns>
        public NetworkReader ToReader() => new(this);

        /// <summary>
        ///     Get writer
        /// </summary>
        /// <returns>Obtained writer</returns>
        public static implicit operator NetworkWriter(NetworkBuffer buffer) => new(buffer);

        /// <summary>
        ///     Get reader
        /// </summary>
        /// <returns>Obtained reader</returns>
        public static implicit operator NetworkReader(NetworkBuffer buffer) => new(buffer);
    }
}