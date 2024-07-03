//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     UI abstract class of panels
    /// </summary>
    public abstract class UIBase : MonoBehaviour, IUIBase
    {
        /// <summary>
        ///     UI Hierarchy
        /// </summary>
        [ShowInInspector]
        public abstract UILayer Layer { get; }

        /// <summary>
        ///     UI Hidden Type
        /// </summary>
        [ShowInInspector]
        public abstract UIHideType HideType { get; }

        /// <summary>
        ///     Display UIPanel
        /// </summary>
        public virtual void Show()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        /// <summary>
        ///     Hide UIPanel
        /// </summary>
        public virtual void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}