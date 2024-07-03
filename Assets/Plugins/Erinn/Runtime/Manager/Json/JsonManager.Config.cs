//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.IO;
using UnityEngine;

namespace Erinn
{
    internal partial class JsonManager
    {
        private static string _configFolder;

        public static string ConfigFolder
        {
            get
            {
                if (_configFolder != null)
                    return _configFolder;
                _configFolder = Path.Combine(Application.persistentDataPath, "ConfigData");
                return _configFolder;
            }
        }

        void IJsonManager.SaveConfig(string name, object obj) => SaveConfig(name, obj);

        void IJsonManager.SaveConfig(string name, object obj, Action onComplete) => SaveConfig(name, obj, onComplete);

        bool IJsonManager.LoadConfig<T>(string name, out T result) => LoadConfig(name, out result);

        void IJsonManager.DeleteConfig(string name) => DeleteConfig(name);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        public static async void SaveConfig(string name, object obj)
        {
            var filePath = GetConfigPath(name);
            var saveJson = SerializeObject(obj);
            await WriteTextAsync(filePath, saveJson);
        }

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        public static async void SaveConfig(string name, object obj, Action onComplete)
        {
            var filePath = GetConfigPath(name);
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
        /// <returns>Return decrypted data</returns>
        public static bool LoadConfig<T>(string name, out T result)
        {
            var filePath = GetConfigPath(name);
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
        ///     Delete configuration data
        /// </summary>
        /// <param name="name">Name</param>
        public static void DeleteConfig(string name)
        {
            var filePath = GetConfigPath(name);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        ///     JsonManager obtains configuration path
        /// </summary>
        /// <param name="name">The path name passed in</param>
        /// <returns>The path returned to</returns>
        private static string GetConfigPath(string name)
        {
            if (!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);
            return Path.Combine(ConfigFolder, $"{FormatName(name)}.eire");
        }
    }
}