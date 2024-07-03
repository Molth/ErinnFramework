//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Implement
        /// </summary>
        void OnExecute(object[] args);

        /// <summary>
        ///     Revoke
        /// </summary>
        void OnCancel(object[] args);
    }
}