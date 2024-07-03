//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network expansion
    /// </summary>
    public static partial class NetcodeExtensions
    {
        /// <summary>
        ///     Read Bytes
        /// </summary>
        /// <param name="messagePayload">FastBufferReader</param>
        /// <param name="bytes">bytes</param>
        public static unsafe void ReadBytes(this FastBufferReader messagePayload, out byte[] bytes)
        {
            var size = messagePayload.Length - messagePayload.Position;
            bytes = new byte[size];
            messagePayload.TryBeginRead(size);
            fixed (byte* ptr = bytes)
            {
                messagePayload.ReadBytes(ptr, size);
            }
        }

        /// <summary>
        ///     Read Bytes And deserialize it into specific values
        /// </summary>
        /// <param name="messagePayload">FastBufferReader</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        public static T ReadBytesValue<T>(this FastBufferReader messagePayload)
        {
            messagePayload.ReadBytes(out var bytes);
            return NetcodeSerializer.Deserialize<T>(bytes);
        }

        /// <summary>
        ///     Read Bytes And deserialize it into specific values
        /// </summary>
        /// <param name="messagePayload">FastBufferReader</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Whether the value was successfully read</returns>
        public static bool ReadBytesValue<T>(this FastBufferReader messagePayload, out T result)
        {
            try
            {
                messagePayload.ReadBytes(out var bytes);
                result = NetcodeSerializer.Deserialize<T>(bytes);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        ///     Read Bytes And deserialize it into specific values
        /// </summary>
        /// <param name="messagePayload">FastBufferReader</param>
        /// <param name="type">Type</param>
        /// <returns>Result</returns>
        public static object ReadBytesValue(this FastBufferReader messagePayload, Type type)
        {
            messagePayload.ReadBytes(out var bytes);
            return NetcodeSerializer.Deserialize(type, bytes);
        }

        /// <summary>
        ///     Read Bytes And deserialize it into specific values
        /// </summary>
        /// <param name="messagePayload">FastBufferReader</param>
        /// <param name="type">Type</param>
        /// <param name="result">Result</param>
        /// <returns>Whether the value was successfully read</returns>
        public static bool ReadBytesValue(this FastBufferReader messagePayload, Type type, out object result)
        {
            try
            {
                messagePayload.ReadBytes(out var bytes);
                result = NetcodeSerializer.Deserialize(type, bytes);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}