//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.IO;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class BytesManager
    {
        private static string _folder;

        public static string Folder
        {
            get
            {
                if (_folder != null)
                    return _folder;
                _folder = Path.Combine(Application.persistentDataPath, "PlayerData");
                return _folder;
            }
        }

        void IByteManager.Save<T>(string name, T obj) => Save(name, obj);

        void IByteManager.Save<T>(string name, T obj, Action onComplete) => Save(name, obj, onComplete);

        void IByteManager.SaveObject(string name, object obj) => SaveObject(name, obj);

        void IByteManager.SaveObject(string name, object obj, Action onComplete) => SaveObject(name, obj, onComplete);

        bool IByteManager.Load<T>(string name, out T result) => Load(name, out result);

        bool IByteManager.LoadObject(string name, Type type, out object result) => LoadObject(name, type, out result);

        void IByteManager.Delete(string name) => Delete(name);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        public static async void Save<T>(string name, T obj)
        {
            var filePath = GetPath(name);
            var saveBytes = Serialize(obj);
            await WriteTextAsync(filePath, saveBytes);
        }

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        public static async void Save<T>(string name, T obj, Action onComplete)
        {
            var filePath = GetPath(name);
            var saveBytes = Serialize(obj);
            await WriteTextAsync(filePath, saveBytes);
            onComplete.Invoke();
        }

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        public static async void SaveObject(string name, object obj)
        {
            var filePath = GetPath(name);
            var saveBytes = SerializeObject(obj);
            await WriteTextAsync(filePath, saveBytes);
        }

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        public static async void SaveObject(string name, object obj, Action onComplete)
        {
            var filePath = GetPath(name);
            var saveBytes = SerializeObject(obj);
            await WriteTextAsync(filePath, saveBytes);
            onComplete.Invoke();
        }

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">The name of the loaded data</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Can use any type</typeparam>
        /// <returns>Return loaded data</returns>
        public static bool Load<T>(string name, out T result)
        {
            var filePath = GetPath(name);
            if (!File.Exists(filePath))
            {
                result = default;
                return false;
            }

            var saveBytes = File.ReadAllBytes(filePath);
            if (saveBytes.Length == 0)
            {
                result = default;
                return false;
            }

            result = Deserialize<T>(saveBytes);
            return true;
        }

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">The name of the loaded data</param>
        /// <param name="type">Type</param>
        /// <param name="result">Result</param>
        /// <returns>Return loaded data</returns>
        public static bool LoadObject(string name, Type type, out object result)
        {
            var filePath = GetPath(name);
            if (!File.Exists(filePath))
            {
                result = default;
                return false;
            }

            var saveBytes = File.ReadAllBytes(filePath);
            if (saveBytes.Length == 0)
            {
                result = default;
                return false;
            }

            result = DeserializeObject(type, saveBytes);
            return true;
        }

        /// <summary>
        ///     Delete data
        /// </summary>
        /// <param name="name">Name</param>
        public static void Delete(string name)
        {
            var filePath = GetPath(name);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        ///     ByteManager obtains path
        /// </summary>
        /// <param name="name">The path name passed in</param>
        /// <returns>Obtained path</returns>
        private static string GetPath(string name)
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
            return Path.Combine(Folder, $"{FormatName(name)}.eire");
        }
    }
}