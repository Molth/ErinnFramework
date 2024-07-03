//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Network object pool prefabricated component singleton handle
    /// </summary>
    public readonly struct NetworkPooledPrefabInstanceHandler : INetworkPrefabInstanceHandler
    {
        /// <summary>
        ///     Key
        /// </summary>
        private readonly string _key;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="key">Key</param>
        public NetworkPooledPrefabInstanceHandler(string key) => _key = key;

        /// <summary>
        ///     Generate network object
        /// </summary>
        /// <param name="ownerClientId">Owner ClientId</param>
        /// <param name="position">Position</param>
        /// <param name="rotation">Rotate</param>
        /// <returns>New object generated</returns>
        public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
        {
            var gameObject = Entry.Pool.PopGo($"Prefabs/{_key}");
            if (gameObject == null)
            {
                gameObject = NetworkObjectPool.Singleton.InstantiateInternal(_key, position, rotation);
            }
            else
            {
                var transform = gameObject.transform;
                transform.position = position;
                transform.rotation = rotation;
            }

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            return gameObject.GetComponent<NetworkObject>();
        }

        /// <summary>
        ///     Destroy network object
        /// </summary>
        /// <param name="networkObject">Network object</param>
        public void Destroy(NetworkObject networkObject)
        {
            var gameObject = networkObject.gameObject;
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
            Entry.Pool.PushGo($"Prefabs/{_key}", gameObject);
        }
    }
}