//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Command Manager
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        ///     Directly execute command
        ///     No cache
        /// </summary>
        /// <param name="args">Execution parameters</param>
        /// <typeparam name="T">Command type</typeparam>
        void ExecuteDirectly<T>(params object[] args) where T : ICommand;

        /// <summary>
        ///     Directly revoke command
        ///     No cache
        /// </summary>
        /// <typeparam name="T">Command type</typeparam>
        /// <returns>Command present</returns>
        bool CancelDirectly<T>(params object[] args) where T : ICommand;

        /// <summary>
        ///     Execute command
        /// </summary>
        /// <param name="args">Execution parameters</param>
        /// <typeparam name="T">Command type</typeparam>
        void Execute<T>(params object[] args) where T : ICommand;

        /// <summary>
        ///     Revoke command
        /// </summary>
        /// <typeparam name="T">Command type</typeparam>
        /// <returns>No command or successful revocation</returns>
        bool Cancel<T>() where T : ICommand;

        /// <summary>
        ///     Redo Command
        /// </summary>
        /// <typeparam name="T">Command type</typeparam>
        /// <returns>No command or successful redo</returns>
        bool Redo<T>() where T : ICommand;

        /// <summary>
        ///     Completely revoke command
        /// </summary>
        /// <typeparam name="T">Command type</typeparam>
        void CancelAll<T>() where T : ICommand;

        /// <summary>
        ///     Complete redo command
        /// </summary>
        /// <typeparam name="T">Command type</typeparam>
        void RedoAll<T>() where T : ICommand;

        /// <summary>
        ///     Destruction command
        /// </summary>
        /// <typeparam name="T">Command type</typeparam>
        void Dispose<T>() where T : ICommand;
    }
}