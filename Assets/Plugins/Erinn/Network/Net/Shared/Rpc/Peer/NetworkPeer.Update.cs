//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Network Peer
    /// </summary>
    public abstract partial class NetworkPeer
    {
        /// <summary>
        ///     Listening
        /// </summary>
        private InterlockedBoolean _isListening;

#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Running
        /// </summary>
        private InterlockedBoolean _isRunning;
#endif

        /// <summary>
        ///     State Lock
        /// </summary>
        private InterlockedBoolean _stateLock;

        /// <summary>
        ///     Listening
        /// </summary>
        public bool IsListening => _isListening.Value;

        /// <summary>
        ///     Running
        /// </summary>
        public bool IsRunning =>
#if !UNITY_2021_3_OR_NEWER && !GODOT
            _isRunning.Value;
#else
            false;
#endif

        /// <summary>
        ///     Update
        /// </summary>
        public void Update()
        {
#if !UNITY_2021_3_OR_NEWER && !GODOT
            if (!IsListening || IsRunning)
                return;
#else
            if (!IsListening)
                return;
#endif
            PollSafely();
        }

        /// <summary>
        ///     Start polling
        /// </summary>
        protected void StartPolling()
        {
#if !UNITY_2021_3_OR_NEWER && !GODOT
            if (!_isListening.Set(true))
            {
                Log.Warning("Started");
                return;
            }

            if (!Setting.Managed)
                return;
            if (!_isRunning.Set(true))
            {
                Log.Warning("Running");
                return;
            }

            new Thread(!Setting.NoDelay ? Polling : PollingNoDelay) { IsBackground = true }.Start();
#else
            if (_isListening.Set(true))
            {
                EngineLoop.AddToPlayerLoop(Update);
                return;
            }

            Log.Warning("Started");
#endif
        }

#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Polling
        /// </summary>
        private void Polling()
        {
            var tick = (int)Setting.Tick;
            while (IsListening)
            {
                PollSafely();
                Thread.Sleep(tick);
            }

            OnPollingOver();
        }

        /// <summary>
        ///     Polling no delay
        /// </summary>
        private void PollingNoDelay()
        {
            while (IsListening)
                PollSafely();
            OnPollingOver();
        }

        /// <summary>
        ///     Polling over callback
        /// </summary>
        private void OnPollingOver() => _isRunning.Set(false);
#endif

        /// <summary>
        ///     Poll safely
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PollSafely()
        {
            if (!_stateLock.Set(true))
                return;
            try
            {
                Poll();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            _stateLock.Set(false);
        }

        /// <summary>
        ///     Poll
        /// </summary>
        protected abstract void Poll();

        /// <summary>
        ///     Stop polling
        /// </summary>
        protected bool StopPolling()
#if UNITY_2021_3_OR_NEWER || GODOT
        {
            var result = _isListening.Set(false);
            if (result)
                EngineLoop.RemoveToPlayerLoop(Update);
            return result;
        }
#else
            => _isListening.Set(false);
#endif
    }
}