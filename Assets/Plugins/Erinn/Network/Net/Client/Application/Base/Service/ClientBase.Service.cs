//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Client Application base
    /// </summary>
    public abstract partial class ClientBase
    {
        /// <summary>
        ///     Network client service channel
        /// </summary>
        private readonly NetworkClientServiceChannel _serviceChannel;

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices() => _serviceChannel.AddServices();

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices(Assembly assembly) => _serviceChannel.AddServices(assembly);

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices(Type[] types) => _serviceChannel.AddServices(types);

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddService<T>() where T : Session, new() => _serviceChannel.AddService<T>();

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddService(Type type) => _serviceChannel.AddService(type);

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveServices() => _serviceChannel.RemoveServices();

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveServices(Assembly assembly) => _serviceChannel.RemoveServices(assembly);

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveServices(Type[] types) => _serviceChannel.RemoveServices(types);

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveService<T>() where T : Session, new() => _serviceChannel.RemoveService<T>();

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveService(Type type) => _serviceChannel.RemoveService(type);

        /// <summary>
        ///     Clear Services
        /// </summary>
        public void ClearServices() => _serviceChannel.ClearServices();
    }
}