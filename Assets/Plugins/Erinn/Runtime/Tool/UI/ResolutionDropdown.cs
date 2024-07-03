//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using TMPro;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Resolution dropdown menu
    /// </summary>
    public class ResolutionDropdown : MonoBehaviour
    {
        /// <summary>
        ///     Drop-down menu
        /// </summary>
        public TMP_Dropdown TmpDropdown;

        /// <summary>
        ///     AwakeWhen calling
        /// </summary>
        protected virtual void Awake() => Init();

        /// <summary>
        ///     Initialization
        /// </summary>
        public void Init()
        {
            if (TmpDropdown == null)
                TmpDropdown = GetComponent<TMP_Dropdown>();
            if (TmpDropdown == null)
                return;
            var options = ConfigInfo.GetResolutionOptions();
            TmpDropdown.AddOptions(options);
            TmpDropdown.value = ConfigInfo.GetResolutionSelect();
            TmpDropdown.onValueChanged.AddListener(ChangeResolution);
        }

        /// <summary>
        ///     Switch resolution
        /// </summary>
        /// <param name="select">Choice</param>
        public void ChangeResolution(int select) => ConfigInfo.SetResolution(select);
    }
}