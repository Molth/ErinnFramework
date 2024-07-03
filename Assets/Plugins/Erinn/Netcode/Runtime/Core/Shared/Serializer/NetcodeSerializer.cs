//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static partial class NetcodeSerializer
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>bytes</returns>
        public static byte[] Serialize<T>(T obj) => MemoryPackSerializer.Serialize(typeof(T), obj);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="bytes">Bytes</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Successfully serialized</returns>
        public static bool Serialize<T>(T obj, out byte[] bytes)
        {
            try
            {
                bytes = MemoryPackSerializer.Serialize(typeof(T), obj);
                return true;
            }
            catch
            {
                bytes = null;
                return false;
            }
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>bytes</returns>
        public static byte[] SerializeObject(object obj) => MemoryPackSerializer.Serialize(obj.GetType(), obj);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="bytes">Bytes</param>
        /// <returns>Successfully serialized</returns>
        public static bool SerializeObject(object obj, out byte[] bytes)
        {
            try
            {
                bytes = MemoryPackSerializer.Serialize(obj.GetType(), obj);
                return true;
            }
            catch
            {
                bytes = null;
                return false;
            }
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        public static T Deserialize<T>(byte[] bytes) => MemoryPackSerializer.Deserialize<T>(bytes);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="bytes">bytes</param>
        /// <param name="result">Result</param>
        /// <returns>Is Deserialize successful</returns>
        public static bool Deserialize<T>(byte[] bytes, out T result)
        {
            try
            {
                result = MemoryPackSerializer.Deserialize<T>(bytes);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="bytes">bytes</param>
        /// <returns>Result</returns>
        public static object Deserialize(Type type, byte[] bytes) => MemoryPackSerializer.Deserialize(type, bytes);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="bytes">bytes</param>
        /// <param name="result">Result</param>
        /// <returns>Is Deserialize successful</returns>
        public static bool Deserialize(Type type, byte[] bytes, out object result)
        {
            try
            {
                result = MemoryPackSerializer.Deserialize(type, bytes);
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