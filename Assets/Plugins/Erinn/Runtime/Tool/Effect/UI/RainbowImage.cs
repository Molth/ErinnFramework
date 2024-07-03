//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine.UI;

namespace Erinn
{
    /// <summary>
    ///     Rainbow image
    /// </summary>
    public sealed class RainbowImage : RainbowBase
    {
        /// <summary>
        ///     Picture
        /// </summary>
        private Image _image;

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        private void Start() => _image = GetComponent<Image>();

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update() => _image.color = CompulteColor();
    }
}