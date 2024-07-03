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
    ///     Service Server
    /// </summary>
    public abstract partial class ApplicationBase
    {
        /// <summary>
        ///     Network server service channel
        /// </summary>
        private readonly NetworkServerServiceChannel _serviceChannel;

        /// <summary>
        ///     Add Service Manager
        /// </summary>
        public void AddServices() => _serviceChannel.AddServices();

        /// <summary>
        ///     Add Service Manager
        /// </summary>
        public void AddServices(Assembly assembly) => _serviceChannel.AddServices(assembly);

        /// <summary>
        ///     Add Service Manager
        /// </summary>
        public void AddServices(Type[] types) => _serviceChannel.AddServices(types);

        /// <summary>
        ///     Add Service Manager
        /// </summary>
        public void AddService<T>() where T : Service, new() => _serviceChannel.AddService<T>();

        /// <summary>
        ///     Add Service Manager
        /// </summary>
        public void AddService(Type type) => _serviceChannel.AddService(type);

        /// <summary>
        ///     Remove Service Manager
        /// </summary>
        public void RemoveServices() => _serviceChannel.RemoveServices();

        /// <summary>
        ///     Remove Service Manager
        /// </summary>
        public void RemoveServices(Assembly assembly) => _serviceChannel.RemoveServices(assembly);

        /// <summary>
        ///     Remove Service Manager
        /// </summary>
        public void RemoveServices(Type[] types) => _serviceChannel.RemoveServices(types);

        /// <summary>
        ///     Remove Service Manager
        /// </summary>
        public void RemoveService<T>() where T : Service, new() => _serviceChannel.RemoveService<T>();

        /// <summary>
        ///     Remove Service Manager
        /// </summary>
        public void RemoveService(Type type) => _serviceChannel.RemoveService(type);

        /// <summary>
        ///     Clear Service Managers
        /// </summary>
        public void ClearServices() => _serviceChannel.ClearServices();
    }
}