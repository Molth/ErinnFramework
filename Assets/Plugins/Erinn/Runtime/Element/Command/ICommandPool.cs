//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Command Pool
    /// </summary>
    internal interface ICommandPool : IReference
    {
        /// <summary>
        ///     Directly execute
        /// </summary>
        void ExecuteDirectly(object[] args);

        /// <summary>
        ///     Direct revocation
        /// </summary>
        void CancelDirectly(object[] args);

        /// <summary>
        ///     Implement
        /// </summary>
        void Execute(object[] args);

        /// <summary>
        ///     Revoke
        /// </summary>
        bool Cancel();

        /// <summary>
        ///     Redo
        /// </summary>
        bool Redo();

        /// <summary>
        ///     Revoke All
        /// </summary>
        void CancelAll();

        /// <summary>
        ///     Redo All
        /// </summary>
        void RedoAll();
    }
}