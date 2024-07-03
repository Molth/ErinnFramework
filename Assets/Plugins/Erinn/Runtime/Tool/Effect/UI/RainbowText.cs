//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine.UI;

namespace Erinn
{
    /// <summary>
    ///     Rainbow Text
    /// </summary>
    public sealed class RainbowText : RainbowBase
    {
        /// <summary>
        ///     Text
        /// </summary>
        private Text _text;

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        private void Start() => _text = GetComponent<Text>();

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update() => _text.color = CompulteColor();
    }
}