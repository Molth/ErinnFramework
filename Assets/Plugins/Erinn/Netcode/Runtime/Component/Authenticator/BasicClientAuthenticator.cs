//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Client validator
    /// </summary>
    [DisallowMultipleComponent]
    public class BasicClientAuthenticator : MonoBehaviour
    {
        /// <summary>
        ///     Connection timed out
        /// </summary>
        [LabelText("Connection timed out")] [Range(5f, 30f)]
        public float Timeout = 5f;

        /// <summary>
        ///     Check for timeout protocol
        /// </summary>
        private Coroutine _checkingTimeout;

        /// <summary>
        ///     Start When calling
        /// </summary>
        private void Start()
        {
            NetcodeEvents.OnClientStarted += BeginCheckingTimeout;
            Init();
        }

        /// <summary>
        ///     Destroy When calling
        /// </summary>
        private void OnDestroy() => NetcodeEvents.OnClientStarted -= BeginCheckingTimeout;

        /// <summary>
        ///     Initialization
        /// </summary>
        protected virtual void Init()
        {
        }

        /// <summary>
        ///     Subscription events
        /// </summary>
        private void RegisterHandler()
        {
            NetcodeEvents.OnClientStopped += UnregisterHandler;
            NetcodeEvents.OnClientConnectedCallback += OnConnectedSuccess;
            NetcodeEvents.OnClientDisconnectCallback += OnDisconnected;
        }

        /// <summary>
        ///     Unsubscribe from events
        /// </summary>
        private void UnregisterHandler(bool isServer)
        {
            if (_checkingTimeout != null)
                StopCoroutine(_checkingTimeout);
            NetcodeEvents.OnClientStopped -= UnregisterHandler;
            NetcodeEvents.OnClientConnectedCallback -= OnConnectedSuccess;
            NetcodeEvents.OnClientDisconnectCallback -= OnDisconnected;
        }

        /// <summary>
        ///     Start checking timeout
        /// </summary>
        private void BeginCheckingTimeout()
        {
            if (NetcodeSystem.IsServer)
                return;
            RegisterHandler();
            if (_checkingTimeout != null)
                StopCoroutine(_checkingTimeout);
            _checkingTimeout = StartCoroutine(CheckingTimeout());
        }

        /// <summary>
        ///     Check timeout
        /// </summary>
        private IEnumerator CheckingTimeout()
        {
            var time = Time.unscaledTime + Timeout;
            while (true)
            {
                if (Time.unscaledTime >= time)
                {
                    NetcodeClient.StopClient();
                    OnTimeout();
                    break;
                }

                yield return null;
            }
        }

        /// <summary>
        ///     Timed out call
        /// </summary>
        protected virtual void OnTimeout() => Log.Warning("Timeout");

        /// <summary>
        ///     Call upon successful connection
        ///     Unsubscribe from events
        /// </summary>
        private void OnConnectedSuccess(ulong clientId)
        {
            UnregisterHandler(false);
            OnConnected();
        }

        /// <summary>
        ///     Call upon successful connection
        /// </summary>
        protected virtual void OnConnected()
        {
        }

        /// <summary>
        ///     Call when connection fails
        /// </summary>
        private void OnDisconnected(ulong clientId) => OnDisconnected();

        /// <summary>
        ///     Call when connection fails
        /// </summary>
        protected virtual void OnDisconnected()
        {
            if (string.IsNullOrEmpty(NetcodeClient.DisconnectReason))
                return;
            Log.Error(NetcodeClient.DisconnectReason);
        }
    }
}