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
    internal sealed class NetworkClientMessageProcessor<T> : NetworkClientMessageProcessorBase where T : struct, INetworkMessage, IMemoryPackable<T>
    {
        /// <summary>
        ///     Handler
        /// </summary>
        private Action<T> _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="handler">Handler</param>
        public NetworkClientMessageProcessor(Action<T> handler) => _handler = handler;

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="bytes">Payload</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Invoke(in ArraySegment<byte> bytes)
        {
            var obj = new T();
            try
            {
                MemoryPackSerializer.Deserialize(bytes, ref obj);
            }
            catch
            {
                return;
            }

            try
            {
                _handler.Invoke(obj);
            }
            catch (Exception e)
            {
                var hash32 = NetworkHash.GetId32<T>();
                Log.Error($"Command [{typeof(T).FullName}] [{hash32}]: [{e}]");
            }
        }

        /// <summary>
        ///     Release
        /// </summary>
        public override void Dispose() => _handler = null;

        /// <summary>
        ///     Replace handler
        /// </summary>
        /// <param name="hash32">Hash32</param>
        /// <param name="handler">Handler</param>
        public void Replace(uint hash32, Action<T> handler)
        {
            if (_handler != null && _handler == handler)
                return;
            _handler = handler;
            Log.Info($"Replace Command: [{typeof(T).FullName}] [{hash32}]");
        }
    }
}