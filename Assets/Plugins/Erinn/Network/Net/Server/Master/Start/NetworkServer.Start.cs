//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
// ReSharper disable PossibleNullReferenceException
#endif

#pragma warning disable CS8602

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public sealed partial class NetworkServer
    {
        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public bool Start(ushort port, uint maxClients = 4095U)
        {
            if (_peer == null)
            {
                Log.Warning("Uninitialized");
                return false;
            }

            return _peer.Start(port, maxClients);
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="setting">Server Setting</param>
        public bool Start(NetworkServerSetting setting) => Start(setting.ProtocolType, setting.Port, setting.MaxClients);

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public bool Start(NetworkProtocolType networkProtocolType, ushort port, uint maxClients = 4095U)
        {
            if (_peer != null)
            {
                if (networkProtocolType != _peer.ProtocolType)
                {
                    Log.Warning($"Inconsistent types: [{_peer.ProtocolType}]");
                    return false;
                }

                return _peer.Start(port, maxClients);
            }

            if (!Create(networkProtocolType))
                return false;
            _peer.Start(port, maxClients);
            return true;
        }
    }
}