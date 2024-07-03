//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor interface
    /// </summary>
    internal interface INetworkServerFuncProcessor<in TRequest, out TResponse> where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
    {
        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">Request</param>
        TResponse Invoke(uint id, TRequest obj);
    }
}