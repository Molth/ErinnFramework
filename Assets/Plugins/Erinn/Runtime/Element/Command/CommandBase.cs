//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Command base class
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        ///     Implement
        /// </summary>
        void ICommand.OnExecute(object[] args) => OnExecute(args);

        /// <summary>
        ///     Revoke
        /// </summary>
        void ICommand.OnCancel(object[] args) => Cancel(args);

        /// <summary>
        ///     Implement
        /// </summary>
        protected abstract void OnExecute(object[] args);

        /// <summary>
        ///     Revoke
        /// </summary>
        protected abstract void Cancel(object[] args);
    }
}