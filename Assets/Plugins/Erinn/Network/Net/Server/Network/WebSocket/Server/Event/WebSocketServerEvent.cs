//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Receive event
    /// </summary>
    internal readonly struct WebSocketServerEvent
    {
        /// <summary>
        ///     Type
        /// </summary>
        public readonly NetworkEventType EventType;

        /// <summary>
        ///     Index
        /// </summary>
        public readonly uint Id;

        /// <summary>
        ///     Data
        /// </summary>
        public readonly NetworkPacket Data;

        /// <summary>
        ///     Remote IPEndPoint
        /// </summary>
        public readonly IPEndPoint RemoteEndPoint;

        /// <summary>
        ///     Connection
        /// </summary>
        public readonly WebSocketConnection Connection;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="eventType">Type</param>
        /// <param name="id">Index</param>
        /// <param name="connection">Connection</param>
        public WebSocketServerEvent(NetworkEventType eventType, uint id, WebSocketConnection connection)
        {
            EventType = eventType;
            Id = id;
            Data = default;
            RemoteEndPoint = null;
            Connection = connection;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="eventType">Type</param>
        /// <param name="id">Index</param>
        /// <param name="ipEndPoint">Remote IPEndPoint</param>
        /// <param name="connection">Connection</param>
        public WebSocketServerEvent(NetworkEventType eventType, uint id, IPEndPoint ipEndPoint, WebSocketConnection connection)
        {
            EventType = eventType;
            Id = id;
            Data = default;
            RemoteEndPoint = ipEndPoint;
            Connection = connection;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="eventType">Type</param>
        /// <param name="id">Index</param>
        /// <param name="data">Data</param>
        public WebSocketServerEvent(NetworkEventType eventType, uint id, NetworkPacket data)
        {
            EventType = eventType;
            Id = id;
            Data = data;
            RemoteEndPoint = null;
            Connection = null;
        }
    }
}