//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Rainbow Elf
    /// </summary>
    public sealed class RainbowSpriteRenderer : RainbowBase
    {
        /// <summary>
        ///     Sprite renderer
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        private void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update() => _spriteRenderer.color = CompulteColor();
    }
}