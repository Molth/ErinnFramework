//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    internal sealed partial class PoolManager
    {
        /// <summary>
        ///     Store dictionaries for all pools
        /// </summary>
        public static readonly Dictionary<Type, IPool> PoolDict = new();

        public override int Priority => 2;

        T IPoolManager.Pop<T>() => Pop<T>();

        bool IPoolManager.Pop<T>(out T result) => Pop(out result);

        bool IPoolManager.Push<T>(T obj) => Push(obj);

        bool IPoolManager.Count<T>(out int count) => Count<T>(out count);

        void IPoolManager.Clear<T>() => Clear<T>();

        void IPoolManager.Dispose<T>() => Dispose<T>();

        void IPoolManager.ClearAll() => ClearAll();

        /// <summary>
        ///     Get Elements
        /// </summary>
        public static T Pop<T>() where T : class
        {
            Pop<T>(out var result);
            return result;
        }

        /// <summary>
        ///     Get Elements
        /// </summary>
        public static bool Pop<T>(out T result) where T : class
        {
            var key = typeof(T);
            if (PoolDict.TryGetValue(key, out var value))
            {
                var pool = (HashPool<T>)value;
                return pool.TryPop(out result);
            }

            result = default;
            return false;
        }

        /// <summary>
        ///     Pushing in elements
        /// </summary>
        public static bool Push<T>(T obj) where T : class
        {
            if (obj == null)
                return false;
            var key = typeof(T);
            if (PoolDict.TryGetValue(key, out var value))
            {
                var pool = (HashPool<T>)value;
                return pool.Push(obj);
            }

            value = HashPool<T>.Create(obj);
            PoolDict.Add(key, value);
            return true;
        }

        /// <summary>
        ///     Count
        /// </summary>
        /// <param name="count">Count</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Contains</returns>
        public static bool Count<T>(out int count) where T : class
        {
            var key = typeof(T);
            if (!PoolDict.TryGetValue(key, out var value))
            {
                count = 0;
                return false;
            }

            count = value.Count;
            return true;
        }

        /// <summary>
        ///     Empty Element
        /// </summary>
        public static void Clear<T>() where T : class
        {
            var key = typeof(T);
            if (PoolDict.TryGetValue(key, out var value))
                value.Clear();
        }

        /// <summary>
        ///     Destroy object pool
        /// </summary>
        public static void Dispose<T>() where T : class
        {
            var key = typeof(T);
            if (PoolDict.TryGetValue(key, out var value))
            {
                value.Clear();
                PoolDict.Remove(key);
            }
        }

        /// <summary>
        ///     Clear all elements
        /// </summary>
        public static void ClearAll()
        {
            foreach (var pool in PoolDict.Values)
                pool.Clear();
            PoolDict.Clear();
        }

        public override void OnDispose()
        {
            ClearAll();
            ClearAllGo();
        }
    }
}