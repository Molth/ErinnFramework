//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode.Components;

namespace Erinn
{
    /// <summary>
    ///     ClientTransform
    /// </summary>
    public sealed class NetworkClientTransform : NetworkTransform
    {
        /// <summary>
        ///     Cancel verification
        /// </summary>
        protected override bool OnIsServerAuthoritative() => false;
    }
}