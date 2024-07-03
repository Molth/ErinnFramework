//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network expansion
    /// </summary>
    public static partial class NetcodeExtensions
    {
        /// <summary>
        ///     Write Bytes Value
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public static void WriteBytesValue<T>(this FastBufferWriter messageStream, T obj)
        {
            var bytes = NetcodeSerializer.Serialize(obj);
            WriteBytes(messageStream, bytes);
        }

        /// <summary>
        ///     Write Bytes Value
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="obj">object</param>
        public static void WriteObjectBytesValue(this FastBufferWriter messageStream, object obj)
        {
            var bytes = NetcodeSerializer.SerializeObject(obj);
            WriteBytes(messageStream, bytes);
        }

        /// <summary>
        ///     Write Bytes Value
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="bytes">bytes</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void WriteBytes(FastBufferWriter messageStream, byte[] bytes)
        {
            var size = 4 + bytes.Length;
            messageStream.TryBeginWriteInternal(size);
            fixed (byte* ptr = bytes)
            {
                messageStream.WriteBytes(ptr, size);
            }
        }
    }
}