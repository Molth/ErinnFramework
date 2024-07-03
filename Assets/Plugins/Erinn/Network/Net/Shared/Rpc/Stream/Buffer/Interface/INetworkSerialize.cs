//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network serialize interface
    /// </summary>
    public interface INetworkSerialize
    {
        /// <summary>
        ///     Write
        /// </summary>
        void Serialize(NetworkWriter writer);

        /// <summary>
        ///     Read
        /// </summary>
        void Deserialize(NetworkReader reader);
    }
}