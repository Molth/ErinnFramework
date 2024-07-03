//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer controller interface
    /// </summary>
    public partial interface ITimerController
    {
        /// <summary>
        ///     Set Zoom
        /// </summary>
        /// <param name="scale">Zoom</param>
        void SetScale(float scale);
    }
}