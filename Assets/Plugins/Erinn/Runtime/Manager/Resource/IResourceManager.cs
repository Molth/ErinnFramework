//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Erinn
{
    /// <summary>
    ///     Resource Manager
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        ///     Load resource
        /// </summary>
        /// <param name="path">Path</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Resources obtained</returns>
        T Load<T>(string path) where T : Object;

        /// <summary>
        ///     Asynchronous loading of resource
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="action">Call upon completion of loading</param>
        /// <typeparam name="T">Type</typeparam>
        void LoadAsync<T>(string path, Action<T> action) where T : Object;

        /// <summary>
        ///     Asynchronous loading of resource
        /// </summary>
        /// <param name="path">Path</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Resources obtained</returns>
        UniTask<T> LoadAsync<T>(string path) where T : Object;

        /// <summary>
        ///     Release resource
        /// </summary>
        /// <param name="path">Path</param>
        void Dispose(string path);

        /// <summary>
        ///     Release resource
        /// </summary>
        /// <param name="path">Path</param>
        /// <typeparam name="T">Type</typeparam>
        void Dispose<T>(string path);
    }
}