//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using TMPro;

namespace Erinn
{
    /// <summary>
    ///     Rainbow Text
    /// </summary>
    public sealed class RainbowTextMeshPro : RainbowBase
    {
        /// <summary>
        ///     Text
        /// </summary>
        private TMP_Text _text;

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        private void Start() => _text = GetComponent<TMP_Text>();

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update() => _text.color = CompulteColor();
    }
}