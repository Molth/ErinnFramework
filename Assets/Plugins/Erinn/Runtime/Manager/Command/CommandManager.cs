//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Command Manager
    /// </summary>
    internal sealed class CommandManager : ModuleSingleton, ICommandManager
    {
        /// <summary>
        ///     Command Pool Storage Dictionary
        /// </summary>
        public static readonly Dictionary<Type, CommandPool> CommandDict = new();

        public override int Priority => 6;

        void ICommandManager.ExecuteDirectly<T>(object[] args) => ExecuteDirectly<T>(args);

        bool ICommandManager.CancelDirectly<T>(object[] args) => CancelDirectly<T>(args);

        void ICommandManager.Execute<T>(object[] args) => Execute<T>(args);

        bool ICommandManager.Cancel<T>() => Cancel<T>();

        bool ICommandManager.Redo<T>() => Redo<T>();

        void ICommandManager.CancelAll<T>() => CancelAll<T>();

        void ICommandManager.RedoAll<T>() => RedoAll<T>();

        void ICommandManager.Dispose<T>() => Dispose<T>();

        /// <summary>
        ///     Directly execute command
        /// </summary>
        public static void ExecuteDirectly<T>(object[] args) where T : ICommand
        {
            var key = typeof(T);
            if (!CommandDict.TryGetValue(key, out var commandPool))
            {
                commandPool = CommandPool.Create(key);
                CommandDict.Add(key, commandPool);
            }

            commandPool.ExecuteDirectly(args);
        }

        /// <summary>
        ///     Directly revoke command
        /// </summary>
        public static bool CancelDirectly<T>(object[] args) where T : ICommand
        {
            var key = typeof(T);
            if (CommandDict.TryGetValue(key, out var commandPool))
            {
                commandPool.CancelDirectly(args);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Execute command
        /// </summary>
        public static void Execute<T>(object[] args) where T : ICommand
        {
            var key = typeof(T);
            if (!CommandDict.TryGetValue(key, out var commandPool))
            {
                commandPool = CommandPool.Create(key);
                CommandDict.Add(key, commandPool);
            }

            commandPool.Execute(args);
        }

        /// <summary>
        ///     Revoke the previous command
        /// </summary>
        public static bool Cancel<T>() where T : ICommand
        {
            var key = typeof(T);
            return !CommandDict.TryGetValue(key, out var commandPool) || commandPool.Cancel();
        }

        /// <summary>
        ///     Redo the previous command
        /// </summary>
        public static bool Redo<T>() where T : ICommand
        {
            var key = typeof(T);
            return !CommandDict.TryGetValue(key, out var commandPool) || commandPool.Redo();
        }

        /// <summary>
        ///     Revoke All
        /// </summary>
        public static void CancelAll<T>() where T : ICommand
        {
            var key = typeof(T);
            if (CommandDict.TryGetValue(key, out var commandPool))
                commandPool.CancelAll();
        }

        /// <summary>
        ///     Redo All
        /// </summary>
        public static void RedoAll<T>() where T : ICommand
        {
            var key = typeof(T);
            if (CommandDict.TryGetValue(key, out var commandPool))
                commandPool.RedoAll();
        }

        /// <summary>
        ///     Remove command
        /// </summary>
        public static void Dispose<T>() where T : ICommand
        {
            var key = typeof(T);
            if (CommandDict.TryGetValue(key, out var commandPool))
            {
                ReferencePool.Release(commandPool);
                CommandDict.Remove(key);
            }
        }

        /// <summary>
        ///     Clear Command Manager
        /// </summary>
        public override void OnDispose()
        {
            foreach (var commandPool in CommandDict.Values)
                ReferencePool.Release(commandPool);
            CommandDict.Clear();
        }
    }
}