//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.IO;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class JsonManager
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

        void IJsonManager.Save(string name, object obj) => Save(name, obj);

        void IJsonManager.Save(string name, object obj, Action onComplete) => Save(name, obj, onComplete);

        bool IJsonManager.Load<T>(string name, out T result) => Load(name, out result);

        void IJsonManager.Delete(string name) => Delete(name);

        void IJsonManager.SaveString(string name, string obj) => SaveString(name, obj);

        void IJsonManager.SaveString(string name, string obj, Action onComplete) => SaveString(name, obj, onComplete);

        bool IJsonManager.LoadString(string name, out string result) => LoadString(name, out result);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        public static async void Save(string name, object obj)
        {
            var filePath = GetPath(name);
            var saveJson = SerializeObject(obj);
            await WriteTextAsync(filePath, saveJson);
        }

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        public static async void Save(string name, object obj, Action onComplete)
        {
            var filePath = GetPath(name);
            var saveJson = SerializeObject(obj);
            await WriteTextAsync(filePath, saveJson);
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

            var saveJson = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(saveJson))
            {
                result = default;
                return false;
            }

            result = DeserializeObject<T>(saveJson);
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
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        public static async void SaveString(string name, string obj)
        {
            var filePath = GetPath(name);
            await WriteTextAsync(filePath, obj);
        }

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        public static async void SaveString(string name, string obj, Action onComplete)
        {
            var filePath = GetPath(name);
            await WriteTextAsync(filePath, obj);
            onComplete.Invoke();
        }

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">The name of the loaded data</param>
        /// <param name="result">Result</param>
        /// <returns>Return loaded data</returns>
        public static bool LoadString(string name, out string result)
        {
            var filePath = GetPath(name);
            if (!File.Exists(filePath))
            {
                result = default;
                return false;
            }

            result = File.ReadAllText(filePath);
            return true;
        }

        /// <summary>
        ///     JsonManager obtains path
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