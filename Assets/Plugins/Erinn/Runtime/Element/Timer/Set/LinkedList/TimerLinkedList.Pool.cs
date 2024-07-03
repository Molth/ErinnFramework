//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;

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
        ///     Attempt to obtainTimer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timer">Timer</param>
        /// <returns>Has it been successfully obtained</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGet(uint id, out Timer timer) => _timerDictionary.TryGetValue(id, out timer);

        /// <summary>
        ///     Recycling timer
        /// </summary>
        /// <param name="timer">Timer</param>
        private void Recycle(Timer timer)
        {
            var id = timer.Id;
            if (!_timerDictionary.Remove(id))
                return;
            _indexPool.Return(id);
            Push(timer);
        }

        /// <summary>
        ///     Recycle buffer
        /// </summary>
        private void RecycleBuffer()
        {
            if (_timerBuffer.Count == 0)
                return;
            foreach (var timer in _timerBuffer)
                _timerPool.Push(timer);
            _timerBuffer.Clear();
        }

        /// <summary>
        ///     Get Timer
        /// </summary>
        /// <returns>Timer obtained</returns>
        private Timer Pop()
        {
            Count++;
            return _timerPool.TryPop(out var timer) ? timer : new Timer();
        }

        /// <summary>
        ///     Recycling timer
        /// </summary>
        /// <param name="timer">Recycled timer</param>
        private void Push(Timer timer)
        {
            Count--;
            timer.OnComplete = null;
            _timerBuffer.Push(timer);
        }

        /// <summary>
        ///     Join the team
        /// </summary>
        /// <param name="timer">Timer</param>
        private void Enqueue(Timer timer) => _incomingTimerQueue.Enqueue(timer);

        /// <summary>
        ///     Leaving the team
        /// </summary>
        private void Dequeue()
        {
            if (_incomingTimerQueue.Count == 0)
                return;
            foreach (var timer in _incomingTimerQueue)
                Add(timer);
            _incomingTimerQueue.Clear();
        }

        /// <summary>
        ///     Recycle queue
        /// </summary>
        private void RecycleQueue()
        {
            if (_incomingTimerQueue.Count == 0)
                return;
            foreach (var timer in _incomingTimerQueue)
                _timerPool.Push(timer);
            _incomingTimerQueue.Clear();
        }
    }
}