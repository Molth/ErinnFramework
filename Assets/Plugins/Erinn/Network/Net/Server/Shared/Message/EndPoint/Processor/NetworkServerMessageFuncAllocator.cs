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
    internal sealed class NetworkServerMessageFuncAllocator<TRequest, TResponse> : NetworkServerMessageFuncProcessorBase where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
    {
        /// <summary>
        ///     Server
        /// </summary>
        private NetworkServer _server;

        /// <summary>
        ///     Handler
        /// </summary>
        private Func<uint, TRequest, TResponse> _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkServer"></param>
        /// <param name="handler"></param>
        public NetworkServerMessageFuncAllocator(NetworkServer networkServer, Func<uint, TRequest, TResponse> handler)
        {
            _server = networkServer;
            _handler = handler;
        }

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="bytes">Payload</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Invoke(uint id, uint serialNumber, in ArraySegment<byte> bytes)
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
                result = _handler.Invoke(id, obj);
            }
            catch
            {
                return;
            }
            finally
            {
                NetworkMessagePool<TRequest>.Return(in obj);
            }

            if (!NetworkSerializer.Create(in result, out var networkPacket))
                return;
            var networkMessagePacket = new NetworkMessageResponse(serialNumber, networkPacket);
            _server.Send(id, networkMessagePacket);
        }

        /// <summary>
        ///     Release
        /// </summary>
        public override void Dispose()
        {
            _server = null;
            _handler = null;
        }

        /// <summary>
        ///     Replace handler
        /// </summary>
        /// <param name="hash32">Hash32</param>
        /// <param name="handler">Handler</param>
        public void Replace(uint hash32, Func<uint, TRequest, TResponse> handler)
        {
            if (_handler != null && _handler == handler)
                return;
            _handler = handler;
            Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
        }
    }
}