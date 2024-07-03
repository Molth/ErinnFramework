//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode.Components;

namespace Erinn
{
    /// <summary>
    ///     ClientAnimator
    /// </summary>
    public sealed class NetworkClientAnimator : NetworkAnimator
    {
        /// <summary>
        ///     Cancel verification
        /// </summary>
        protected override bool OnIsServerAuthoritative() => false;
    }
}