//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkClientServiceHandler : INetworkClientServiceHandler
    {
        /// <summary>
        ///     Handler
        /// </summary>
        private readonly NetworkReaderWrapHandlerBase _handler;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="handler">Handler</param>
        public NetworkClientServiceHandler(NetworkReaderWrapHandlerBase handler) => _handler = handler;

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="reader">NetworkReader</param>
        public void Invoke(NetworkBuffer reader) => _handler.Invoke(reader);
    }
}