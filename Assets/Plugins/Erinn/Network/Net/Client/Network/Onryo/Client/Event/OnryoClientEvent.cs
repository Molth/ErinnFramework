//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Receive event
    /// </summary>
    internal readonly struct OnryoClientEvent
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
        public OnryoClientEvent(NetworkEventType eventType)
        {
            EventType = eventType;
            Data = default;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="eventType">Type</param>
        /// <param name="data">Data</param>
        public OnryoClientEvent(NetworkEventType eventType, NetworkPacket data)
        {
            EventType = eventType;
            Data = data;
        }
    }
}