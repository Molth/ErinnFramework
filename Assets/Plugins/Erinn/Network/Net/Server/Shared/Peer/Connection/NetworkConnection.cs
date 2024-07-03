//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;

#pragma warning disable CS8603

// ReSharper disable CommentTypo

namespace Erinn
{
    /// <summary>
    ///     Network connection
    /// </summary>
    public readonly struct NetworkConnection : ISettable
    {
        /// <summary>
        ///     IPEndPoint
        /// </summary>
        public readonly IPEndPoint IPEndPoint;

        /// <summary>
        ///     Timestamp
        /// </summary>
        public readonly long Timestamp;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="ipEndPoint">IPEndPoint</param>
        public NetworkConnection(IPEndPoint ipEndPoint)
        {
            IPEndPoint = ipEndPoint;
            Timestamp = NetworkTime.ElapsedTicks;
        }

        /// <summary>
        ///     Is IPv4
        /// </summary>
        public bool IsIPv4 => (int)IPEndPoint.Address.AddressFamily == 2;

        /// <summary>
        ///     Is IPv6
        /// </summary>
        public bool IsIPv6 => (int)IPEndPoint.Address.AddressFamily == 23;

        /// <summary>
        ///     Is it valid
        /// </summary>
        public bool IsSet => IPEndPoint != null;

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Timestamp.GetHashCode();

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the NetworkConnection value as a string</returns>
        public override string ToString() => IPEndPoint.ToString();
    }
}