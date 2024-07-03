//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif
using MemoryPack;

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Message response handler
    /// </summary>
    public sealed class MessageEndPointResultHandler<T> : MessageEndPointResultHandlerBase
    {
        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageEndPoint">MessageEndPoint</param>
        public MessageEndPointResultHandler(MessageEndPoint messageEndPoint) : base(messageEndPoint)
        {
        }

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="payload">Payload</param>
        public override void SetResult(uint serialNumber, in ArraySegment<byte> payload)
        {
            T result = default;
            try
            {
                MemoryPackSerializer.Deserialize(payload, ref result);
            }
            catch
            {
                MessageEndPoint.SetResult(serialNumber, null);
                return;
            }

            MessageEndPoint.SetResult(serialNumber, result);
        }

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="payload">Payload</param>
        public override void SetResult(uint serialNumber, int cookie, in ArraySegment<byte> payload)
        {
            T result = default;
            try
            {
                MemoryPackSerializer.Deserialize(payload, ref result);
            }
            catch
            {
                MessageEndPoint.SetResult(serialNumber, cookie, null);
                return;
            }

            MessageEndPoint.SetResult(serialNumber, cookie, result);
        }
    }
}