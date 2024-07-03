//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public static partial class NetcodeClient
    {
        /// <summary>
        ///     NetworkObjectManager
        /// </summary>
        private static NetworkSpawnManager SpawnManager => NetManager.SpawnManager;

        /// <summary>
        ///     Obtain local player objects
        /// </summary>
        /// <returns>Local player objects obtained</returns>
        public static NetworkObject GetLocalPlayerObject() => !IsClient ? null : SpawnManager.GetLocalPlayerObject();
    }
}