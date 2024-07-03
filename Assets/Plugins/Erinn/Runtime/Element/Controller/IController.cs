//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Controller interface
    /// </summary>
    public interface IController : IReference
    {
        /// <summary>
        ///     Controller initialization
        /// </summary>
        internal void Init(IActor owner, object[] args);
    }
}