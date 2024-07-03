//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Animation state base class interface
    /// </summary>
    public interface IStateMachineBehaviourInitialize
    {
        /// <summary>
        ///     Already initialized
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        ///     Initialization
        /// </summary>
        void Initialize(IActor owner);

        /// <summary>
        ///     Clean up
        /// </summary>
        void Dispose();
    }
}