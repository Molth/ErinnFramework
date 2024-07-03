//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Message response handler
    /// </summary>
    public abstract class MessageEndPointResultHandlerBase : IMessageEndPointResultHandler
    {
        /// <summary>
        ///     MessageEndPoint
        /// </summary>
        protected MessageEndPoint MessageEndPoint;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageEndPoint">MessageEndPoint</param>
        protected MessageEndPointResultHandlerBase(MessageEndPoint messageEndPoint) => MessageEndPoint = messageEndPoint;

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="payload">Payload</param>
        public abstract void SetResult(uint serialNumber, in ArraySegment<byte> payload);

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="payload">Payload</param>
        public abstract void SetResult(uint serialNumber, int cookie, in ArraySegment<byte> payload);
    }
}