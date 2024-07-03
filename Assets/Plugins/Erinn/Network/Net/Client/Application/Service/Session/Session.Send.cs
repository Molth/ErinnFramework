//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;

#pragma warning disable CS8603

namespace Erinn
{
    /// <summary>
    ///     Service Session
    /// </summary>
    public abstract partial class Session
    {
        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        protected void Send<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            if (!Connected)
                return;
            _networkClient.Send(in obj);
        }

        /// <summary>
        ///     Send
        /// </summary>
        private void SendInternal(uint command, in NetworkWriter writer)
        {
            var networkRpcPacket = new NetworkRpcPacket(command, writer.ToArraySegment());
            _networkClient.Send(networkRpcPacket);
            NetworkWriterPool.Return(writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send(uint command)
        {
            if (!Connected)
                return;
            var networkRpcPacket = new NetworkRpcPacket(command);
            _networkClient.Send(networkRpcPacket);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0>(uint command, in T0 arg0)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1>(uint command, in T0 arg0, in T1 arg1)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2>(uint command, in T0 arg0, in T1 arg1, in T2 arg2)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12, in T13 arg13)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12, in T13 arg13, in T14 arg14)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13, in arg14);
            SendInternal(command, writer);
        }

        /// <summary>
        ///     Send
        /// </summary>
        protected void Send<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(uint command, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9, in T10 arg10, in T11 arg11, in T12 arg12, in T13 arg13, in T14 arg14, in T15 arg15)
        {
            if (!Connected)
                return;
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13, in arg14, in arg15);
            SendInternal(command, writer);
        }
    }
}