//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Read
    /// </summary>
    public readonly partial struct NetworkReader : ISettable, INetworkBuffer, INetworkReader, IEquatable<NetworkReader>
    {
        /// <summary>
        ///     Buffer zone
        /// </summary>
        private readonly NetworkBuffer _buffer;

        /// <summary>
        ///     Position
        /// </summary>
        public int Position => _buffer.Position;

        /// <summary>
        ///     Length
        /// </summary>
        public int Length => _buffer.Length;

        /// <summary>
        ///     Empty
        /// </summary>
        public bool IsEmpty => _buffer.IsEmpty;

        /// <summary>
        ///     Surplus
        /// </summary>
        public int Remaining => _buffer.Remaining;

        /// <summary>
        ///     Capacity
        /// </summary>
        public int Capacity => _buffer.Capacity;

        /// <summary>
        ///     Offset
        /// </summary>
        public int Offset => _buffer.Offset;

        /// <summary>
        ///     Count
        /// </summary>
        public int Size => _buffer.Size;

        /// <summary>
        ///     Can read
        /// </summary>
        public bool CanRead => true;

        /// <summary>
        ///     Can write
        /// </summary>
        public bool CanWrite => false;

        /// <summary>
        ///     Is it valid
        /// </summary>
        public bool IsSet => _buffer != null;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        public NetworkReader(int capacity) => _buffer = new NetworkBuffer(capacity);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="segment">Buffer zone</param>
        public NetworkReader(ArraySegment<byte> segment) => _buffer = new NetworkBuffer(segment);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        public NetworkReader(byte[] bytes) => _buffer = new NetworkBuffer(bytes);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="count">Length</param>
        public NetworkReader(byte[] bytes, int count) => _buffer = new NetworkBuffer(bytes, count);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Length</param>
        public NetworkReader(byte[] bytes, int offset, int count) => _buffer = new NetworkBuffer(bytes, offset, count);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="memoryBuffer">Buffer zone</param>
        public NetworkReader(NetworkBuffer memoryBuffer) => _buffer = memoryBuffer;

        /// <summary>
        ///     Obtain NetworkWriter
        /// </summary>
        /// <returns>Obtained NetworkWriter</returns>
        public NetworkWriter ToWriter() => new(_buffer);

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        public void Advance(int count) => _buffer.Advance(count);

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        public void AdvancePosition(int count) => _buffer.AdvancePosition(count);

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        public void AdvanceLength(int count) => _buffer.AdvanceLength(count);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="segment">Buffer zone</param>
        public void SetBuffer(ArraySegment<byte> segment) => _buffer.SetBuffer(segment);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        public void SetBuffer(byte[] bytes) => _buffer.SetBuffer(bytes);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="count">Length</param>
        public void SetBuffer(byte[] bytes, int count) => _buffer.SetBuffer(bytes, count);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Length</param>
        public void SetBuffer(byte[] bytes, int offset, int count) => _buffer.SetBuffer(bytes, offset, count);

        /// <summary>
        ///     Set position
        /// </summary>
        /// <param name="position">Position</param>
        public void SetPosition(int position) => _buffer.SetPosition(position);

        /// <summary>
        ///     Set length
        /// </summary>
        /// <param name="length">Length</param>
        public void SetLength(int length) => _buffer.SetLength(length);

        /// <summary>
        ///     Release
        /// </summary>
        public void Flush() => _buffer.Flush();

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst) => _buffer.CopyTo(dst);

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst, int srcOffset) => _buffer.CopyTo(dst, srcOffset);

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="count">Count</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst, int srcOffset, int count) => _buffer.CopyTo(dst, srcOffset, count);

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        /// <returns>Copied count</returns>
        public int CopyTo(byte[] dst, int srcOffset, int dstOffset, int count) => _buffer.CopyTo(dst, srcOffset, dstOffset, count);

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        public Span<byte> AsSpan() => _buffer.AsSpan();

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        public Memory<byte> AsMemory() => _buffer.AsMemory();

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        public ArraySegment<byte> ToArraySegment() => _buffer.ToArraySegment();

        /// <summary>
        ///     Get array
        /// </summary>
        /// <returns>Obtained array</returns>
        public byte[] ToArray() => _buffer.ToArray();

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => _buffer.GetHashCode();

        /// <summary>
        ///     Get writer
        /// </summary>
        /// <returns>Obtained writer</returns>
        public static implicit operator NetworkWriter(NetworkReader reader) => reader.ToWriter();

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="other">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(NetworkReader other) => _buffer == other._buffer;

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public override bool Equals(object? obj) => obj is NetworkReader other && Equals(other);

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(NetworkReader left, NetworkReader right) => left._buffer == right._buffer;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(NetworkReader left, NetworkReader right) => left._buffer != right._buffer;

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose()
        {
            if (NetworkReaderPool.TryReturn(this))
                return;
            Flush();
        }
    }
}