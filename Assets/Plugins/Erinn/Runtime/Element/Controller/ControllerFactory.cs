//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Controller Factory
    /// </summary>
    public static class ControllerFactory
    {
        /// <summary>
        ///     Get controller
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained controller</returns>
        public static T GetController<T>(IActor owner) where T : IController => GetController<T>(owner, null);

        /// <summary>
        ///     Get controller
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <param name="args">Parameter</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained controller</returns>
        public static T GetController<T>(IActor owner, object[] args) where T : IController
        {
            var controller = (T)ReferencePool.Acquire(typeof(T));
            controller.Init(owner, args);
            return controller;
        }
    }
}