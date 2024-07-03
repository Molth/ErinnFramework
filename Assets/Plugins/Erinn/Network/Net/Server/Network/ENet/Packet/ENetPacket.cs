//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using ENet;

namespace Erinn
{
    /// <summary>
    ///     ENet packet
    /// </summary>
    internal struct ENetPacket
    {
        /// <summary>
        ///     Peer
        /// </summary>
        private Peer _peer;

        /// <summary>
        ///     Packet
        /// </summary>
        private Packet _packet;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="peer">Peer</param>
        /// <param name="packet">Packet</param>
        public ENetPacket(Peer peer, Packet packet)
        {
            _peer = peer;
            _packet = packet;
        }

        /// <summary>
        ///     Send
        /// </summary>
        public void Send()
        {
            if (_peer.Send(0, ref _packet))
                return;
            _packet.Dispose();
        }

        /// <summary>
        ///     Dispose
        /// </summary>
        public void Dispose() => _packet.Dispose();
    }
}