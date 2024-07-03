//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    internal sealed partial class Timer
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval time</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Have you already</param>
        /// <param name="onComplete">Callback upon completion</param>
        public void Create(uint id, ulong timestamp, ulong tick, uint loops, bool forever, Action<uint> onComplete)
        {
            Id = id;
            State = TimerState.Run;
            Timestamp = timestamp;
            Target = timestamp + tick;
            Tick = tick;
            Loops = loops;
            Forever = forever;
            OnComplete = onComplete;
        }

        /// <summary>
        ///     Polling
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        public void Poll(ulong timestamp)
        {
            switch (State)
            {
                case TimerState.Stop:
                    break;
                case TimerState.Pause:
                {
                    if (timestamp >= Target + Tick)
                        State = TimerState.Stop;
                    break;
                }
                case TimerState.Run:
                {
                    if (timestamp >= Target)
                    {
                        try
                        {
                            OnComplete(Id);
                        }
                        catch (Exception e)
                        {
                            State = TimerState.Stop;
                            Log.Error($"Timer: [{Id}] [{e}]");
                            return;
                        }

                        if (Forever)
                        {
                            Target += Tick;
                        }
                        else
                        {
                            if (Loops > 0)
                            {
                                Loops--;
                                Target += Tick;
                            }
                            else
                            {
                                State = TimerState.Stop;
                            }
                        }
                    }

                    break;
                }
            }
        }
    }
}