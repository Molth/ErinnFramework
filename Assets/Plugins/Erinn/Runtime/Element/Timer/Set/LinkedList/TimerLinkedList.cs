//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    public sealed partial class TimerLinkedList : ITimerLinkedList
    {
        /// <summary>
        ///     Time Poll
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        public void Poll(ulong timestamp)
        {
#if !UNITY_2021_3_OR_NEWER
            lock (_locker)
            {
#endif
                if (_head == null)
                {
                    Dequeue();
                    return;
                }

                var set = false;
                Timer startOfRange = null;
                var node = _head;
                node.Poll(timestamp);
                if (node.State == TimerState.Stop)
                {
                    Recycle(node);
                    set = true;
                }

                while (true)
                {
                    var next = node.Next;
                    if (next == null)
                        break;
                    next.Poll(timestamp);
                    if (next.State == TimerState.Stop)
                    {
                        Recycle(next);
                        if (!set)
                        {
                            set = true;
                            startOfRange = node;
                        }
                    }
                    else if (set)
                    {
                        RemoveRange(startOfRange, node);
                        set = false;
                    }

                    node = next;
                }

                if (set)
                    RemoveRange(startOfRange, null);
                RecycleBuffer();
                Dequeue();
#if !UNITY_2021_3_OR_NEWER
            }
#endif
        }

        /// <summary>
        ///     Empty
        /// </summary>
        public void Clear()
        {
#if !UNITY_2021_3_OR_NEWER
            lock (_locker)
            {
#endif
                Release();
                RecycleQueue();
                if (_head == null)
                    return;
                var node = _head;
                do
                {
                    node.OnComplete = null;
                    _timerPool.Push(node);
                    node = node.Next;
                } while (node != null);

                _head = null;
                _tail = null;
#if !UNITY_2021_3_OR_NEWER
            }
#endif
        }

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose()
        {
#if !UNITY_2021_3_OR_NEWER
            lock (_locker)
            {
#endif
                Release();
                _incomingTimerQueue.Clear();
                _timerBuffer.Clear();
                _timerPool.Clear();
                _head = null;
                _tail = null;
#if !UNITY_2021_3_OR_NEWER
            }
#endif
        }

        /// <summary>
        ///     Release
        /// </summary>
        private void Release()
        {
            Count = 0;
            _indexPool.Clear();
            _timerDictionary.Clear();
        }
    }
}