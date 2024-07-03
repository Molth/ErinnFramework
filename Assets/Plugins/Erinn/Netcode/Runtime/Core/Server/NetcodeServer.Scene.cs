//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public static partial class NetcodeServer
    {
        /// <summary>
        ///     Scene Manager
        /// </summary>
        private static NetworkSceneManager NetSceneManager => NetManager.SceneManager;

        /// <summary>
        ///     Client synchronization mode
        /// </summary>
        public static LoadSceneMode ClientSynchronizationMode => NetSceneManager.ClientSynchronizationMode;

        /// <summary>
        ///     Enable scene synchronization
        /// </summary>
        public static bool ActiveSceneSynchronizationEnabled => NetSceneManager.ActiveSceneSynchronizationEnabled;

        /// <summary>
        ///     Broadcast synchronization uninstallation
        /// </summary>
        public static bool PostSynchronizationSceneUnloading => NetSceneManager.PostSynchronizationSceneUnloading;

        /// <summary>
        ///     Server loading scenario
        /// </summary>
        /// <param name="sceneName">Scene Name</param>
        public static void LoadScene(string sceneName)
        {
            if (IsServer)
                NetSceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        /// <summary>
        ///     Server loading scenario
        /// </summary>
        /// <param name="sceneName">Scene Name</param>
        /// <param name="loadSceneMode">Load mode</param>
        public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            if (IsServer)
                NetSceneManager.LoadScene(sceneName, loadSceneMode);
        }

        /// <summary>
        ///     Server loading scenario
        /// </summary>
        /// <param name="scene">Scene</param>
        public static void UnloadScene(Scene scene)
        {
            if (IsServer)
                NetSceneManager.UnloadScene(scene);
        }

        /// <summary>
        ///     Set client synchronization mode
        /// </summary>
        /// <param name="loadSceneMode">Client synchronization mode</param>
        public static void SetClientSynchronizationMode(LoadSceneMode loadSceneMode)
        {
            if (IsServer)
                NetSceneManager.SetClientSynchronizationMode(loadSceneMode);
        }

        /// <summary>
        ///     Set scene synchronization
        /// </summary>
        /// <param name="enabled">Whether to enable scene synchronization</param>
        public static void SetActiveSceneSynchronizationEnabled(bool enabled)
        {
            if (IsServer)
                NetSceneManager.ActiveSceneSynchronizationEnabled = enabled;
        }

        /// <summary>
        ///     Set up broadcast synchronization uninstallation
        /// </summary>
        /// <param name="enabled">Do you want to broadcast synchronous uninstallation</param>
        public static void SetPostSynchronizationSceneUnloading(bool enabled)
        {
            if (IsServer)
                NetSceneManager.PostSynchronizationSceneUnloading = enabled;
        }
    }
}