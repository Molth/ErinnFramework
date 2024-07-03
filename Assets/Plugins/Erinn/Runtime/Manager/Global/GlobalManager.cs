//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Global Manager
    /// </summary>
    [HideMonoScript]
    [DisallowMultipleComponent]
    public sealed partial class GlobalManager : MonoBehaviour
    {
        /// <summary>
        ///     Single example
        /// </summary>
        private static GlobalManager _instance;

        /// <summary>
        ///     Lock
        /// </summary>
        private static readonly object Locker = new();

        /// <summary>
        ///     Single example
        /// </summary>
        internal static GlobalManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    _instance = FindObjectOfType<GlobalManager>();
                    if (_instance != null)
                        return _instance;
                    var prefab = Resources.Load<GameObject>("Erinn/GlobalManager");
                    var obj = Instantiate(prefab);
                    obj.transform.position = Vector3.zero;
                    obj.name = "GlobalManager";
                    _instance = obj.GetComponent<GlobalManager>();
                }

                return _instance;
            }
        }
    }
}