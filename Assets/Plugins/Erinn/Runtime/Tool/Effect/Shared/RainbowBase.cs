//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Rainbow base class
    /// </summary>
    public abstract class RainbowBase : MonoBehaviour
    {
        /// <summary>
        ///     Speed
        /// </summary>
        [Range(-1f, 1f)] [LabelText("Speed")] [SerializeField]
        private float _speed = 0.5f;

        /// <summary>
        ///     Saturation
        /// </summary>
        [Range(0f, 1f)] [LabelText("Saturation")] [SerializeField]
        private float _saturation = 1f;

        /// <summary>
        ///     Wave
        /// </summary>
        [LabelText("Wave")] [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        /// <summary>
        ///     Random period
        /// </summary>
        [LabelText("Random period")] [DisableInPlayMode] [SerializeField]
        private bool _randomPeriod = true;

        /// <summary>
        ///     Cycle
        /// </summary>
        private float _period;

        /// <summary>
        ///     Call on load
        /// </summary>
        protected void Awake() => _period = _randomPeriod ? MathV.Next(0f, 1f) : 0f;

        /// <summary>
        ///     Get colors
        /// </summary>
        /// <returns>Obtained colors</returns>
        protected Color CompulteColor() => Color.HSVToRGB(_animationCurve.Evaluate((_speed * Time.unscaledTime + _period) % 1f), _saturation, 1f);
    }
}