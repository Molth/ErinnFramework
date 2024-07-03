//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     UIInterface Manager
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        ///     UIManager synchronization display UI
        /// </summary>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void Show<T>() where T : UIBase;

        /// <summary>
        ///     UIManager synchronization display UI
        /// </summary>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void Show<T>(Action<T> action) where T : UIBase;

        /// <summary>
        ///     UIManager asynchronous display UIBase class
        /// </summary>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void ShowAsync<T>() where T : UIBase;

        /// <summary>
        ///     UIManager asynchronous display UIBase class
        /// </summary>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void ShowAsync<T>(Action<T> action) where T : UIBase;

        /// <summary>
        ///     UIManager synchronization display UI
        /// </summary>
        /// <param name="path">Path</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void Show<T>(string path) where T : UIBase;

        /// <summary>
        ///     UIManager synchronization display UI
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void Show<T>(string path, Action<T> action) where T : UIBase;

        /// <summary>
        ///     UIManager asynchronous display UIBase class
        /// </summary>
        /// <param name="path">Path</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void ShowAsync<T>(string path) where T : UIBase;

        /// <summary>
        ///     UIManager asynchronous display UIBase class
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void ShowAsync<T>(string path, Action<T> action) where T : UIBase;

        /// <summary>
        ///     UIManager HiddenUIBase class
        /// </summary>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        void Hide<T>() where T : UIBase;

        /// <summary>
        ///     UIManager GetUIBase class
        /// </summary>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        /// <returns>Obtained UI</returns>
        T Get<T>() where T : UIBase;
    }
}