//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     MonoBehaviorSingle singleton, Do not create when not found
    /// </summary>
    public abstract class MonoSingletonUncreated<T> : MonoBehaviour where T : MonoSingletonUncreated<T>
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