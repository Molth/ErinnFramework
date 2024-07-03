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
    ///     Message response handler interface
    /// </summary>
    public interface IMessageEndPointResultHandler
    {
        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="payload">Payload</param>
        void SetResult(uint serialNumber, in ArraySegment<byte> payload);

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="payload">Payload</param>
        void SetResult(uint serialNumber, int cookie, in ArraySegment<byte> payload);
    }
}