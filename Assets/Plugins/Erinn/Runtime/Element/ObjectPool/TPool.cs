//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Object Pool Interface
    /// </summary>
    internal interface IPool<T> : IPool
    {
        /// <summary>
        ///     Pop up object
        /// </summary>
        /// <returns>Obtained object</returns>
        T Pop();

        /// <summary>
        ///     Push Object
        /// </summary>
        /// <param name="obj">Object to be pushed in</param>
        /// <returns>Whether the object was successfully pushed</returns>
        bool Push(T obj);
    }
}