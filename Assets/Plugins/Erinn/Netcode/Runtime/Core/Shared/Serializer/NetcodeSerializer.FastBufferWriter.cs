//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;
using Unity.Collections;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static partial class NetcodeSerializer
    {
        /// <summary>
        ///     Obtain FastBufferWriter
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained FastBufferWriter</returns>
        public static FastBufferWriter GetMessageWriter<T>(T message) where T : struct => GetWriter(Serialize(message));

        /// <summary>
        ///     Obtain FastBufferWriter
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained FastBufferWriter</returns>
        public static FastBufferWriter GetMessageWriter<T>(T message, Allocator allocator) where T : struct => GetWriter(Serialize(message), allocator);

        /// <summary>
        ///     Obtain FastBufferWriter
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained FastBufferWriter</returns>
        public static FastBufferWriter GetEventWriter<T>(T message) where T : struct, IMemoryPackable<T> => GetWriter(Serialize(message));

        /// <summary>
        ///     Obtain FastBufferWriter
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained FastBufferWriter</returns>
        public static FastBufferWriter GetEventWriter<T>(T message, Allocator allocator) where T : struct, IMemoryPackable<T> => GetWriter(Serialize(message), allocator);

        /// <summary>
        ///     Obtain Bytes FastBufferWriter
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Obtained FastBufferWriter</returns>
        public static FastBufferWriter GetBytesWriter(byte[] bytes) => bytes == null || bytes.Length == 0 ? new FastBufferWriter(0, Allocator.Temp) : GetWriter(bytes);

        /// <summary>
        ///     Obtain Bytes FastBufferWriter
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <param name="allocator">Allocation type</param>
        /// <returns>Obtained FastBufferWriter</returns>
        public static FastBufferWriter GetBytesWriter(byte[] bytes, Allocator allocator) => bytes == null || bytes.Length == 0 ? new FastBufferWriter(0, Allocator.Temp) : GetWriter(bytes, allocator);

        /// <summary>
        ///     Obtain Bytes FastBufferWriter
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Obtained FastBufferWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FastBufferWriter GetWriter(byte[] bytes)
        {
            var size = 4 + bytes.Length;
            var messageStream = new FastBufferWriter(size, Allocator.Temp);
            WriteBytes(messageStream, bytes, size);
            return messageStream;
        }

        /// <summary>
        ///     Obtain Bytes FastBufferWriter
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <param name="allocator">Allocation type</param>
        /// <returns>Obtained FastBufferWriter</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FastBufferWriter GetWriter(byte[] bytes, Allocator allocator)
        {
            var size = 4 + bytes.Length;
            var messageStream = new FastBufferWriter(size, allocator);
            WriteBytes(messageStream, bytes, size);
            return messageStream;
        }

        /// <summary>
        ///     Write Bytes Value
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="bytes">bytes</param>
        /// <param name="size">Capacity</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void WriteBytes(FastBufferWriter messageStream, byte[] bytes, int size)
        {
            messageStream.TryBeginWriteInternal(size);
            fixed (byte* ptr = bytes)
            {
                messageStream.WriteBytes(ptr, size);
            }
        }
    }
}