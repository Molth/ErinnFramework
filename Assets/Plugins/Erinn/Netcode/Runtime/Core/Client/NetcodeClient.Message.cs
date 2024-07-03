//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public static partial class NetcodeClient
    {
        /// <summary>
        ///     Message Manager
        /// </summary>
        private static CustomMessagingManager MessagingManager => NetManager.CustomMessagingManager;

        /// <summary>
        ///     Registration Event
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="callback">Event</param>
        public static void RegisterNamedMessageHandler(string name, CustomMessagingManager.HandleNamedMessageDelegate callback)
        {
            if (IsClient)
                MessagingManager.RegisterNamedMessageHandler(name, callback);
        }

        /// <summary>
        ///     Remove named event
        /// </summary>
        /// <param name="name">Name</param>
        public static void UnregisterNamedMessageHandler(string name)
        {
            if (IsClient)
                MessagingManager.UnregisterNamedMessageHandler(name);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsg(string messageName, FastBufferWriter messageStream)
        {
            MessagingManager.SendNamedMessage(messageName, 0, messageStream);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendNamedMsg(string messageName, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            MessagingManager.SendNamedMessage(messageName, 0, messageStream, networkDelivery);
            messageStream.Dispose();
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        public static void SendNamedPipe(string messageName, FastBufferWriter messageStream)
        {
            if (IsClient)
                SendNamedMsg(messageName, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedPipe(string messageName, FastBufferWriter messageStream, NetworkDelivery networkDelivery)
        {
            if (IsClient)
                SendNamedMsg(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, T message) where T : struct
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsg(messageName, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, T message, NetworkDelivery networkDelivery) where T : struct
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message);
            SendNamedMsg(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, T message, Allocator allocator) where T : struct
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsg(messageName, messageStream);
        }

        /// <summary>
        ///     Send named event information
        /// </summary>
        /// <param name="messageName">Information Name</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendNamedMessage<T>(string messageName, T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetMessageWriter(message, allocator);
            SendNamedMsg(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        public static void SendNamedBytes(string messageName, byte[] bytes)
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsg(messageName, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public static void SendNamedBytes(string messageName, byte[] bytes, NetworkDelivery networkDelivery)
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes);
            SendNamedMsg(messageName, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytes(string messageName, byte[] bytes, Allocator allocator)
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsg(messageName, messageStream);
        }

        /// <summary>
        ///     Send named eventsBytes
        /// </summary>
        /// <param name="messageName">BytesName</param>
        /// <param name="bytes">Bytes</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        public static void SendNamedBytes(string messageName, byte[] bytes, NetworkDelivery networkDelivery, Allocator allocator)
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetBytesWriter(bytes, allocator);
            SendNamedMsg(messageName, messageStream, networkDelivery);
        }
    }
}