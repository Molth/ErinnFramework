//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Entity Interface
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///     Get controller
        /// </summary>
        /// <param name="args">Parameter</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained controller</returns>
        T GetController<T>(params object[] args) where T : IController;

        /// <summary>
        ///     Remove controller
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void RemoveController<T>() where T : IController;

        /// <summary>
        ///     Release controller
        /// </summary>
        void ReleaseControllers();
    }
}