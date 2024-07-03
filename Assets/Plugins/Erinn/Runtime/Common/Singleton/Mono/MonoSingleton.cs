//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     MonoBehaviorSingle singleton, Create when not found
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /// <summary>
        ///     Linear lock
        /// </summary>
        private static readonly Type Locker = typeof(T);

        /// <summary>
        ///     Singleton
        /// </summary>
        private static T _instance;

        /// <summary>
        ///     Singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance != null)
                        return _instance;
                    var obj = new GameObject(typeof(T).Name, typeof(T));
                    _instance = obj.GetComponent<T>();
                }

                return _instance;
            }
        }

        /// <summary>
        ///     Singleton
        /// </summary>
        public static T Singleton => Instance;
    }
}