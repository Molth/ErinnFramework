//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin All rights reserved
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network Peer
    /// </summary>
    public abstract partial class NetworkPeer : INetworkPeer
    {
        /// <summary>
        ///     Represents whether the peer is set
        /// </summary>
        private InterlockedBoolean _isSet;

        /// <summary>
        ///     Represents whether the peer is set
        /// </summary>
        public bool IsSet => _isSet.Value;

        /// <summary>
        ///     Protocol type
        /// </summary>
        public abstract NetworkProtocolType ProtocolType { get; }

        /// <summary>
        ///     Release
        /// </summary>
        public abstract bool Shutdown();

        /// <summary>
        ///     Sets the peer
        /// </summary>
        /// <returns>Whether the peer was successfully set</returns>
        protected bool Set() => _isSet.Set(true);

        /// <summary>
        ///     Resets the peer
        /// </summary>
        /// <returns>Whether the peer was successfully reset</returns>
        protected bool Reset() => _isSet.Set(false);

        /// <summary>
        ///     Check set
        /// </summary>
        /// <returns>Set</returns>
        private bool CheckSet()
        {
            if (!IsSet)
                return true;
            Log.Warning("Set");
            return false;
        }

        /// <summary>
        ///     Start listening
        /// </summary>
        /// <returns>Whether listening started successfully</returns>
#if !UNITY_2021_3_OR_NEWER && !GODOT
        protected bool StartListening()
        {
            if (IsListening)
            {
                Log.Warning("Started");
                return false;
            }

            if (!IsRunning)
                return Set();
            Log.Warning("Running");
            return false;
        }
#else
        protected bool StartListening() => Set();
#endif
    }
}