//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Network client service channel interface
    /// </summary>
    public interface INetworkClientServiceChannel
    {
        /// <summary>
        ///     Add Service
        /// </summary>
        void AddServices();

        /// <summary>
        ///     Add Service
        /// </summary>
        void AddServices(Assembly assembly);

        /// <summary>
        ///     Add Service
        /// </summary>
        void AddServices(Type[] types);

        /// <summary>
        ///     Add Service
        /// </summary>
        void AddService<T>() where T : Session, new();

        /// <summary>
        ///     Add Service
        /// </summary>
        void AddService(Type type);

        /// <summary>
        ///     Remove Service
        /// </summary>
        void RemoveServices();

        /// <summary>
        ///     Remove Service
        /// </summary>
        void RemoveServices(Assembly assembly);

        /// <summary>
        ///     Remove Service
        /// </summary>
        void RemoveServices(Type[] types);

        /// <summary>
        ///     Remove Service
        /// </summary>
        void RemoveService<T>() where T : Session, new();

        /// <summary>
        ///     Remove Service
        /// </summary>
        void RemoveService(Type type);

        /// <summary>
        ///     Clear Service
        /// </summary>
        void ClearServices();
    }
}