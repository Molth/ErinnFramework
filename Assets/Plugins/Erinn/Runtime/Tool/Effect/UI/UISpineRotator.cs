//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     UIOfSpineRotate
    /// </summary>
    public sealed class UISpineRotator : MonoBehaviour
    {
        /// <summary>
        ///     Rotation speed
        /// </summary>
        public float Speed = 0.5f;

        /// <summary>
        ///     Wave
        /// </summary>
        private AnimationCurve _rotationAnimationCurve;

        /// <summary>
        ///     AwakeWhen calling
        /// </summary>
        private void Awake() => _rotationAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update()
        {
            if (Speed != 0f)
                transform.localEulerAngles = new Vector3(0f, 0f, -360f * _rotationAnimationCurve.Evaluate(Speed * Time.unscaledTime % 1f));
        }
    }
}