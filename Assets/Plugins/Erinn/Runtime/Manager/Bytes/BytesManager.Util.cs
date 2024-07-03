//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Erinn
{
    internal sealed partial class BytesManager
    {
        /// <summary>
        ///     Cancel Dictionary
        /// </summary>
        private static readonly Dictionary<string, CancellationTokenSource> CancelSourceDict = new();

        byte[] IByteManager.Serialize<T>(T obj) => Serialize(obj);

        byte[] IByteManager.SerializeObject(object obj) => SerializeObject(obj);

        byte[] IByteManager.SerializeObject(Type type, object obj) => SerializeObject(type, obj);

        T IByteManager.Deserialize<T>(byte[] text) => Deserialize<T>(text);

        object IByteManager.DeserializeObject(Type type, byte[] text) => DeserializeObject(type, text);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Text</returns>
        public static byte[] Serialize<T>(T obj) => MemoryPackSerializer.Serialize(typeof(T), obj);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Text</returns>
        public static byte[] SerializeObject(object obj) => MemoryPackSerializer.Serialize(obj.GetType(), obj);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="obj">object</param>
        /// <returns>Text</returns>
        public static byte[] SerializeObject(Type type, object obj) => MemoryPackSerializer.Serialize(type, obj);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="text">Text</param>
        /// <returns>Element</returns>
        public static T Deserialize<T>(byte[] text) => MemoryPackSerializer.Deserialize<T>(text);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="text">Text</param>
        /// <returns>Element</returns>
        public static object DeserializeObject(Type type, byte[] text) => MemoryPackSerializer.Deserialize(type, text);

        /// <summary>
        ///     Format Path
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Formatted path</returns>
        private static string FormatName(string name) => name.Replace("/", "").Replace("\\", "");

        /// <summary>
        ///     Asynchronous writing of text
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="content">Text</param>
        /// <returns>Task</returns>
        private static async UniTask WriteTextAsync(string path, byte[] content)
        {
            try
            {
                if (CancelSourceDict.TryGetValue(path, out var source))
                {
                    source?.Cancel();
                    CancelSourceDict.Remove(path);
                }

                using var cancelSource = new CancellationTokenSource();
                CancelSourceDict[path] = source;
                await File.WriteAllBytesAsync(path, content, cancelSource.Token);
            }
            catch
            {
                await UniTask.Yield();
            }
            finally
            {
                CancelSourceDict.Remove(path);
            }
        }
    }
}