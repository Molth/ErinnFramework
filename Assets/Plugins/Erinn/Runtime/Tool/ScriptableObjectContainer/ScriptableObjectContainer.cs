//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     ScriptableObjectContainer
    /// </summary>
    [Serializable]
    public abstract class ScriptableObjectContainer<T> : ScriptableObject, IScriptableObjectContainer
    {
        /// <summary>
        ///     Element
        /// </summary>
        [SerializeField] public T[] Elements;
    }
}