//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Network Discovery Component Base Class
    /// </summary>
    /// <typeparam name="TBroadCast">Broadcast</typeparam>
    /// <typeparam name="TResponse">Response</typeparam>
    [DisallowMultipleComponent]
    public abstract class NetworkDiscoveryBase<TBroadCast, TResponse> : MonoBehaviour where TBroadCast : struct, INetworkSerializable where TResponse : struct, INetworkSerializable
    {
        /// <summary>
        ///     Vision
        /// </summary>
        public ulong UniqueIdentifier = MathV.HashU64("Erinn");

        /// <summary>
        ///     UdpClient
        /// </summary>
        private UdpClient _udpClient;

        /// <summary>
        ///     Running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     At the beginning, call
        /// </summary>
        protected virtual void Start()
        {
            NetcodeEvents.OnClientStarted += StopDiscovery;
            NetcodeEvents.OnServerStarted += StopDiscovery;
            NetcodeEvents.OnServerStopped += StopDiscovery;
        }

        /// <summary>
        ///     Call upon destruction
        /// </summary>
        protected virtual void OnDestroy()
        {
            StopDiscovery();
            NetcodeEvents.OnClientStarted -= StopDiscovery;
            NetcodeEvents.OnServerStarted -= StopDiscovery;
            NetcodeEvents.OnServerStopped -= StopDiscovery;
        }

        /// <summary>
        ///     Call on exit
        /// </summary>
        protected virtual void OnApplicationQuit() => StopDiscovery();

        /// <summary>
        ///     Client begins network discovery
        /// </summary>
        public bool StartDiscovery()
        {
            if (NetcodeSystem.IsListening)
            {
                Log.Error("Cannot send broadcast during connection");
                return false;
            }

            if (IsRunning)
            {
                Log.Error("Network discovery has been initiated");
                return false;
            }

            _udpClient = new UdpClient(0) { EnableBroadcast = true, MulticastLoopback = false };
            _ = ListenAsync(ClientReceiveResponseAsync);
            IsRunning = true;
            return true;
        }

        /// <summary>
        ///     Server registration network discovery
        /// </summary>
        public bool AdviseServer()
        {
            if (!NetcodeSystem.IsServer)
            {
                Log.Error("Only the server can register for network discovery");
                return false;
            }

            if (IsRunning)
            {
                Log.Error("Network discovery has been initiated");
                return false;
            }

            _udpClient = new UdpClient(NetcodeSystem.Port + 1) { EnableBroadcast = true, MulticastLoopback = false };
            _ = ListenAsync(ServerReceiveBroadcastAsync);
            IsRunning = true;
            return true;
        }

        /// <summary>
        ///     Stop broadcasting
        /// </summary>
        private void StopDiscovery(bool _) => StopDiscovery();

        /// <summary>
        ///     Stop broadcasting
        /// </summary>
        public void StopDiscovery()
        {
            IsRunning = false;
            if (_udpClient != null)
            {
                try
                {
                    _udpClient.Close();
                }
                catch
                {
                    Log.Info("An error occurred while stopping the broadcast");
                }
                finally
                {
                    _udpClient = null;
                }
            }
        }

        /// <summary>
        ///     Client Broadcast
        /// </summary>
        public void ClientBroadcast()
        {
            if (NetcodeSystem.IsListening)
            {
                Log.Error("Cannot send broadcast during connection");
                return;
            }

            var broadCast = GetClientBroadcast();
            var endPoint = new IPEndPoint(IPAddress.Broadcast, NetcodeSystem.Port + 1);
            using var writer = new FastBufferWriter(1024, Allocator.Temp);
            WriteHeader(writer, MessageType.BroadCast);
            writer.WriteNetworkSerializable(broadCast);
            var data = writer.ToArray();
            try
            {
                _udpClient.SendAsync(data, data.Length, endPoint);
            }
            catch
            {
                Log.Info("An error occurred during broadcasting");
            }
        }

        /// <summary>
        ///     Obtain broadcast data
        /// </summary>
        /// <returns>Obtained broadcast data</returns>
        protected abstract TBroadCast GetClientBroadcast();

        /// <summary>
        ///     Client receives server response
        /// </summary>
        /// <param name="sender">Response sender</param>
        /// <param name="response">Response data</param>
        protected abstract void ClientReceiveResponse(IPEndPoint sender, TResponse response);

        /// <summary>
        ///     Client asynchronously receives server response
        /// </summary>
        private async UniTask ClientReceiveResponseAsync()
        {
            var udpReceiveResult = await _udpClient.ReceiveAsync();
            var segment = new ArraySegment<byte>(udpReceiveResult.Buffer, 0, udpReceiveResult.Buffer.Length);
            using var reader = new FastBufferReader(segment, Allocator.Persistent);
            try
            {
                if (!ReadAndCheckHeader(reader, MessageType.Response))
                    return;
                reader.ReadNetworkSerializable(out TResponse receivedResponse);
                ClientReceiveResponse(udpReceiveResult.RemoteEndPoint, receivedResponse);
            }
            catch (Exception e)
            {
                Log.Info(e.Message);
            }
        }

        /// <summary>
        ///     Get server response data
        /// </summary>
        /// <returns>Obtained server response data</returns>
        protected abstract TResponse GetServerResponse();

        /// <summary>
        ///     Server processing client response
        /// </summary>
        /// <param name="sender">Broadcast sender</param>
        /// <param name="broadCast">Broadcast data</param>
        /// <param name="response">Return response data</param>
        /// <returns>Do you want to return response data</returns>
        protected abstract bool ServerProcessBroadcast(IPEndPoint sender, TBroadCast broadCast, out TResponse response);

        /// <summary>
        ///     Server asynchronously receives client broadcasts
        /// </summary>
        private async UniTask ServerReceiveBroadcastAsync()
        {
            var udpReceiveResult = await _udpClient.ReceiveAsync();
            var segment = new ArraySegment<byte>(udpReceiveResult.Buffer, 0, udpReceiveResult.Buffer.Length);
            using var reader = new FastBufferReader(segment, Allocator.Persistent);
            try
            {
                if (!ReadAndCheckHeader(reader, MessageType.BroadCast))
                    return;
                reader.ReadNetworkSerializable(out TBroadCast receivedBroadcast);
                if (!ServerProcessBroadcast(udpReceiveResult.RemoteEndPoint, receivedBroadcast, out var response))
                    return;
                using var writer = new FastBufferWriter(1024, Allocator.Persistent);
                WriteHeader(writer, MessageType.Response);
                writer.WriteNetworkSerializable(response);
                var data = writer.ToArray();
                await _udpClient.SendAsync(data, data.Length, udpReceiveResult.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Log.Info(e.Message);
            }
        }

        /// <summary>
        ///     Asynchronous listening
        /// </summary>
        /// <param name="onReceiveTask">Receiving tasks</param>
        private async UniTask ListenAsync(Func<UniTask> onReceiveTask)
        {
            while (true)
            {
                try
                {
                    if (_udpClient != null)
                        await onReceiveTask();
                    else
                        break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Log.Info(e.Message);
                }
            }
        }

        /// <summary>
        ///     Write a title
        /// </summary>
        /// <param name="writer">FastBufferWriter</param>
        /// <param name="messageType">Information type</param>
        private void WriteHeader(FastBufferWriter writer, MessageType messageType)
        {
            writer.WriteValueSafe(UniqueIdentifier);
            writer.WriteByteSafe((byte)messageType);
        }

        /// <summary>
        ///     Read and check the title
        /// </summary>
        /// <param name="reader">FastBufferReader</param>
        /// <param name="expectedType">Information type</param>
        /// <returns>Successfully read</returns>
        private bool ReadAndCheckHeader(FastBufferReader reader, MessageType expectedType)
        {
            reader.ReadValueSafe(out ulong receivedApplicationId);
            if (receivedApplicationId != UniqueIdentifier)
                return false;
            reader.ReadByteSafe(out var messageType);
            return messageType == (byte)expectedType;
        }

        /// <summary>
        ///     Information type
        /// </summary>
        private enum MessageType : byte
        {
            BroadCast = 0,
            Response = 1
        }
    }
}