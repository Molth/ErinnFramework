//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Singleton system
    /// </summary>
    public static class SingletonSystem
    {
        /// <summary>
        ///     Singleton dictionary
        /// </summary>
        private static readonly Dictionary<Type, ISingleton> SingletonDict = new();

        /// <summary>
        ///     Registration singleton
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void Register<T>() where T : Singleton<T>, new()
        {
            if (!Entry.IsRuntime)
            {
                Log.Error("Single instances can only be registered in runtime mode");
                return;
            }

            var key = typeof(T);
            if (SingletonDict.ContainsKey(key))
                return;
            ISingleton singleton = new T();
            SingletonDict.Add(key, singleton);
            singleton.RegisterInstance();
        }

        /// <summary>
        ///     Delete singleton
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void Dispose<T>() where T : Singleton<T>, new()
        {
            var key = typeof(T);
            if (SingletonDict.TryGetValue(key, out var value))
            {
                value.DisposeInstance();
                SingletonDict.Remove(key);
            }
        }

        /// <summary>
        ///     Check if there are any singletons included
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Does it contain a singleton</returns>
        public static bool Check<T>() where T : Singleton<T>, new() => SingletonDict.ContainsKey(typeof(T));

        /// <summary>
        ///     Obtain singleton
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained singleton</returns>
        public static T Get<T>() where T : Singleton<T>, new() => SingletonDict.TryGetValue(typeof(T), out var value) ? (T)value : null;

        /// <summary>
        ///     Clear singletons
        /// </summary>
        public static void Clear()
        {
            foreach (var value in SingletonDict.Values)
                value.DisposeInstance();
            SingletonDict.Clear();
        }
    }
}