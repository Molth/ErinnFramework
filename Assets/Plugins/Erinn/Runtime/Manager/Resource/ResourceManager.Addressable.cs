//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Erinn
{
    internal sealed partial class ResourceManager
    {
        /// <summary>
        ///     Resource Storage Dictionary
        /// </summary>
        public static readonly Dictionary<string, AsyncOperationHandle> AssetDict = new();

        /// <summary>
        ///     Dictionary of available resource paths
        /// </summary>
        public static Dictionary<string, HashSet<string>> PathDict { get; private set; }

        T IResourceManager.Load<T>(string path) => Load<T>(path);

        void IResourceManager.LoadAsync<T>(string path, Action<T> action) => LoadAsync(path, action);

        UniTask<T> IResourceManager.LoadAsync<T>(string path) => LoadAsync<T>(path);

        void IResourceManager.Dispose(string path) => Dispose(path);

        void IResourceManager.Dispose<T>(string path) => Dispose<T>(path);

        /// <summary>
        ///     Check if the path is valid
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Is it valid</returns>
        public static bool CheckPathValid(string path)
        {
            if (AssetDict.ContainsKey(path))
                return true;
            if (PathDict == null)
                return false;
            var group = path.Split('/')[0];
            return PathDict.TryGetValue(group, out var value) && value.Contains(path);
        }

        /// <summary>
        ///     Loading resource through Resource Manager (Synchronization)
        /// </summary>
        /// <param name="path">Path of resource</param>
        /// <typeparam name="T">Can use any inheritanceObjectObject of</typeparam>
        public static T Load<T>(string path) where T : Object
        {
            if (!CheckPathValid(path))
            {
                Log.Error($"{path} is not found! ");
                return null;
            }

            T result;
            AsyncOperationHandle<T> handle;
            if (AssetDict.TryGetValue(path, out var value))
            {
                handle = value.Convert<T>();
                result = handle.WaitForCompletion();
            }
            else
            {
                handle = Addressables.LoadAssetAsync<T>(path);
                AssetDict.Add(path, handle);
                result = handle.WaitForCompletion();
            }

            if (result == null)
            {
                AssetDict.Remove(path);
                return null;
            }

            return LoadCompleted(result);
        }

        /// <summary>
        ///     Asynchronous loading of resource through the Resource Loading Manager
        /// </summary>
        /// <param name="path">Path of resource</param>
        /// <param name="action">Callback function for resource</param>
        /// <typeparam name="T">Can use any inheritanceObjectObject of</typeparam>
        public static void LoadAsync<T>(string path, Action<T> action) where T : Object
        {
            if (!CheckPathValid(path))
            {
                Log.Error($"{path} is not found! ");
                return;
            }

            AsyncOperationHandle<T> handle;
            if (AssetDict.TryGetValue(path, out var value))
            {
                handle = value.Convert<T>();
                if (handle.IsDone)
                    OnComplete(handle);
                else
                    handle.Completed += OnComplete;
                return;
            }

            handle = Addressables.LoadAssetAsync<T>(path);
            handle.Completed += OnComplete;
            AssetDict.Add(path, handle);
            void OnComplete(AsyncOperationHandle<T> obj) => action.Invoke(LoadCompleted(obj.Result));
        }

        /// <summary>
        ///     Asynchronous loading of resource through the Resource Loading Manager
        /// </summary>
        /// <param name="path">Path of resource</param>
        /// <typeparam name="T">Can use any inheritanceObjectObject of</typeparam>
        /// <returns>Task to return resource</returns>
        public static async UniTask<T> LoadAsync<T>(string path) where T : Object
        {
            if (!CheckPathValid(path))
            {
                Log.Error($"{path} is not found! ");
                return null;
            }

            AsyncOperationHandle<T> handle;
            if (AssetDict.TryGetValue(path, out var value))
            {
                handle = value.Convert<T>();
                if (!handle.IsDone)
                    await handle.Task;
                return LoadCompleted(handle.Result);
            }

            handle = Addressables.LoadAssetAsync<T>(path);
            AssetDict.Add(path, handle);
            await handle.Task;
            return LoadCompleted(handle.Result);
        }

        /// <summary>
        ///     Release resource through the Resource Loading Manager
        /// </summary>
        /// <param name="path">Path of resource</param>
        public static void Dispose(string path)
        {
            if (!AssetDict.TryGetValue(path, out var value))
                return;
            Addressables.Release(value);
            AssetDict.Remove(path);
        }

        /// <summary>
        ///     Release resource through the Resource Loading Manager
        /// </summary>
        /// <param name="path">Path of resource</param>
        /// <typeparam name="T">Can use any inheritanceObjectObject of</typeparam>
        public static void Dispose<T>(string path)
        {
            if (!AssetDict.TryGetValue(path, out var value))
                return;
            var handle = value.Convert<T>();
            Addressables.Release(handle);
            AssetDict.Remove(path);
        }

        /// <summary>
        ///     ApplyJsonManager obtains resource configuration dictionary
        /// </summary>
        /// <returns>Returned resource configuration dictionary</returns>
        [SuppressMessage("ReSharper", "Unity.UnknownResource")]
        public static Dictionary<string, HashSet<string>> GetResourceDict()
        {
            var file = Resources.Load<TextAsset>("AddressablePathData");
            if (file == null)
                return null;
            var saveJson = file.text;
            if (string.IsNullOrEmpty(saveJson))
                return null;
            return JsonManager.DeserializeObject<Dictionary<string, HashSet<string>>>(saveJson);
        }

        public override void OnInit() => PathDict = GetResourceDict();

        public override void OnDispose()
        {
            PathDict = null;
            foreach (var handle in AssetDict.Values)
                Addressables.Release(handle);
            AssetDict.Clear();
        }
    }
}