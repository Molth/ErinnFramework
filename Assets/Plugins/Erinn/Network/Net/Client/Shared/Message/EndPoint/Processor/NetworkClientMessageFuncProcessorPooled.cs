//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkClientMessageFuncProcessorPooled<TRequest, TResponse> : NetworkClientMessageFuncProcessorBase where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
    {
        /// <summary>
        ///     Server
        /// </summary>
        private NetworkClient _client;

        /// <summary>
        ///     Handler
        /// </summary>
        private Func<TRequest, TResponse> _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkClient"></param>
        /// <param name="handler"></param>
        public NetworkClientMessageFuncProcessorPooled(NetworkClient networkClient, Func<TRequest, TResponse> handler)
        {
            _client = networkClient;
            _handler = handler;
        }

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="bytes">Payload</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Invoke(uint serialNumber, in ArraySegment<byte> bytes)
        {
            var obj = NetworkMessagePool<TRequest>.Rent();
            try
            {
                MemoryPackSerializer.Deserialize(bytes, ref obj);
            }
            catch
            {
                NetworkMessagePool<TRequest>.Return(in obj);
                return;
            }

            TResponse result;
            try
            {
                result = _handler.Invoke(obj);
            }
            catch
            {
                return;
            }

            if (!NetworkSerializer.Create(in result, out var networkPacket))
                return;
            var networkMessagePacket = new NetworkMessageResponse(serialNumber, networkPacket);
            _client.Send(networkMessagePacket);
        }

        /// <summary>
        ///     Release
        /// </summary>
        public override void Dispose()
        {
            _client = null;
            _handler = null;
        }

        /// <summary>
        ///     Replace handler
        /// </summary>
        /// <param name="hash32">Hash32</param>
        /// <param name="handler">Handler</param>
        public void Replace(uint hash32, Func<TRequest, TResponse> handler)
        {
            if (_handler != null && _handler == handler)
                return;
            _handler = handler;
            Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
        }
    }
}