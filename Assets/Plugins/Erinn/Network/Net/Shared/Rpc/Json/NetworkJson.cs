//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using UnityEngine;
#endif
#if GODOT
using Godot;
#endif
#if UNITY_2021_3_OR_NEWER || GODOT
using System.IO;
#endif
using System.Runtime.CompilerServices;
using System.Text;
#if !UNITY_2021_3_OR_NEWER || GODOT
using System.Text.Json;
#endif

#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8603
#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Network Json
    /// </summary>
    public static class NetworkJson
    {
        /// <summary>
        ///     Get accessible folder
        /// </summary>
        /// <returns>Accessible folder</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Combine(string path1) => Path.Combine(GetFolder(), path1);

        /// <summary>
        ///     Get accessible folder
        /// </summary>
        /// <returns>Accessible folder</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Combine(string path1, string path2) => Path.Combine(GetFolder(), path1, path2);

        /// <summary>
        ///     Get accessible folder
        /// </summary>
        /// <returns>Accessible folder</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Combine(string path1, string path2, string path3) => Path.Combine(GetFolder(), path1, path2, path3);

        /// <summary>
        ///     Get accessible folder
        /// </summary>
        /// <returns>Accessible folder</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetFolder() =>
#if UNITY_2021_3_OR_NEWER
            Application.platform switch
            {
                RuntimePlatform.OSXPlayer or RuntimePlatform.WindowsPlayer or RuntimePlatform.LinuxPlayer or RuntimePlatform.LinuxServer or RuntimePlatform.WindowsServer or RuntimePlatform.OSXServer => Path.GetDirectoryName(Application.dataPath),
                _ => Application.persistentDataPath
            };
#elif GODOT
            OS.GetName() switch
            {
                "Windows" or "macOS" or "Linux" => Path.GetDirectoryName(OS.GetExecutablePath()),
                _ => OS.GetUserDataDir()
            };
#else
            Environment.CurrentDirectory;
#endif

        /// <summary>
        ///     Serialize to json
        /// </summary>
        /// <returns>Json</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Serialize<T>(T obj) =>
#if !UNITY_2021_3_OR_NEWER || GODOT
            JsonSerializer.Serialize(obj, typeof(T));
#else
            JsonUtility.ToJson(obj);
#endif

        /// <summary>
        ///     Deserialize to value
        /// </summary>
        /// <param name="json">Json</param>
        /// <returns>Value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Deserialize<T>(string json) =>
#if !UNITY_2021_3_OR_NEWER|| GODOT
            JsonSerializer.Deserialize<T>(json);
#else
            JsonUtility.FromJson<T>(json);
#endif

        /// <summary>
        ///     Create directory if needed
        /// </summary>
        /// <param name="path">File path</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CreateDirectory(string path)
        {
            var directoryPath = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directoryPath))
                return false;
            if (Directory.Exists(directoryPath))
                return true;
            Directory.CreateDirectory(directoryPath);
            return true;
        }

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="value">Default</param>
        /// <returns>T</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValueOrDefault<T>(string path, T value)
        {
            if (File.Exists(path))
                return Deserialize<T>(File.ReadAllText(path));
            if (!CreateDirectory(path))
                return value;
            File.WriteAllText(path, Serialize(value));
            return value;
        }

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="value">Value</param>
        /// <returns>T</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteValue<T>(string path, T value)
        {
            if (!CreateDirectory(path))
                return value;
            File.WriteAllText(path, Serialize(value));
            return value;
        }

        /// <summary>
        ///     Read value
        /// </summary>
        /// <param name="path">File path</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadValue<T>(string path) => Deserialize<T>(File.ReadAllText(path));

        /// <summary>
        ///     Write string
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="contents">Value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write(string path, string contents) => File.WriteAllText(path, contents, Encoding.UTF8);

        /// <summary>
        ///     Read string
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string? Read(string path) => !File.Exists(path) ? null : File.ReadAllText(path, Encoding.UTF8);
    }
}