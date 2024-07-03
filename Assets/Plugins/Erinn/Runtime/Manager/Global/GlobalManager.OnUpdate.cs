//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Global Manager
    /// </summary>
    public sealed partial class GlobalManager
    {
        /// <summary>
        ///     Update Execution Event
        /// </summary>
        internal static readonly ActionContainer OnUpdate = new();

        /// <summary>
        ///     LateUpdate Execution Event
        /// </summary>
        internal static readonly ActionContainer OnLateUpdate = new();

        /// <summary>
        ///     FixedUpdate Execution Event
        /// </summary>
        internal static readonly ActionContainer OnFixedUpdate = new();

        /// <summary>
        ///     StartEvent
        /// </summary>
        public static event Action OnStart;

        /// <summary>
        ///     Exit Event
        /// </summary>
        public static event Action OnQuit;

        /// <summary>
        ///     Add event listening
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="action">Event</param>
        public static void AddListener(int key, Action action)
        {
            switch (key)
            {
                case 0:
                    OnUpdate.Add(action);
                    break;
                case 1:
                    OnLateUpdate.Add(action);
                    break;
                case 2:
                    OnFixedUpdate.Add(action);
                    break;
            }
        }

        /// <summary>
        ///     Remove event listening
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="action">Event</param>
        public static void RemoveListener(int key, Action action)
        {
            if (action == null)
                return;
            switch (key)
            {
                case 0:
                    OnUpdate.Remove(action);
                    break;
                case 1:
                    OnLateUpdate.Remove(action);
                    break;
                case 2:
                    OnFixedUpdate.Remove(action);
                    break;
            }
        }

        /// <summary>
        ///     Clear event listening
        /// </summary>
        /// <param name="key">Key</param>
        public static void ClearAllListener(int key)
        {
            switch (key)
            {
                case 0:
                    OnUpdate.Clear();
                    break;
                case 1:
                    OnLateUpdate.Clear();
                    break;
                case 2:
                    OnFixedUpdate.Clear();
                    break;
            }
        }

        /// <summary>
        ///     Clear all event listening
        /// </summary>
        public static void ClearAllListeners()
        {
            OnUpdate.Clear();
            OnLateUpdate.Clear();
            OnFixedUpdate.Clear();
        }
    }
}