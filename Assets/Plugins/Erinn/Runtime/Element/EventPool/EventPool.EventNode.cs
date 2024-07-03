//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Event Pool
    /// </summary>
    /// <typeparam name="T">Event type</typeparam>
    internal sealed partial class EventPool<T> where T : BaseEventArgs
    {
        /// <summary>
        ///     Event Node
        /// </summary>
        private readonly struct EventNode
        {
            /// <summary>
            ///     Sender
            /// </summary>
            public readonly object Sender;

            /// <summary>
            ///     Event parameters
            /// </summary>
            public readonly T EventArgs;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="sender">Sender </param>
            /// <param name="eventArgs">Event parameters</param>
            private EventNode(object sender, T eventArgs)
            {
                Sender = sender;
                EventArgs = eventArgs;
            }

            /// <summary>
            ///     Create event nodes
            /// </summary>
            /// <param name="sender">Sender </param>
            /// <param name="eventArgs">Event parameters</param>
            /// <returns>Event nodes created</returns>
            public static EventNode Create(object sender, T eventArgs) => new(sender, eventArgs);
        }
    }
}