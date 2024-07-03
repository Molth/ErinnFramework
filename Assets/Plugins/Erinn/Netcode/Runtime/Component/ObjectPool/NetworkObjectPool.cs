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
    ///     Network object pool
    /// </summary>
    public sealed class NetworkObjectPool : MonoSingleton<NetworkObjectPool>
    {
        /// <summary>
        ///     Prefabricated components
        /// </summary>
        [SerializeField] private NetworkPrefabsList[] _prefabs;

        /// <summary>
        ///     Prefabricated components
        /// </summary>
        private HashSet<GameObject> _prefabHashSet;

        /// <summary>
        ///     Prefabricated component mapping
        /// </summary>
        private Map<GameObject, string> _prefabMap;

        /// <summary>
        ///     Call on load
        /// </summary>
        private void Start()
        {
            if (_prefabs == null)
                return;
            ValidatePrefabInternal();
            RegisterPrefabInternal();
        }

        /// <summary>
        ///     Verify prefabricated components
        /// </summary>
        private void ValidatePrefabInternal()
        {
            _prefabHashSet = new HashSet<GameObject>();
            for (var i = 0; i < _prefabs.Length; ++i)
            {
                var prefabs = _prefabs[i].PrefabList;
                for (var j = 0; j < prefabs.Count; ++j)
                {
                    var prefab = prefabs[j];
                    if (prefab == null)
                    {
                        Log.Error($"Prefabricated components cannot be empty[{j}]");
                        continue;
                    }

                    if (!_prefabHashSet.Add(prefab.Prefab))
                        Log.Error($"Prefabricated components are duplicated[{j}]");
                }
            }
        }

        /// <summary>
        ///     Register Prefabricated Component Handle
        /// </summary>
        private void RegisterPrefabInternal()
        {
            _prefabMap = new Map<GameObject, string>();
            foreach (var prefab in _prefabHashSet)
            {
                var key = prefab.name;
                _prefabMap[prefab] = key;
                NetworkManager.Singleton.PrefabHandler.AddHandler(prefab, new NetworkPooledPrefabInstanceHandler(key));
            }
        }

        /// <summary>
        ///     Get network objects
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Obtained network objects</returns>
        public NetworkObject Pop(string key)
        {
            if (!_prefabMap.TryGetKey(key, out var prefab))
                return null;
            var go = Entry.Pool.PopGo($"Prefabs/{key}");
            if (go == null)
            {
                go = Instantiate(prefab);
                go.name = key;
            }

            return go.GetComponent<NetworkObject>();
        }

        /// <summary>
        ///     Get network object
        /// </summary>
        /// <param name="prefab">Prefabricated components</param>
        /// <returns>Obtained network object</returns>
        public NetworkObject Pop(GameObject prefab)
        {
            if (!_prefabMap.TryGetValue(prefab, out var key))
                return null;
            var go = Entry.Pool.PopGo($"Prefabs/{key}");
            if (go == null)
            {
                go = Instantiate(prefab);
                go.name = key;
            }

            return go.GetComponent<NetworkObject>();
        }

        /// <summary>
        ///     Get network object
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="position">Position</param>
        /// <param name="rotation">Rotate</param>
        /// <returns>Obtained network object</returns>
        public NetworkObject Pop(string key, Vector3 position, Quaternion rotation)
        {
            if (!_prefabMap.TryGetKey(key, out var prefab))
                return null;
            var go = Entry.Pool.PopGo($"Prefabs/{key}");
            if (go == null)
            {
                go = Instantiate(prefab, position, rotation);
                go.name = key;
            }
            else
            {
                go.transform.position = position;
                go.transform.rotation = rotation;
            }

            return go.GetComponent<NetworkObject>();
        }

        /// <summary>
        ///     Get network object
        /// </summary>
        /// <param name="prefab">Prefabricated components</param>
        /// <param name="position">Position</param>
        /// <param name="rotation">Rotate</param>
        /// <returns>Obtained network object</returns>
        public NetworkObject Pop(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_prefabMap.TryGetValue(prefab, out var key))
                return null;
            var go = Entry.Pool.PopGo($"Prefabs/{key}");
            if (go == null)
            {
                go = Instantiate(prefab, position, rotation);
                go.name = key;
            }
            else
            {
                go.transform.position = position;
                go.transform.rotation = rotation;
            }

            return go.GetComponent<NetworkObject>();
        }

        /// <summary>
        ///     Clear object pool
        /// </summary>
        public void Clear(GameObject prefab)
        {
            if (!_prefabMap.TryGetValue(prefab, out var key))
                return;
            Entry.Pool.ClearGo($"Prefabs/{key}");
        }

        /// <summary>
        ///     Clear object pool
        /// </summary>
        public void ClearAll()
        {
            foreach (var key in _prefabMap.Values)
                Entry.Pool.ClearGo($"Prefabs/{key}");
        }

        /// <summary>
        ///     Clone network object
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="position">Position</param>
        /// <param name="rotation">Rotate</param>
        /// <returns>Obtained network object</returns>
        internal GameObject InstantiateInternal(string key, Vector3 position, Quaternion rotation)
        {
            var prefab = _prefabMap.GetKey(key);
            return Instantiate(prefab, position, rotation);
        }
    }
}