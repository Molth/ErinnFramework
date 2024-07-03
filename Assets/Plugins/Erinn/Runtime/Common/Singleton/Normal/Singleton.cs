//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Ordinary singleton
    /// </summary>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>, new()
    {
        /// <summary>
        ///     Singleton
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        ///     Registration singleton
        /// </summary>
        void ISingleton.RegisterInstance() => Instance = (T)this;

        /// <summary>
        ///     Singleton of destruction
        /// </summary>
        void ISingleton.DisposeInstance() => Instance = null;

        /// <summary>
        ///     Registration singleton
        /// </summary>
        public static void Register() => SingletonSystem.Register<T>();

        /// <summary>
        ///     Singleton of destruction
        /// </summary>
        public static void Dispose() => SingletonSystem.Dispose<T>();
    }
}