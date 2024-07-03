//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    public partial interface IEventManager
    {
        /// <summary>
        ///     Get the number of pool event handling functions
        /// </summary>
        int EventHandlerCount { get; }

        /// <summary>
        ///     Get the number of pool events
        /// </summary>
        int EventCount { get; }

        /// <summary>
        ///     Get the number of pool event handling functions
        /// </summary>
        /// <param name="id">Pool Event Type Number</param>
        /// <returns>The number of pool event handling functions</returns>
        int Count(int id);

        /// <summary>
        ///     Check for the existence of pool event handling functions
        /// </summary>
        /// <param name="id">Pool Event Type Number</param>
        /// <param name="handler">The pool event handling function to be checked</param>
        /// <returns>Is there a pool event handling function present</returns>
        bool Check(int id, EventHandler<BaseEventArgs> handler);

        /// <summary>
        ///     Subscription Pool Event Handling Function
        /// </summary>
        /// <param name="id">Pool Event Type Number</param>
        /// <param name="handler">Pool event handler to subscribe to</param>
        void Subscribe(int id, EventHandler<BaseEventArgs> handler);

        /// <summary>
        ///     Unsubscribe Pool Event Handling Function
        /// </summary>
        /// <param name="id">Pool Event Type Number</param>
        /// <param name="handler">Pool event handler to unsubscribe from</param>
        void Unsubscribe(int id, EventHandler<BaseEventArgs> handler);

        /// <summary>
        ///     Throw event immediate mode, This operation is not thread safe, The incident will be immediately distributed
        /// </summary>
        /// <param name="sender">Pool Event Source</param>
        /// <param name="e">Pool event parameters</param>
        void Fire(object sender, BaseEventArgs e);

        /// <summary>
        ///     Close and clean up the pool event manager
        /// </summary>
        void Shutdown();
    }
}