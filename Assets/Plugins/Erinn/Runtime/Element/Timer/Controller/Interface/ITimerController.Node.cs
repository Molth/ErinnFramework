//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Timer controller interface
    /// </summary>
    public partial interface ITimerController
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, uint loops, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, bool forever, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Second</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, uint loops, bool forever, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, Action onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, uint loops, Action onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, bool forever, Action onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Second</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        uint Create(float second, uint loops, bool forever, Action onComplete);
    }
}