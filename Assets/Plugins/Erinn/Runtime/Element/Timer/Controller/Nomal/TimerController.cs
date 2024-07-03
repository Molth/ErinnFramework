//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer controller
    /// </summary>
    internal sealed partial class TimerController : ITimerController
    {
        /// <summary>
        ///     Zoom
        /// </summary>
        public float Scale { get; private set; } = 1.0f;

        /// <summary>
        ///     Set Zoom
        /// </summary>
        /// <param name="scale">Zoom</param>
        public void SetScale(float scale)
        {
            if (scale < 0.0f)
                scale = 0.0f;
            Scale = scale;
        }
    }
}