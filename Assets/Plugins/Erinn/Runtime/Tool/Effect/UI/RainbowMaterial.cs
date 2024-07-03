//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Rainbow material
    /// </summary>
    public sealed class RainbowMaterial : RainbowBase
    {
        /// <summary>
        ///     Text
        /// </summary>
        private Material _material;

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        private void Start() => _material = GetComponent<Material>();

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update() => _material.color = CompulteColor();
    }
}