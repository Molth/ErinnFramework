//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Global Manager
    /// </summary>
    public sealed partial class GlobalManager
    {
        /// <summary>
        ///     Call on load
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Log.Warning("GlobalManager not allow duplicate instances.");
                DestroyImmediate(gameObject);
            }
        }

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        private void Start()
        {
            //ImplementStartEvent
            OnStart?.Invoke();
            //EmptyStartEvent
            OnStart = null;
        }

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update()
        {
            TimerManager.OnUpdate();
            OnUpdate.Invoke();
        }

        /// <summary>
        ///     FixedUpdateWhen calling
        /// </summary>
        private void FixedUpdate() => OnFixedUpdate.Invoke();

        /// <summary>
        ///     LateUpdateWhen calling
        /// </summary>
        private void LateUpdate() => OnLateUpdate.Invoke();

        /// <summary>
        ///     Call on exit
        /// </summary>
        private void OnApplicationQuit() => Dispose();

        /// <summary>
        ///     Execute when exiting the application
        /// </summary>
        private static void Dispose()
        {
            //If the framework has been cleaned up, return, Prevent multiple cleaning of the framework
            if (!Entry.IsRuntime)
                return;
            //Stop all collaborations
            Instance.StopAllCoroutines();
#if UNITY_EDITOR
            //Clear debugging data
            _data = null;
#endif
            //Execute exit event
            OnQuit?.Invoke();
            //Clear exit event
            OnQuit = null;
            //Clear event polling
            ClearAllListeners();
            //Clear singleton system
            SingletonSystem.Clear();
            //Clear the information system
            MessageSystem.Clear();
            //Cleaning modules
            Entry.ClearAll();
            //Garbage collection 
            GC.Collect();
        }
    }
}