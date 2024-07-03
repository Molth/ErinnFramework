//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Generic Information Interface
    /// </summary>
    /// <typeparam name="TMessage">Type</typeparam>
    public interface IMessage<in TMessage> : IMessage where TMessage : struct, IMessage
    {
        /// <summary>
        ///     Execution event
        /// </summary>
        /// <param name="msg">Information</param>
        void Execute(TMessage msg);
    }
}