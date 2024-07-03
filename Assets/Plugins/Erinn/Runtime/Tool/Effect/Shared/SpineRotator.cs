//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     SpineRotate
    /// </summary>
    public class SpineRotator : MonoBehaviour
    {
        /// <summary>
        ///     Enable rotation
        /// </summary>
        public bool EnableRotate;

        /// <summary>
        ///     Speed
        /// </summary>
        public float Speed;

        /// <summary>
        ///     Wave
        /// </summary>
        public AnimationCurve RotationAnimationCurve;

        /// <summary>
        ///     AwakeWhen calling
        /// </summary>
        private void Awake()
        {
            RotationAnimationCurve ??= AnimationCurve.Linear(0f, 0f, 1f, 1f);
            if (Speed == 0f)
                Speed = 0.5f;
        }

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        private void Update()
        {
            if (EnableRotate && Speed != 0.0f)
                transform.localEulerAngles = new Vector3(0f, 0f, -360f * RotationAnimationCurve.Evaluate(Speed * Time.unscaledTime % 1f));
        }
    }
}