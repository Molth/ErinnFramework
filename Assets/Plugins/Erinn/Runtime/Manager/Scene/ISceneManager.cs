//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Scene Manager
    /// </summary>
    public interface ISceneManager
    {
        /// <summary>
        ///     Current scene
        /// </summary>
        string Current { get; }

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        /// <param name="id">Index</param>
        void LoadSceneAsync(int id);

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        /// <param name="name">Name</param>
        void LoadSceneAsync(string name);

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="onComplete">Call upon completion</param>
        void LoadSceneAsync(int id, Action onComplete);

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="onComplete">Call upon completion</param>
        void LoadSceneAsync(string name, Action onComplete);

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="onComplete">Call upon completion</param>
        /// <param name="onLoading">Call during progress</param>
        void LoadSceneAsync(int id, Action onComplete, Action<float> onLoading);

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="onComplete">Call upon completion</param>
        /// <param name="onLoading">Call during progress</param>
        void LoadSceneAsync(string name, Action onComplete, Action<float> onLoading);

        /// <summary>
        ///     Asynchronous uninstallation scenario
        /// </summary>
        /// <param name="id">Index</param>
        void UnloadSceneAsync(int id);

        /// <summary>
        ///     Asynchronous uninstallation scenario
        /// </summary>
        /// <param name="name">Name</param>
        void UnloadSceneAsync(string name);

        /// <summary>
        ///     Asynchronous uninstallation scenario
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="onComplete">Call upon completion</param>
        void UnloadSceneAsync(int id, Action onComplete);

        /// <summary>
        ///     Asynchronous uninstallation scenario
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="onComplete">Call upon completion</param>
        void UnloadSceneAsync(string name, Action onComplete);

        /// <summary>
        ///     Asynchronous uninstallation scenario
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="onComplete">Call upon completion</param>
        /// <param name="onLoading">Call during progress</param>
        void UnloadSceneAsync(int id, Action onComplete, Action<float> onLoading);

        /// <summary>
        ///     Asynchronous uninstallation scenario
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="onComplete">Call upon completion</param>
        /// <param name="onLoading">Call during progress</param>
        void UnloadSceneAsync(string name, Action onComplete, Action<float> onLoading);
    }
}