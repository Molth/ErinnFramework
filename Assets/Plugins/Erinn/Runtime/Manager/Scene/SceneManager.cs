//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Cysharp.Threading.Tasks;

namespace Erinn
{
    using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

    internal sealed class SceneManager : ModuleSingleton, ISceneManager
    {
        public override int Priority => 1;

        /// <summary>
        ///     Current scene
        /// </summary>
        public string Current => UnitySceneManager.GetActiveScene().name;

        void ISceneManager.LoadSceneAsync(int id) => LoadSceneAsync(id);

        void ISceneManager.LoadSceneAsync(string name) => LoadSceneAsync(name);

        void ISceneManager.LoadSceneAsync(int id, Action onComplete) => LoadSceneAsync(id, onComplete);

        void ISceneManager.LoadSceneAsync(string name, Action onComplete) => LoadSceneAsync(name, onComplete);

        void ISceneManager.LoadSceneAsync(int id, Action onComplete, Action<float> onLoading) => LoadSceneAsync(id, onComplete, onLoading);

        void ISceneManager.LoadSceneAsync(string name, Action onComplete, Action<float> onLoading) => LoadSceneAsync(name, onComplete, onLoading);

        void ISceneManager.UnloadSceneAsync(int id) => UnloadSceneAsync(id);

        void ISceneManager.UnloadSceneAsync(string name) => UnloadSceneAsync(name);

        void ISceneManager.UnloadSceneAsync(int id, Action onComplete) => UnloadSceneAsync(id, onComplete);

        void ISceneManager.UnloadSceneAsync(string name, Action onComplete) => UnloadSceneAsync(name, onComplete);

        void ISceneManager.UnloadSceneAsync(int id, Action onComplete, Action<float> onLoading) => UnloadSceneAsync(id, onComplete, onLoading);

        void ISceneManager.UnloadSceneAsync(string name, Action onComplete, Action<float> onLoading) => UnloadSceneAsync(name, onComplete, onLoading);

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        public static async void LoadSceneAsync(int id)
        {
            var loadSceneTask = UnitySceneManager.LoadSceneAsync(id);
            await loadSceneTask;
        }

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        public static async void LoadSceneAsync(string name)
        {
            var loadSceneTask = UnitySceneManager.LoadSceneAsync(name);
            await loadSceneTask;
        }

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        public static async void LoadSceneAsync(int id, Action onComplete)
        {
            var loadSceneTask = UnitySceneManager.LoadSceneAsync(id);
            await loadSceneTask;
            onComplete.Invoke();
        }

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        public static async void LoadSceneAsync(string name, Action onComplete)
        {
            var loadSceneTask = UnitySceneManager.LoadSceneAsync(name);
            await loadSceneTask;
            onComplete.Invoke();
        }

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        public static async void LoadSceneAsync(int id, Action onComplete, Action<float> onLoading)
        {
            var loadSceneTask = UnitySceneManager.LoadSceneAsync(id);
            while (!loadSceneTask.isDone)
            {
                onLoading.Invoke(loadSceneTask.progress);
                await UniTask.Yield();
            }

            onComplete.Invoke();
        }

        /// <summary>
        ///     Asynchronous loading scenario
        /// </summary>
        public static async void LoadSceneAsync(string name, Action onComplete, Action<float> onLoading)
        {
            var loadSceneTask = UnitySceneManager.LoadSceneAsync(name);
            while (!loadSceneTask.isDone)
            {
                onLoading.Invoke(loadSceneTask.progress);
                await UniTask.Yield();
            }

            onComplete.Invoke();
        }

        public static async void UnloadSceneAsync(int id)
        {
            var unloadSceneTask = UnitySceneManager.UnloadSceneAsync(id);
            await unloadSceneTask;
        }

        public static async void UnloadSceneAsync(string name)
        {
            var unloadSceneTask = UnitySceneManager.UnloadSceneAsync(name);
            await unloadSceneTask;
        }

        public static async void UnloadSceneAsync(int id, Action onComplete)
        {
            var unloadSceneTask = UnitySceneManager.UnloadSceneAsync(id);
            await unloadSceneTask;
            onComplete.Invoke();
        }

        public static async void UnloadSceneAsync(string name, Action onComplete)
        {
            var unloadSceneTask = UnitySceneManager.UnloadSceneAsync(name);
            await unloadSceneTask;
            onComplete.Invoke();
        }

        public static async void UnloadSceneAsync(int id, Action onComplete, Action<float> onLoading)
        {
            var unloadSceneTask = UnitySceneManager.UnloadSceneAsync(id);
            while (!unloadSceneTask.isDone)
            {
                onLoading.Invoke(unloadSceneTask.progress);
                await UniTask.Yield();
            }

            onComplete.Invoke();
        }

        public static async void UnloadSceneAsync(string name, Action onComplete, Action<float> onLoading)
        {
            var unloadSceneTask = UnitySceneManager.UnloadSceneAsync(name);
            while (!unloadSceneTask.isDone)
            {
                onLoading.Invoke(unloadSceneTask.progress);
                await UniTask.Yield();
            }

            onComplete.Invoke();
        }
    }
}