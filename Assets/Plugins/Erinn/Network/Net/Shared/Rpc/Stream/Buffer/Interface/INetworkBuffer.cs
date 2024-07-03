//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Buffer interface
    /// </summary>
    public interface INetworkBuffer : IDisposable
    {
        /// <summary>
        ///     Position
        /// </summary>
        int Position { get; }

        /// <summary>
        ///     Length
        /// </summary>
        int Length { get; }

        /// <summary>
        ///     Empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        ///     Surplus
        /// </summary>
        int Remaining { get; }

        /// <summary>
        ///     Capacity
        /// </summary>
        int Capacity { get; }

        /// <summary>
        ///     Offset
        /// </summary>
        int Offset { get; }

        /// <summary>
        ///     Count
        /// </summary>
        int Size { get; }

        /// <summary>
        ///     Can read
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        ///     Can write
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        void Advance(int count);

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        void AdvancePosition(int count);

        /// <summary>
        ///     Advance
        /// </summary>
        /// <param name="count">Count</param>
        void AdvanceLength(int count);

        /// <summary>
        ///     Set position
        /// </summary>
        /// <param name="position">Position</param>
        void SetPosition(int position);

        /// <summary>
        ///     Set length
        /// </summary>
        /// <param name="length">Length</param>
        void SetLength(int length);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="segment">Buffer zone</param>
        void SetBuffer(ArraySegment<byte> segment);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        void SetBuffer(byte[] bytes);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="count">Length</param>
        void SetBuffer(byte[] bytes, int count);

        /// <summary>
        ///     Set buffer
        /// </summary>
        /// <param name="bytes">Buffer zone</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Length</param>
        void SetBuffer(byte[] bytes, int offset, int count);

        /// <summary>
        ///     Empty
        /// </summary>
        void Flush();

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <returns>Copied count</returns>
        int CopyTo(byte[] dst);

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <returns>Copied count</returns>
        int CopyTo(byte[] dst, int srcOffset);

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="count">Count</param>
        /// <returns>Copied count</returns>
        int CopyTo(byte[] dst, int srcOffset, int count);

        /// <summary>
        ///     Copy to destination
        /// </summary>
        /// <param name="dst">Destination</param>
        /// <param name="srcOffset">Source offset</param>
        /// <param name="dstOffset">Destination offset</param>
        /// <param name="count">Count</param>
        /// <returns>Copied count</returns>
        int CopyTo(byte[] dst, int srcOffset, int dstOffset, int count);

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        Span<byte> AsSpan();

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        Memory<byte> AsMemory();

        /// <summary>
        ///     Get shards
        /// </summary>
        /// <returns>Obtained shards</returns>
        ArraySegment<byte> ToArraySegment();

        /// <summary>
        ///     Get array
        /// </summary>
        /// <returns>Obtained array</returns>
        byte[] ToArray();
    }
}