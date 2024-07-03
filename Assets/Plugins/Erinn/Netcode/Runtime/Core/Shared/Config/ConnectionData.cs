//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Unity.Netcode.Transports.UTP;

namespace Erinn
{
    /// <summary>
    ///     Connect data
    /// </summary>
    [Serializable]
    public struct ConnectionData
    {
        /// <summary>
        ///     Address
        /// </summary>
        public string Address;

        /// <summary>
        ///     Port
        /// </summary>
        public ushort Port;

        /// <summary>
        ///     Structure
        /// </summary>
        public ConnectionData(string address, ushort port)
        {
            Address = address;
            Port = port;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        public ConnectionData(string address, ushort port, string listenAddress)
        {
            Address = address;
            Port = port;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="connectionAddressData">Connect data</param>
        public ConnectionData(UnityTransport.ConnectionAddressData connectionAddressData)
        {
            Address = connectionAddressData.Address;
            Port = connectionAddressData.Port;
        }
    }
}