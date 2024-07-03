//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#pragma warning disable CS8601
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    public sealed partial class TimerLinkedList
    {
        /// <summary>
        ///     Add Timer
        /// </summary>
        /// <param name="timer">Timer</param>
        private void Add(Timer timer)
        {
            timer.Next = null;
            if (_head == null)
            {
                _head = timer;
                _tail = timer;
            }
            else
            {
                _tail.Next = timer;
                _tail = timer;
            }
        }

        /// <summary>
        ///     Remove timer range
        /// </summary>
        /// <param name="previous">Start</param>
        /// <param name="last">Finish</param>
        private void RemoveRange(Timer previous, Timer last)
        {
            if (previous != null)
                previous.Next = last?.Next;
            else
                _head = last?.Next;
            if (last == null)
                _tail = previous;
        }
    }
}