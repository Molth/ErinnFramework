//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System.Threading.Tasks;
#endif

#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Service Session
    /// </summary>
    public abstract partial class Service
    {
        /// <summary>
        ///     Send packet async
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        protected async Task<MessageResult<TResponse>> SendAsync<TRequest, TResponse>(TRequest request, int timeout = 5000) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => !Connected ? new MessageResult<TResponse>(MessageState.Nil) : await _endPoint.SendAsync<TRequest, TResponse>(Id, request, timeout);

        /// <summary>
        ///     Set message result
        /// </summary>
        /// <param name="response">Async response</param>
        /// <typeparam name="TResponse">Type</typeparam>
        /// <returns>MessageResult</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MessageResult<TResponse> SetResult<TResponse>(MessageResult<NetworkRouteResponse> response)
        {
            var state = response.State;
            if (state != MessageState.Success)
                return new MessageResult<TResponse>(state);
            TResponse? result;
            try
            {
                result = MemoryPackSerializer.Deserialize<TResponse>(response.Result.Payload);
            }
            catch
            {
                return new MessageResult<TResponse>(MessageState.Nil);
            }

            return new MessageResult<TResponse>(MessageState.Success, result);
        }

        /// <summary>
        ///     Send async
        /// </summary>
        private async Task<MessageResult<TResponse>> SendAsyncInternal<TResponse>(uint command, NetworkWriter writer, int timeout)
        {
            var payload = writer.ToArraySegment();
            var request = new NetworkRouteRequest(command, payload);
            var task = _endPoint.SendAsync<NetworkRouteRequest, NetworkRouteResponse>(Id, request, timeout);
            NetworkWriterPool.Return(writer);
            var response = await task;
            return SetResult<TResponse>(response);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<TResponse>(uint command, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var request = new NetworkRouteRequest(command);
            var response = await _endPoint.SendAsync<NetworkRouteRequest, NetworkRouteResponse>(Id, request, timeout);
            return SetResult<TResponse>(response);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, TResponse>(uint command, T0 arg0, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, TResponse>(uint command, T0 arg0, T1 arg1, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13, in arg14);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }

        /// <summary>
        ///     Send packet async
        /// </summary>
        protected async Task<MessageResult<TResponse>> SendAsync<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResponse>(uint command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, int timeout = 5000)
        {
            if (!Connected)
                return new MessageResult<TResponse>(MessageState.Nil);
            var writer = NetworkWriterPool.Write(in arg0, in arg1, in arg2, in arg3, in arg4, in arg5, in arg6, in arg7, in arg8, in arg9, in arg10, in arg11, in arg12, in arg13, in arg14, in arg15);
            return await SendAsyncInternal<TResponse>(command, writer, timeout);
        }
    }
}