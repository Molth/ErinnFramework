//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Timer controller
    /// </summary>
    internal sealed partial class TimerController
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, Action<uint> onComplete) => Create(second, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, uint loops, Action<uint> onComplete) => Create(second, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, bool forever, Action<uint> onComplete) => Create(second, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Second</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, uint loops, bool forever, Action<uint> onComplete)
        {
            Stop();
            _handler = TimerManager.Create((ulong)(second * Scale * 1000f), loops, forever, onComplete);
            return _handler.Id;
        }

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, Action onComplete) => Create(second, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, uint loops, Action onComplete) => Create(second, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, bool forever, Action onComplete) => Create(second, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="second">Second</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public uint Create(float second, uint loops, bool forever, Action onComplete) => Create(second, loops, forever, _ => onComplete());
    }
}