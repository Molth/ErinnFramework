//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
using System.Threading.Tasks;
#endif

#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Message endPoint
    /// </summary>
    public sealed class MessageEndPoint : IMessageEndPoint
    {
        /// <summary>
        ///     Current index
        /// </summary>
        private uint _serialNumber;

        /// <summary>
        ///     Message entries
        /// </summary>
        private readonly ConcurrentDictionary<uint, MessageEntry> _messageEntryDictionary = new();

        /// <summary>
        ///     Acquire message async
        /// </summary>
        /// <param name="timeout">Timeout</param>
        /// <param name="serialNumber">SerialNumber</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Message</returns>
        public Task<MessageResult<T>> AcquireAsync<T>(int timeout, out uint serialNumber)
        {
            if (timeout < 0)
                throw new InvalidOperationException("Timeout must be greater than 0.");
            var taskCompletionSource = new TaskCompletionSource<object>();
            var messageEntry = new MessageEntry(taskCompletionSource);
            serialNumber = _serialNumber;
            Interlocked.Add(ref Unsafe.As<uint, int>(ref _serialNumber), 1);
            _messageEntryDictionary[serialNumber] = messageEntry;
            return messageEntry.GetResult<T>(this, serialNumber, timeout);
        }

        /// <summary>
        ///     Checked
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        public bool Checked(uint serialNumber) => _messageEntryDictionary.ContainsKey(serialNumber);

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="obj">object</param>
        public bool SetResult(uint serialNumber, object obj)
        {
            if (!_messageEntryDictionary.TryRemove(serialNumber, out var messageEntry))
                return false;
            messageEntry.SetResult(obj);
            return true;
        }

        /// <summary>
        ///     Clear
        /// </summary>
        public void Clear()
        {
            foreach (var messageEntry in _messageEntryDictionary.Values)
                messageEntry.SetResult(null);
            _messageEntryDictionary.Clear();
        }

        /// <summary>
        ///     Acquire message async
        /// </summary>
        /// <param name="timeout">Timeout</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="serialNumber">SerialNumber</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Message</returns>
        public Task<MessageResult<T>> AcquireAsync<T>(int timeout, int cookie, out uint serialNumber)
        {
            if (timeout < 0)
                throw new InvalidOperationException("Timeout must be greater than 0.");
            var taskCompletionSource = new TaskCompletionSource<object>();
            var messageEntry = new MessageEntry(taskCompletionSource, cookie);
            serialNumber = _serialNumber;
            Interlocked.Add(ref Unsafe.As<uint, int>(ref _serialNumber), 1);
            _messageEntryDictionary[serialNumber] = messageEntry;
            return messageEntry.GetResult<T>(this, serialNumber, timeout);
        }

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="obj">object</param>
        public bool SetResult(uint serialNumber, int cookie, object obj)
        {
            if (!_messageEntryDictionary.TryGetValue(serialNumber, out var messageEntry))
                return false;
            if (messageEntry.SetResult(cookie, obj))
            {
                _messageEntryDictionary.TryRemove(serialNumber, out _);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Remove
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        private void Remove(uint serialNumber) => _messageEntryDictionary.TryRemove(serialNumber, out _);

        /// <summary>
        ///     Entry
        /// </summary>
        private readonly struct MessageEntry
        {
            /// <summary>
            ///     TaskCompletionSource
            /// </summary>
            private readonly TaskCompletionSource<object> _taskCompletionSource;

            /// <summary>
            ///     Cookie
            /// </summary>
            private readonly int _cookie;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="taskCompletionSource">TaskCompletionSource</param>
            /// <param name="cookie">Cookie</param>
            public MessageEntry(TaskCompletionSource<object> taskCompletionSource, int cookie = 0)
            {
                _taskCompletionSource = taskCompletionSource;
                _cookie = cookie;
            }

            /// <summary>
            ///     Get result
            /// </summary>
            /// <param name="messageEndPoint">MessageEndPoint</param>
            /// <param name="serialNumber">SerialNumber</param>
            /// <param name="timeout">Timeout</param>
            /// <typeparam name="T">Type</typeparam>
            /// <returns>Message</returns>
            public async Task<MessageResult<T>> GetResult<T>(MessageEndPoint messageEndPoint, uint serialNumber, int timeout)
            {
                var task = _taskCompletionSource.Task;
                var timeoutTask = Task.Delay(timeout);
                var completedTask = await Task.WhenAny(task, timeoutTask);
                if (completedTask == task)
                {
                    if (task.Result == null)
                        return new MessageResult<T>(MessageState.Nil);
                    try
                    {
                        var obj = (T)task.Result;
                        return new MessageResult<T>(MessageState.Success, obj);
                    }
                    catch
                    {
                        return new MessageResult<T>(MessageState.Invalid);
                    }
                }

                _taskCompletionSource.SetResult(null);
                messageEndPoint.Remove(serialNumber);
                return new MessageResult<T>(MessageState.Timeout);
            }

            /// <summary>
            ///     Set result
            /// </summary>
            /// <param name="obj">object</param>
            public void SetResult(object obj) => _taskCompletionSource.SetResult(obj);

            /// <summary>
            ///     Set result
            /// </summary>
            /// <param name="cookie">Cookie</param>
            /// <param name="obj">object</param>
            public bool SetResult(int cookie, object obj)
            {
                if (_cookie != cookie)
                    return false;
                _taskCompletionSource.SetResult(obj);
                return true;
            }
        }
    }
}