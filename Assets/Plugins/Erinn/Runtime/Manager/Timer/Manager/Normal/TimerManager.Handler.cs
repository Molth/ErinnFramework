//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer Manager
    /// </summary>
    internal sealed partial class TimerManager
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerSet.Stop(TimerHandler handler) => Stop(handler);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerSet.Play(TimerHandler handler) => Play(handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerSet.Pause(TimerHandler handler) => Pause(handler);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerManager.Reset(TimerHandler handler) => Reset(handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        bool ITimerSet.SetForever(TimerHandler handler, bool forever) => SetForever(handler, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        bool ITimerSet.SetLoops(TimerHandler handler, uint loops) => SetLoops(handler, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool ITimerSet.GetData(TimerHandler handler, out TimerData data) => GetData(handler, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool ITimerManager.GetDelta(TimerHandler handler, out float delta) => GetDelta(handler, out delta);

        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool Stop(TimerHandler handler) => Stop(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool Play(TimerHandler handler) => Play(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool Pause(TimerHandler handler) => Pause(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool Reset(TimerHandler handler) => Reset(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        public static bool SetForever(TimerHandler handler, bool forever) => SetForever(handler.Id, handler.Timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        public static bool SetLoops(TimerHandler handler, uint loops) => SetLoops(handler.Id, handler.Timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public static bool GetData(TimerHandler handler, out TimerData data) => GetData(handler.Id, handler.Timestamp, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public static bool GetDelta(TimerHandler handler, out float delta) => GetDelta(handler.Id, handler.Timestamp, out delta);
    }
}