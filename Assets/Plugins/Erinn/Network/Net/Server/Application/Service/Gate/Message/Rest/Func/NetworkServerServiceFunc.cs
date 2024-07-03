//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
#endif

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var result = _func.Invoke(session);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var result = _func.Invoke(session, param0);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var result = _func.Invoke(session, param0, param1);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var result = _func.Invoke(session, param0, param1, param2);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var result = _func.Invoke(session, param0, param1, param2, param3);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var param10 = reader.Read<T10>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var param10 = reader.Read<T10>();
            var param11 = reader.Read<T11>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var param10 = reader.Read<T10>();
            var param11 = reader.Read<T11>();
            var param12 = reader.Read<T12>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var param10 = reader.Read<T10>();
            var param11 = reader.Read<T11>();
            var param12 = reader.Read<T12>();
            var param13 = reader.Read<T13>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var param10 = reader.Read<T10>();
            var param11 = reader.Read<T11>();
            var param12 = reader.Read<T12>();
            var param13 = reader.Read<T13>();
            var param14 = reader.Read<T14>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }

    /// <summary>
    ///     MessageProcessor
    /// </summary>
    internal sealed class NetworkServerServiceFunc<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : NetworkServerServiceFuncBase where TS : Service, new()
    {
        /// <summary>
        ///     Sessions
        /// </summary>
        private readonly Dictionary<uint, TS> _sessions;

        /// <summary>
        ///     Func
        /// </summary>
        private readonly Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> _func;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="sessions">Sessions</param>
        /// <param name="func">Func</param>
        public NetworkServerServiceFunc(Dictionary<uint, TS> sessions, Func<TS, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> func)
        {
            _sessions = sessions;
            _func = func;
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="reader">NetworkReader</param>
        /// <param name="writer">NetworkWriter</param>
        public override ArraySegment<byte> Invoke(uint id, NetworkBuffer reader, NetworkBuffer writer)
        {
            if (!_sessions.TryGetValue(id, out var session))
                return default;
            var param0 = reader.Read<T0>();
            var param1 = reader.Read<T1>();
            var param2 = reader.Read<T2>();
            var param3 = reader.Read<T3>();
            var param4 = reader.Read<T4>();
            var param5 = reader.Read<T5>();
            var param6 = reader.Read<T6>();
            var param7 = reader.Read<T7>();
            var param8 = reader.Read<T8>();
            var param9 = reader.Read<T9>();
            var param10 = reader.Read<T10>();
            var param11 = reader.Read<T11>();
            var param12 = reader.Read<T12>();
            var param13 = reader.Read<T13>();
            var param14 = reader.Read<T14>();
            var param15 = reader.Read<T15>();
            var result = _func.Invoke(session, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15);
            writer.Write(result);
            return writer.ToArraySegment();
        }
    }
}