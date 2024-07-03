//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Abstract class of controller
    /// </summary>
    public abstract class Controller<T> : IController where T : class, IActor
    {
        /// <summary>
        ///     Owner of the controller
        /// </summary>
        protected T Owner { get; private set; }

        /// <summary>
        ///     Initialize the controller through an interface
        /// </summary>
        void IController.Init(IActor inputOwner, object[] args)
        {
            Owner = (T)inputOwner;
            OnInit(args);
        }

        /// <summary>
        ///     Controller release
        /// </summary>
        void IReference.Clear()
        {
            OnClear();
            Owner = null;
        }

        /// <summary>
        ///     Controller initialization
        /// </summary>
        protected abstract void OnInit(object[] args);

        /// <summary>
        ///     Controller release
        /// </summary>
        protected abstract void OnClear();
    }
}