//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Receive event
    /// </summary>
    internal readonly struct WebSocketClientEvent
    {
        /// <summary>
        ///     Type
        /// </summary>
        public readonly NetworkEventType EventType;

        /// <summary>
        ///     Data
        /// </summary>
        public readonly NetworkPacket Data;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="eventType">Type</param>
        public WebSocketClientEvent(NetworkEventType eventType)
        {
            EventType = eventType;
            Data = default;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="eventType">Type</param>
        /// <param name="data">Data</param>
        public WebSocketClientEvent(NetworkEventType eventType, NetworkPacket data)
        {
            EventType = eventType;
            Data = data;
        }
    }
}