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
    ///     Network client service channel
    /// </summary>
    public sealed class NetworkClientServiceChannel : INetworkClientServiceChannel
    {
        /// <summary>
        ///     Network client
        /// </summary>
        private readonly NetworkClient _peer;

        /// <summary>
        ///     EndPoint
        /// </summary>
        private readonly NetworkClientMessageEndPoint _endPoint;

        /// <summary>
        ///     Services
        /// </summary>
        private readonly Dictionary<Type, ISession> _gates = new();

        /// <summary>
        ///     Client Service Event Handler Dictionary
        /// </summary>
        private readonly Dictionary<uint, NetworkClientServiceHandler> _handlers = new();

        /// <summary>
        ///     Server Service Event Func Dictionary
        /// </summary>
        private readonly Dictionary<uint, NetworkClientServiceFuncBase> _funcs = new();

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
        /// <param name="peer">Network client</param>
        /// <param name="endPoint">EndPoint</param>
        public NetworkClientServiceChannel(NetworkClient peer, NetworkClientMessageEndPoint endPoint)
        {
            _peer = peer;
            _endPoint = endPoint;
            _peer.RegisterHandler<NetworkRpcPacket>(InvokeHandler);
            _peer.OnConnectedCallback += OnConnectedCallback;
            _peer.OnDisconnectedCallback += OnDisconnectedCallback;
            _endPoint.RegisterFunc<NetworkRouteRequest, NetworkRouteResponse>(InvokeHandler);
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices()
        {
            if (!CheckState())
                return;
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (!type.IsAbstract && typeof(Session).IsAssignableFrom(type))
                        AddServiceInternal(type);
                }
            }
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices(Assembly assembly)
        {
            if (!CheckState())
                return;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsAbstract && typeof(Session).IsAssignableFrom(type))
                    AddServiceInternal(type);
            }
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddServices(Type[] types)
        {
            if (!CheckState())
                return;
            if (types == null || types.Length == 0)
                return;
            foreach (var type in types)
            {
                if (type.IsAbstract || !typeof(Session).IsAssignableFrom(type))
                {
                    Log.Warning("Invalid service type: [" + type.FullName + "]");
                    continue;
                }

                AddServiceInternal(type);
            }
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddService<T>() where T : Session, new()
        {
            if (!CheckState())
                return;
            var type = typeof(T);
            if (_gates.ContainsKey(type))
                return;
            var gate = new T();
            OnAddService(type, gate);
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        public void AddService(Type type)
        {
            if (!CheckState())
                return;
            if (type.IsAbstract || !typeof(Session).IsAssignableFrom(type))
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
                if (!type.IsAbstract && typeof(Session).IsAssignableFrom(type))
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
        public void RemoveService<T>() where T : Session, new() => RemoveService(typeof(T));

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
        ///     Clear Services
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
        ///     Check Connection State
        /// </summary>
        /// <returns>Connected</returns>
        private bool CheckState()
        {
            if (!_peer.IsSet || !_peer.Connected)
                return true;
            Log.Warning("Services can only be added when not connected");
            return false;
        }

        /// <summary>
        ///     Add Service
        /// </summary>
        private void AddServiceInternal(Type type)
        {
            if (_gates.ContainsKey(type))
                return;
            var gate = (ISession)Activator.CreateInstance(type);
            OnAddService(type, gate);
        }

        /// <summary>
        ///     Add Service Callback
        /// </summary>
        private void OnAddService(Type type, ISession gate)
        {
            gate.RegisterHandlers(_peer, _endPoint, AddHandlerCallback, AddFuncCallback);
            _gates[type] = gate;
            Log.Info("Register Service: [" + type.FullName + "]");
        }

        /// <summary>
        ///     Add handler callback
        /// </summary>
        private void AddHandlerCallback(uint command, NetworkClientServiceHandler handler) => _handlers[command] = handler;

        /// <summary>
        ///     Add func callback
        /// </summary>
        private void AddFuncCallback(uint command, NetworkClientServiceFuncBase func) => _funcs[command] = func;

        /// <summary>
        ///     Client Receive NetworkRpcPacket Callback
        /// </summary>
        /// <param name="networkRpcPacket">NetworkRpcPacket</param>
        private void InvokeHandler(NetworkRpcPacket networkRpcPacket)
        {
            if (!_handlers.TryGetValue(networkRpcPacket.Command, out var handler))
                return;
            var buffer = networkRpcPacket.Payload;
            _reader.SetBuffer(buffer);
            _reader.SetPosition(buffer.Count);
            try
            {
                handler.Invoke(_reader);
            }
            finally
            {
                _reader.Flush();
            }
        }

        /// <summary>
        ///     Server Receive NetworkRouteRequest Callback
        /// </summary>
        /// <param name="networkRouteRequest">NetworkRouteRequest</param>
        private NetworkRouteResponse InvokeHandler(NetworkRouteRequest networkRouteRequest)
        {
            var command = networkRouteRequest.Command;
            if (!_funcs.TryGetValue(command, out var func))
                return new NetworkRouteResponse();
            var buffer = networkRouteRequest.Payload;
            _reader.SetBuffer(buffer);
            _reader.SetPosition(buffer.Count);
            try
            {
                var result = func.Invoke(_reader, _writer);
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
        private void OnConnectedCallback()
        {
            foreach (var gate in _gates.Values)
                gate.OnConnected();
        }

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        private void OnDisconnectedCallback()
        {
            foreach (var gate in _gates.Values)
                gate.OnDisconnected();
        }
    }
}