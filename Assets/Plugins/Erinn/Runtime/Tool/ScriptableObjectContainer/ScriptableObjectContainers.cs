//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     ScriptableObjectContainer
    /// </summary>
    public static class ScriptableObjectContainers
    {
        /// <summary>
        ///     Title Name Title
        /// </summary>
        public const string MenuName = "SoContainer/";

        /// <summary>
        ///     ScriptableObjectContainer Dictionary
        /// </summary>
        private static readonly Dictionary<Type, IScriptableObjectContainer> ScriptableObjectContainerDictionary = new();

        /// <summary>
        ///     ObtainScriptableObjectContainer
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>ObtainedScriptableObjectContainer</returns>
        public static T Load<T>() where T : ScriptableObject, IScriptableObjectContainer
        {
            var key = typeof(T);
            if (ScriptableObjectContainerDictionary.TryGetValue(key, out var container))
                return (T)container;
            var path = MenuName + typeof(T).Name;
            container = Entry.Resource.Load<T>(path);
            ScriptableObjectContainerDictionary[key] = container;
            return (T)container;
        }

        /// <summary>
        ///     ObtainScriptableObjectContainer
        /// </summary>
        /// <param name="path">Path</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>ObtainedScriptableObjectContainer</returns>
        public static T Load<T>(string path) where T : ScriptableObject, IScriptableObjectContainer
        {
            var key = typeof(T);
            if (ScriptableObjectContainerDictionary.TryGetValue(key, out var container))
                return (T)container;
            container = Entry.Resource.Load<T>(path);
            ScriptableObjectContainerDictionary[key] = container;
            return (T)container;
        }
    }
}