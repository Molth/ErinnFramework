//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class PoolManager : ModuleSingleton, IPoolManager
    {
        /// <summary>
        ///     Store a dictionary for all object pools
        /// </summary>
        public static readonly Dictionary<string, HashPool<GameObject>> GameObjectPoolDict = new();

        GameObject IPoolManager.PopGo(string path) => PopGo(path);

        GameObject IPoolManager.PopGo(string path, bool loadIfNull) => PopGo(path, loadIfNull);

        void IPoolManager.PopGoAsync(string path, Action<GameObject> onPop) => PopGoAsync(path, onPop);

        bool IPoolManager.PushGo(GameObject obj) => PushGo(obj);

        bool IPoolManager.PushGo(string key, GameObject obj) => PushGo(key, obj);

        bool IPoolManager.CountGo(string path, out int count) => CountGo(path, out count);

        void IPoolManager.ClearGo(string path) => ClearGo(path);

        void IPoolManager.DisposeGo(string path) => DisposeGo(path);

        void IPoolManager.ClearAllGo() => ClearAllGo();

        /// <summary>
        ///     Get Elements
        /// </summary>
        public static GameObject PopGo(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            if (GameObjectPoolDict.TryGetValue(path, out var pool))
            {
                if (pool.Count > 0)
                {
                    var obj = pool.Pop();
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        ///     Get Elements
        /// </summary>
        public static GameObject PopGo(string path, bool loadIfNull)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            if (GameObjectPoolDict.TryGetValue(path, out var pool))
            {
                if (pool.Count > 0)
                {
                    var obj = pool.Pop();
                    return obj;
                }
            }

            return loadIfNull ? Load(path) : null;
        }

        /// <summary>
        ///     Synchronous loading, If the object pool cannot be obtained, call
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Obtained gameObject</returns>
        private static GameObject Load(string path)
        {
            var obj = ResourceManager.Load<GameObject>(path);
            if (obj != null)
            {
                obj.name = path;
                return obj;
            }

            return null;
        }

        /// <summary>
        ///     Asynchronous retrieval of elements
        /// </summary>
        public static void PopGoAsync(string path, Action<GameObject> onPop)
        {
            if (string.IsNullOrEmpty(path))
                return;
            if (GameObjectPoolDict.TryGetValue(path, out var pool))
            {
                if (pool.Count > 0)
                {
                    var obj = pool.Pop();
                    onPop?.Invoke(obj);
                    return;
                }

                LoadAsync(path, onPop);
            }
            else
            {
                LoadAsync(path, onPop);
            }
        }

        /// <summary>
        ///     Asynchronous loading, If the object pool cannot be obtained, call
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="onPop">Callback upon completion of loading</param>
        private static void LoadAsync(string path, Action<GameObject> onPop) => ResourceManager.LoadAsync<GameObject>(path, obj =>
        {
            if (obj == null)
                return;
            obj.name = path;
            onPop?.Invoke(obj);
        });

        /// <summary>
        ///     Pushing in elements
        /// </summary>
        public static bool PushGo(GameObject obj)
        {
            if (obj == null)
                return false;
            var key = obj.name;
            if (string.IsNullOrEmpty(key))
                return false;
            if (GameObjectPoolDict.TryGetValue(key, out var pool))
                return pool.Push(obj);
            pool = HashPool<GameObject>.Create(obj);
            GameObjectPoolDict.Add(key, pool);
            return true;
        }

        /// <summary>
        ///     Pushing in elements
        /// </summary>
        public static bool PushGo(string key, GameObject obj)
        {
            if (obj == null)
                return false;
            if (string.IsNullOrEmpty(key))
                return false;
            if (GameObjectPoolDict.TryGetValue(key, out var pool))
                return pool.Push(obj);
            pool = HashPool<GameObject>.Create(obj);
            GameObjectPoolDict.Add(key, pool);
            return true;
        }

        /// <summary>
        ///     Count
        /// </summary>
        public static bool CountGo(string path, out int count)
        {
            if (!GameObjectPoolDict.TryGetValue(path, out var pool))
            {
                count = 0;
                return false;
            }

            count = pool.Count;
            return true;
        }

        /// <summary>
        ///     Empty Element
        /// </summary>
        public static void ClearGo(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;
            if (GameObjectPoolDict.TryGetValue(path, out var pool))
                pool.Clear();
        }

        /// <summary>
        ///     Destroy object pool
        /// </summary>
        public static void DisposeGo(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;
            if (GameObjectPoolDict.TryGetValue(path, out var pool))
            {
                pool.Clear();
                GameObjectPoolDict.Remove(path);
            }
        }

        /// <summary>
        ///     Clear all elements
        /// </summary>
        public static void ClearAllGo()
        {
            foreach (var pool in GameObjectPoolDict.Values)
                pool.Clear();
            GameObjectPoolDict.Clear();
        }
    }
}