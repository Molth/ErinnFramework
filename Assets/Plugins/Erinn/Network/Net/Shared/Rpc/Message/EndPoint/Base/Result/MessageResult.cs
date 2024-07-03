//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Message result
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public struct MessageResult<T>
    {
        /// <summary>
        ///     State
        /// </summary>
        public MessageState State;

        /// <summary>
        ///     Result
        /// </summary>
        public T? Result;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="state">State</param>
        /// <param name="result">Result</param>
        public MessageResult(MessageState state, T? result = default)
        {
            State = state;
            Result = result;
        }

        /// <summary>
        ///     As T
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>T</returns>
        public static implicit operator T?(MessageResult<T> obj) => obj.Result;
    }
}