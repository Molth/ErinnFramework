//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Network server service channel
    /// </summary>
    public sealed class NetworkServerServiceChannel : INetworkServerServiceChannel
    {
        /// <summary>
        ///     Network server
        /// </summary>
        private readonly NetworkServer _peer;

        /// <summary>
        ///     EndPoint
        /// </summary>
        private readonly NetworkServerMessageEndPoint _endPoint;

        /// <summary>
        ///     Services
        /// </summary>
        private readonly Dictionary<Type, INetworkServerGate> _gates = new();

        /// <summary>
        ///     Server Service Event Handler Dictionary
        /// </summary>
        private readonly Dictionary<uint, NetworkServerServiceHandlerBase> _handlers = new();

        /// <summary>
        ///     Server Service Event Func Dictionary
        /// </summary>
        private readonly Dictionary<uint, NetworkServerServiceFuncBase> _funcs = new();

        /// <summary>
        ///     NetworkReader
        /// </summary>
        private readonly NetworkBuffer _reader = new(1024);

        /// <summary>
        ///     NetworkWriter
        /// </summary>
        private readonly NetworkBuffer _writer = new(1024);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="peer">Network server</param>
        /// <param name="endPoint">EndPoint</param>
        public NetworkServerServiceChannel(NetworkServer peer, NetworkServerMessageEndPoint endPoint)
        {
            _peer = peer;
            _peer.RegisterHandler<NetworkRpcPacket>(InvokeHandler);
            _peer.OnConnectedCallback += OnConnectedCallback;
            _peer.OnDisconnectedCallback += OnDisconnectedCallback;
            _endPoint = endPoint;
            _endPoint.RegisterFunc<NetworkRouteRequest, NetworkRouteResponse>(InvokeHandler);
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                AddServices(assembly);
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsAbstract && typeof(Service).IsAssignableFrom(type))
                    AddServiceInternal(type);
            }
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices(Type[] types)
        {
            if (types == null || types.Length == 0)
                return;
            foreach (var type in types)
                AddService(type);
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddService<T>() where T : Service, new()
        {
            var type = typeof(T);
            if (_gates.ContainsKey(type))
                return;
            var gate = new NetworkServerGate<T>(_peer, _endPoint);
            OnAddService(type, gate);
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddService(Type type)
        {
            if (type.IsAbstract || !typeof(Service).IsAssignableFrom(type))
            {
                Log.Warning("Invalid service type: [" + type.FullName + "]");
                return;
            }

            AddServiceInternal(type);
        }

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveServices()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                RemoveServices(assembly);
        }

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveServices(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsAbstract && typeof(Service).IsAssignableFrom(type))
                    RemoveService(type);
            }
        }

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveServices(Type[] types)
        {
            if (types == null || types.Length == 0)
                return;
            foreach (var type in types)
                RemoveService(type);
        }

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveService<T>() where T : Service, new() => RemoveService(typeof(T));

        /// <summary>
        ///     Remove Service
        /// </summary>
        public void RemoveService(Type type)
        {
            if (!_gates.Remove(type, out var gate))
                return;
            gate.UnregisterHandlers(RemoveHandlerCallback, RemoveFuncCallback);
            Log.Info("Unregister Service: [" + type.FullName + "]");
        }

        /// <summary>
        ///     Clear Service
        /// </summary>
        public void ClearServices()
        {
            foreach (var gate in _gates.Values)
                gate.UnregisterHandlers(RemoveHandlerCallback, RemoveFuncCallback);
            _gates.Clear();
            Log.Info("Clear Services");
        }

        /// <summary>
        ///     Remove handler callback
        /// </summary>
        private void RemoveHandlerCallback(uint command) => _handlers.Remove(command);

        /// <summary>
        ///     Remove func callback
        /// </summary>
        private void RemoveFuncCallback(uint command) => _funcs.Remove(command);

        /// <summary>
        ///     Add Service
        /// </summary>
        private void AddServiceInternal(Type type)
        {
            if (_gates.ContainsKey(type))
                return;
            var gate = (INetworkServerGate)Activator.CreateInstance(typeof(NetworkServerGate<>).MakeGenericType(type), _peer, _endPoint);
            OnAddService(type, gate);
        }

        /// <summary>
        ///     Add Service Callback
        /// </summary>
        private void OnAddService(Type type, INetworkServerGate gate)
        {
            gate.RegisterHandlers(AddHandlerCallback, AddFuncCallback);
            _gates[type] = gate;
            if (_peer.IsSet)
            {
                foreach (var connection in _peer.Connections)
                    gate.OnConnected(connection);
            }

            Log.Info("Register Service: [" + type.FullName + "]");
        }

        /// <summary>
        ///     Add handler callback
        /// </summary>
        private void AddHandlerCallback(uint command, NetworkServerServiceHandlerBase handler) => _handlers[command] = handler;

        /// <summary>
        ///     Add func callback
        /// </summary>
        private void AddFuncCallback(uint command, NetworkServerServiceFuncBase func) => _funcs[command] = func;

        /// <summary>
        ///     Server Receive NetworkRpcPacket Callback
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="networkRpcPacket">NetworkRpcPacket</param>
        private void InvokeHandler(uint id, NetworkRpcPacket networkRpcPacket)
        {
            if (!_handlers.TryGetValue(networkRpcPacket.Command, out var handler))
                return;
            var buffer = networkRpcPacket.Payload;
            _reader.SetBuffer(buffer);
            _reader.SetPosition(buffer.Count);
            try
            {
                handler.Invoke(id, _reader);
            }
            finally
            {
                _reader.Flush();
            }
        }

        /// <summary>
        ///     Server Receive NetworkRouteRequest Callback
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="networkRouteRequest">NetworkRouteRequest</param>
        private NetworkRouteResponse InvokeHandler(uint id, NetworkRouteRequest networkRouteRequest)
        {
            var command = networkRouteRequest.Command;
            if (!_funcs.TryGetValue(command, out var func))
                return new NetworkRouteResponse();
            var buffer = networkRouteRequest.Payload;
            _reader.SetBuffer(buffer);
            _reader.SetPosition(buffer.Count);
            try
            {
                var result = func.Invoke(id, _reader, _writer);
                return new NetworkRouteResponse(result);
            }
            catch
            {
                return new NetworkRouteResponse();
            }
            finally
            {
                _reader.Flush();
                _writer.Flush();
            }
        }

        /// <summary>
        ///     Call on connected
        /// </summary>
        /// <param name="id">ClientId</param>
        private void OnConnectedCallback(uint id)
        {
            foreach (var gate in _gates.Values)
                gate.OnConnected(id);
        }

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        /// <param name="id">ClientId</param>
        private void OnDisconnectedCallback(uint id)
        {
            foreach (var gate in _gates.Values)
                gate.OnDisconnected(id);
        }
    }
}