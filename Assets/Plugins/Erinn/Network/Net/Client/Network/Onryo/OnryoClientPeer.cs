//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8604
#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed partial class OnryoClientPeer : NetworkClientPeer
    {
        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        public OnryoClientPeer(NetworkClientMessageChannel messageChannel) : base(messageChannel)
        {
        }

        /// <summary>
        ///     Protocol type
        /// </summary>
        public override NetworkProtocolType ProtocolType => NetworkProtocolType.Tcp;

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public override bool Start(string ipAddress, ushort port)
        {
            if (!StartListening())
                return false;
            Connect(ipAddress, port);
            OnStart(ipAddress, port);
            return true;
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override bool Shutdown()
        {
            if (!StopListening())
                return false;
            _disposed.Set(true);
            _tcpClient?.Close();
            _tcpClient = null;
            while (_onryoEvents.TryDequeue(out var netEvent))
            {
                if (netEvent.EventType == NetworkEventType.Data)
                    NetworkPacketPool.Return(in netEvent.Data);
            }

            while (_outgoing.TryDequeue(out var segment))
                NetworkBytesPool.Return(segment.Array);
            return true;
        }

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public override uint GetRoundTripTime() => 0U;

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public override void Send<T>(in T obj)
        {
            if (!Connected || !NetworkSerializer.Serialize(in obj, out var payload))
                return;
            var count = payload.Count;
            var buffer = NetworkBytesPool.Rent(count);
            NetworkSerializer.BlockCopy(in payload, 0, buffer, 0, count);
            _outgoing.Enqueue(new ArraySegment<byte>(buffer, 0, count));
        }

        /// <summary>
        ///     Polling
        /// </summary>
        protected override void Poll()
        {
            while (_onryoEvents.TryDequeue(out var netEvent))
            {
                switch (netEvent.EventType)
                {
                    case NetworkEventType.Data:
                        InvokeHandler(in netEvent.Data);
                        NetworkPacketPool.Return(in netEvent.Data);
                        continue;
                    case NetworkEventType.Connect:
                        OnConnected();
                        continue;
                    case NetworkEventType.Disconnect:
                        OnDisconnected();
                        continue;
                    default:
                        continue;
                }
            }
        }
    }
}