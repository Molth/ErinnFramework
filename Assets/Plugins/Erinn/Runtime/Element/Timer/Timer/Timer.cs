//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    internal sealed partial class Timer
    {
        /// <summary>
        ///     Cease
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <returns>Whether successful</returns>
        public bool Stop(ulong timestamp)
        {
            if (Timestamp != timestamp)
                return false;
            if (State == TimerState.Stop)
                return false;
            State = TimerState.Stop;
            return true;
        }

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <returns>Successfully stopped</returns>
        public bool Play(ulong timestamp)
        {
            if (Timestamp != timestamp)
                return false;
            if (State != TimerState.Pause)
                return false;
            State = TimerState.Run;
            return true;
        }

        /// <summary>
        ///     Suspend
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <returns>Whether successful</returns>
        public bool Pause(ulong timestamp)
        {
            if (Timestamp != timestamp)
                return false;
            if (State != TimerState.Run)
                return false;
            State = TimerState.Pause;
            return true;
        }

        /// <summary>
        ///     Reset
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="newTimestamp">Current timestamp</param>
        /// <returns>Whether successful</returns>
        public bool Reset(ulong timestamp, ulong newTimestamp)
        {
            if (Timestamp != timestamp)
                return false;
            if (State == TimerState.Stop)
                return false;
            Target = newTimestamp + Tick;
            return true;
        }

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        /// <returns>Whether successful</returns>
        public bool SetForever(ulong timestamp, bool forever)
        {
            if (Timestamp != timestamp)
                return false;
            if (State == TimerState.Stop)
                return false;
            Forever = forever;
            return true;
        }

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        /// <returns>Whether successful</returns>
        public bool SetLoops(ulong timestamp, uint loops)
        {
            if (Timestamp != timestamp)
                return false;
            if (State == TimerState.Stop)
                return false;
            Loops = loops;
            return true;
        }

        /// <summary>
        ///     Get data
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public bool GetData(ulong timestamp, out TimerData data)
        {
            if (Timestamp != timestamp)
            {
                data = default;
                return false;
            }

            data = new TimerData(this);
            return true;
        }

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="currentTimestamp">Current timestamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public bool GetDelta(ulong timestamp, ulong currentTimestamp, out ulong delta)
        {
            if (Timestamp != timestamp)
            {
                delta = 0UL;
                return false;
            }

            if (Target > currentTimestamp)
            {
                delta = Target - currentTimestamp;
                return true;
            }

            delta = 0U;
            return false;
        }
    }
}