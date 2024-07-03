//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Object Pool Manager
    /// </summary>
    public interface IPoolManager
    {
        /// <summary>
        ///     Attempting to launch object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        T Pop<T>() where T : class;

        /// <summary>
        ///     Attempting to launch object
        /// </summary>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Was it successfully launched</returns>
        bool Pop<T>(out T result) where T : class;

        /// <summary>
        ///     Push object
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Is the push successful</returns>
        bool Push<T>(T obj) where T : class;

        /// <summary>
        ///     Count
        /// </summary>
        /// <param name="count">Count</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Contains</returns>
        bool Count<T>(out int count) where T : class;

        /// <summary>
        ///     Empty
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void Clear<T>() where T : class;

        /// <summary>
        ///     Empty and destroy object pool
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void Dispose<T>() where T : class;

        /// <summary>
        ///     Empty All
        /// </summary>
        void ClearAll();

        /// <summary>
        ///     ObtainGameObject
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>ObtainedGameObject</returns>
        GameObject PopGo(string path);

        /// <summary>
        ///     ObtainGameObject
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="loadIfNull">Load when object pool cannot be obtained</param>
        /// <returns>ObtainedGameObject</returns>
        GameObject PopGo(string path, bool loadIfNull);

        /// <summary>
        ///     Asynchronous acquisitionGameObject
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="onPop">Call upon launch</param>
        void PopGoAsync(string path, Action<GameObject> onPop);

        /// <summary>
        ///     Push inGameObject
        /// </summary>
        /// <param name="obj">GameObject</param>
        /// <returns>Is the push successful</returns>
        bool PushGo(GameObject obj);

        /// <summary>
        ///     Push inGameObject
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="obj">GameObject</param>
        /// <returns>Is the push successful</returns>
        bool PushGo(string key, GameObject obj);

        /// <summary>
        ///     Count
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="count">Count</param>
        /// <returns>Contains</returns>
        bool CountGo(string key, out int count);

        /// <summary>
        ///     EmptyGameObject
        /// </summary>
        /// <param name="path">Path</param>
        void ClearGo(string path);

        /// <summary>
        ///     DestructionGameObjectPool
        /// </summary>
        /// <param name="path">Path</param>
        void DisposeGo(string path);

        /// <summary>
        ///     Clear AllGameObject
        /// </summary>
        void ClearAllGo();
    }
}