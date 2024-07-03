//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public static partial class NetcodeServer
    {
        /// <summary>
        ///     NetworkObject Manager
        /// </summary>
        private static NetworkSpawnManager SpawnManager => NetManager.SpawnManager;

        /// <summary>
        ///     Get generated NetworkObject
        /// </summary>
        /// <param name="clientId">Client Index</param>
        /// <returns>Obtained NetworkObject</returns>
        public static NetworkObject GetSpawnedObject(ulong clientId) => !IsServer ? null : SpawnManager.SpawnedObjects.TryGetValue(clientId, out var value) ? value : null;

        /// <summary>
        ///     Check if the object is generated on the server
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <returns>Is the object generated on the server</returns>
        public static bool CheckSpawned(GameObject gameObject) => IsServer && gameObject.TryGetComponent<NetworkObject>(out var networkObject) && SpawnManager.SpawnedObjectsList.Contains(networkObject);

        /// <summary>
        ///     Obtain player object
        /// </summary>
        /// <param name="clientId">Client Index</param>
        /// <returns>Obtained player object</returns>
        public static NetworkObject GetPlayerNetworkObject(ulong clientId) => !IsServer ? null : SpawnManager.GetPlayerNetworkObject(clientId);

        /// <summary>
        ///     Obtain objects owned by the client
        /// </summary>
        /// <param name="clientId">Client Index</param>
        /// <returns>Objects owned by the client</returns>
        public static List<NetworkObject> GetClientOwnedObjects(ulong clientId) => !IsServer ? null : SpawnManager.GetClientOwnedObjects(clientId);

        /// <summary>
        ///     Server generates object
        /// </summary>
        /// <param name="gameObject">Object</param>
        public static void Spawn(GameObject gameObject)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.Spawn(true);
        }

        /// <summary>
        ///     Server generates object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="destroyWithScene">Whether to delete during scene switching</param>
        public static void Spawn(GameObject gameObject, bool destroyWithScene)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.Spawn(destroyWithScene);
        }

        /// <summary>
        ///     Server generates object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">Client Index</param>
        public static void SpawnWithOwnership(GameObject gameObject, ulong clientId)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.SpawnWithOwnership(clientId, true);
        }

        /// <summary>
        ///     Server generates object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">Client Index</param>
        /// <param name="destroyWithScene">Whether to delete during scene switching</param>
        public static void SpawnWithOwnership(GameObject gameObject, ulong clientId, bool destroyWithScene)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.SpawnWithOwnership(clientId, destroyWithScene);
        }

        /// <summary>
        ///     Server generates object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">Client Index</param>
        public static void SpawnAsPlayerObject(GameObject gameObject, ulong clientId)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.SpawnAsPlayerObject(clientId, true);
        }

        /// <summary>
        ///     Server generates object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">Client Index</param>
        /// <param name="destroyWithScene">Whether to delete during scene switching</param>
        public static void SpawnAsPlayerObject(GameObject gameObject, ulong clientId, bool destroyWithScene)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.SpawnAsPlayerObject(clientId, destroyWithScene);
        }

        /// <summary>
        ///     Server Switching Ownership
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">Client Index</param>
        public static void ChangeOwnership(GameObject gameObject, ulong clientId)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.ChangeOwnership(clientId);
        }

        /// <summary>
        ///     Server Remove Ownership
        /// </summary>
        /// <param name="gameObject">Object</param>
        public static void RemoveOwnership(GameObject gameObject)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.RemoveOwnership();
        }

        /// <summary>
        ///     Server deleting object
        /// </summary>
        /// <param name="gameObject">Object</param>
        public static void Destroy(GameObject gameObject)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.Despawn();
        }

        /// <summary>
        ///     Server deleting object
        /// </summary>
        /// <param name="gameObject">Object</param>
        public static void UnSpawn(GameObject gameObject)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.Despawn(false);
        }

        /// <summary>
        ///     Server displays object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">ClientId</param>
        public static void NetworkShow(GameObject gameObject, ulong clientId)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.NetworkShow(clientId);
        }

        /// <summary>
        ///     Server hides object
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">ClientId</param>
        public static void NetworkHide(GameObject gameObject, ulong clientId)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.NetworkHide(clientId);
        }

        /// <summary>
        ///     Server checks object visibility
        /// </summary>
        /// <param name="gameObject">Object</param>
        /// <param name="clientId">ClientId</param>
        public static void CheckObjectVisibility(GameObject gameObject, ulong clientId)
        {
            if (!IsServer)
                return;
            if (gameObject.TryGetComponent<NetworkObject>(out var networkObject))
                networkObject.CheckObjectVisibility(clientId);
        }
    }
}