//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Erinn
{
    /// <summary>
    ///     Command Pool
    /// </summary>
    internal sealed class CommandPool : ICommandPool
    {
        /// <summary>
        ///     Command
        /// </summary>
        private ICommand _cmd;

        /// <summary>
        ///     Execution cache area
        /// </summary>
        [ShowInInspector] [FoldoutGroup("Attribute")] [LabelText("Execution cache area")]
        private readonly Stack<object[]> _doBuffer = new();

        /// <summary>
        ///     Revoke cache area
        /// </summary>
        [ShowInInspector] [FoldoutGroup("Attribute")] [LabelText("Revoke cache area")]
        private readonly Stack<object[]> _cancelBuffer = new();

        /// <summary>
        ///     Directly execute commands
        /// </summary>
        public void ExecuteDirectly(object[] args) => _cmd.OnExecute(args);

        /// <summary>
        ///     Directly revoke commands
        /// </summary>
        public void CancelDirectly(object[] args) => _cmd.OnCancel(args);

        /// <summary>
        ///     Execute commands
        /// </summary>
        /// <param name="args">Command parameters</param>
        public void Execute(object[] args)
        {
            if (_cancelBuffer.Count > 0)
                _cancelBuffer.Clear();
            _cmd.OnExecute(args);
            _doBuffer.Push(args);
        }

        /// <summary>
        ///     Revoke the previous command
        /// </summary>
        public bool Cancel()
        {
            if (_doBuffer.Count > 0)
            {
                var args = _doBuffer.Pop();
                _cmd.OnCancel(args);
                _cancelBuffer.Push(args);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Redo the previous command
        /// </summary>
        public bool Redo()
        {
            if (_cancelBuffer.Count > 0)
            {
                var args = _cancelBuffer.Pop();
                _cmd.OnExecute(args);
                _doBuffer.Push(args);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Revoke All
        /// </summary>
        public void CancelAll()
        {
            while (_doBuffer.Count > 0)
            {
                var args = _doBuffer.Pop();
                _cmd.OnCancel(args);
                _cancelBuffer.Push(args);
            }
        }

        /// <summary>
        ///     Redo All
        /// </summary>
        public void RedoAll()
        {
            while (_cancelBuffer.Count > 0)
            {
                var args = _cancelBuffer.Pop();
                _cmd.OnExecute(args);
                _doBuffer.Push(args);
            }
        }

        /// <summary>
        ///     Remove command, Destroy Command Pool
        /// </summary>
        public void Clear()
        {
            _doBuffer.Clear();
            _cancelBuffer.Clear();
            _cmd = null;
        }

        /// <summary>
        ///     Generate new command pool
        /// </summary>
        /// <param name="commandType">Command type</param>
        /// <returns>New command pool generated</returns>
        public static CommandPool Create(Type commandType)
        {
            var commandPool = ReferencePool.Acquire<CommandPool>();
            commandPool.Init((ICommand)Activator.CreateInstance(commandType));
            return commandPool;
        }

        /// <summary>
        ///     Set command
        /// </summary>
        /// <param name="command">Command</param>
        public void Init(ICommand command) => _cmd = command;
    }
}