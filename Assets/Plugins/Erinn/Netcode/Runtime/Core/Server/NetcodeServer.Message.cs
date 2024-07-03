//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public static partial class NetcodeServer
    {
        /// <summary>
        ///     Message Manager
        /// </summary>
        private static CustomMessagingManager MessagingManager => NetManager.CustomMessagingManager;

        /// <summary>
        ///     Registering named event
        /// </summary>
        /// <param name="messageName">Name</param>
        /// <param name="callback">Event</param>
        public static void RegisterNamedMessageHandler(string messageName, CustomMessagingManager.HandleNamedMessageDelegate callback)
        {
            if (IsServer)
                MessagingManager.RegisterNamedMessageHandler(messageName, callback);
        }

        /// <summary>
        ///     Remove named event
        /// </summary>
        /// <param name="messageName">Name</param>
        public static void UnregisterNamedMessageHandler(string messageName)
        {
            if (IsServer)
                MessagingManager.UnregisterNamedMessageHandler(messageName);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsg(string messageName, ulong clientId, FastBufferWriter messageStream)
        {
            MessagingManager.SendNamedMessage(messageName, clientId, messageStream);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsg(string messageName, ulong clientId, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            MessagingManager.SendNamedMessage(messageName, clientId, messageStream, networkDelivery);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsg(string messageName, IReadOnlyList<ulong> clientIds, FastBufferWriter messageStream)
        {
            MessagingManager.SendNamedMessage(messageName, clientIds, messageStream);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsg(string messageName, IReadOnlyList<ulong> clientIds, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            MessagingManager.SendNamedMessage(messageName, clientIds, messageStream, networkDelivery);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsgToAll(string messageName, FastBufferWriter messageStream)
        {
            MessagingManager.SendNamedMessageToAll(messageName, messageStream);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsgToAll(string messageName, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            MessagingManager.SendNamedMessageToAll(messageName, messageStream, networkDelivery);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        public static void SendNamedPipe(string messageName, ulong clientId, FastBufferWriter messageStream)
        {
            if (IsServer)
                SendNamedMsg(messageName, clientId, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedPipe(string messageName, ulong clientId, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            if (IsServer)
                SendNamedMsg(messageName, clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        public static void SendNamedPipe(string messageName, IReadOnlyList<ulong> clientIds, FastBufferWriter messageStream)
        {
            if (IsServer)
                SendNamedMsg(messageName, clientIds, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedPipe(string messageName, IReadOnlyList<ulong> clientIds, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            if (IsServer)
                SendNamedMsg(messageName, clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        public static void SendNamedPipeToAll(string messageName, FastBufferWriter messageStream)
        {
            if (IsServer)
                SendNamedMsgToAll(messageName, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedPipeToAll(string messageName, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            if (IsServer)
                SendNamedMsgToAll(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, ulong clientId, T message) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsg(messageName, clientId, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, ulong clientId, T message, NetworkDelivery networkDelivery) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsg(messageName, clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, ulong clientId, T message, Allocator allocator) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsg(messageName, clientId, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, ulong clientId, T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsg(messageName, clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, IReadOnlyList<ulong> clientIds, T message) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsg(messageName, clientIds, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, IReadOnlyList<ulong> clientIds, T message, NetworkDelivery networkDelivery) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsg(messageName, clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, IReadOnlyList<ulong> clientIds, T message, Allocator allocator) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsg(messageName, clientIds, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, IReadOnlyList<ulong> clientIds, T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsg(messageName, clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessageToAll<T>(string messageName, T message) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsgToAll(messageName, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessageToAll<T>(string messageName, T message, NetworkDelivery networkDelivery) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsgToAll(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessageToAll<T>(string messageName, T message, Allocator allocator) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsgToAll(messageName, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessageToAll<T>(string messageName, T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsgToAll(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="bytes">Bytes</param>
        public static void SendNamedBytes(string messageName, ulong clientId, byte[] bytes)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsg(messageName, clientId, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedBytes(string messageName, ulong clientId, byte[] bytes, NetworkDelivery networkDelivery)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsg(messageName, clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytes(string messageName, ulong clientId, byte[] bytes, Allocator allocator)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsg(messageName, clientId, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientId">TargetId</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytes(string messageName, ulong clientId, byte[] bytes, NetworkDelivery networkDelivery, Allocator allocator)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsg(messageName, clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="bytes">Bytes</param>
        public static void SendNamedBytes(string messageName, IReadOnlyList<ulong> clientIds, byte[] bytes)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsg(messageName, clientIds, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedBytes(string messageName, IReadOnlyList<ulong> clientIds, byte[] bytes, NetworkDelivery networkDelivery)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsg(messageName, clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytes(string messageName, IReadOnlyList<ulong> clientIds, byte[] bytes, Allocator allocator)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsg(messageName, clientIds, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytes(string messageName, IReadOnlyList<ulong> clientIds, byte[] bytes, NetworkDelivery networkDelivery, Allocator allocator)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsg(messageName, clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        public static void SendNamedBytesToAll(string messageName, byte[] bytes)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsgToAll(messageName, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedBytesToAll(string messageName, byte[] bytes, NetworkDelivery networkDelivery)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsgToAll(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytesToAll(string messageName, byte[] bytes, Allocator allocator)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsgToAll(messageName, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytesToAll(string messageName, byte[] bytes, NetworkDelivery networkDelivery, Allocator allocator)
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsgToAll(messageName, messageStream, networkDelivery);
        }
    }
}