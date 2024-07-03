//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;

// ReSharper disable PossibleNullReferenceException
#endif

#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public abstract partial class NetworkClientPeer : NetworkPeer, INetworkClientPeer
    {
        /// <summary>
        ///     Connected
        /// </summary>
        private InterlockedBoolean _connected;

        /// <summary>
        ///     Connected
        /// </summary>
        public bool Connected => _connected.Value;

        /// <summary>
        ///     Connected callback
        /// </summary>
        public event Action OnConnectedCallback;

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        public event Action OnDisconnectedCallback;

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public abstract uint GetRoundTripTime();

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public abstract void Send<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Disconnect
        /// </summary>
        protected bool StopListening()
        {
            _connected.Set(false);
            if (StopPolling())
                Log.Info("Client shutdown");
            return Reset();
        }

        /// <summary>
        ///     Connected callback
        /// </summary>
        protected void OnConnected()
        {
            if (!_connected.Set(true))
                return;
            Log.Info("Connected");
            OnConnectedCallback.Invoke();
        }

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        protected void OnDisconnected()
        {
            var stopPolling = StopPolling();
            var connected = _connected.Set(false);
            Shutdown();
            if (!connected)
            {
                if (stopPolling)
                    Log.Warning("Disconnected");
                return;
            }

            Log.Error("Disconnected");
            OnDisconnectedCallback.Invoke();
        }
    }
}