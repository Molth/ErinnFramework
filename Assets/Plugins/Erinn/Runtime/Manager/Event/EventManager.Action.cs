//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    internal sealed partial class EventManager
    {
        public static readonly Dictionary<string, EventContainer> EventTable = new();

        void IEventManager.AddListener(string key, Action action) => AddListener(key, action);

        void IEventManager.AddListener<T0>(string key, Action<T0> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1>(string key, Action<T0, T1> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2>(string key, Action<T0, T1, T2> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3>(string key, Action<T0, T1, T2, T3> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4>(string key, Action<T0, T1, T2, T3, T4> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5>(string key, Action<T0, T1, T2, T3, T4, T5> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6>(string key, Action<T0, T1, T2, T3, T4, T5, T6> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action) => AddListener(key, action);

        void IEventManager.AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action) => AddListener(key, action);

        void IEventManager.RemoveListener(string key, Action action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0>(string key, Action<T0> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1>(string key, Action<T0, T1> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2>(string key, Action<T0, T1, T2> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3>(string key, Action<T0, T1, T2, T3> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4>(string key, Action<T0, T1, T2, T3, T4> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5>(string key, Action<T0, T1, T2, T3, T4, T5> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6>(string key, Action<T0, T1, T2, T3, T4, T5, T6> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action) => RemoveListener(key, action);

        void IEventManager.RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action) => RemoveListener(key, action);

        void IEventManager.Broadcast(string key) => Broadcast(key);

        void IEventManager.Broadcast<T0>(string key, T0 arg0) => Broadcast(key, arg0);

        void IEventManager.Broadcast<T0, T1>(string key, T0 arg0, T1 arg1) => Broadcast(key, arg0, arg1);

        void IEventManager.Broadcast<T0, T1, T2>(string key, T0 arg0, T1 arg1, T2 arg2) => Broadcast(key, arg0, arg1, arg2);

        void IEventManager.Broadcast<T0, T1, T2, T3>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3) => Broadcast(key, arg0, arg1, arg2, arg3);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Broadcast(key, arg0, arg1, arg2, arg3, arg4);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

        void IEventManager.Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15) => Broadcast(key, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

        void IEventManager.ClearListener(string key) => ClearListener(key);

        void IEventManager.ClearListeners() => ClearListeners();

        public static void TryAdd<T>(string key, T action) where T : Delegate
        {
            if (EventTable.TryGetValue(key, out var value))
                value.Add(action);
            else
                EventTable.Add(key, EventContainer.Create(action));
        }

        public static void TryRemove<T>(string key, T action) where T : Delegate
        {
            if (EventTable.TryGetValue(key, out var value))
            {
                value.Remove(action);
                if (value.Count == 0)
                {
                    value.Dispose();
                    EventTable.Remove(key);
                }
            }
        }

        public static void AddListener(string key, Action action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0>(string key, Action<T0> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1>(string key, Action<T0, T1> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2>(string key, Action<T0, T1, T2> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3>(string key, Action<T0, T1, T2, T3> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4>(string key, Action<T0, T1, T2, T3, T4> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5>(string key, Action<T0, T1, T2, T3, T4, T5> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6>(string key, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryAdd(key, action);
        }

        public static void RemoveListener(string key, Action action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0>(string key, Action<T0> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1>(string key, Action<T0, T1> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2>(string key, Action<T0, T1, T2> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3>(string key, Action<T0, T1, T2, T3> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4>(string key, Action<T0, T1, T2, T3, T4> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5>(string key, Action<T0, T1, T2, T3, T4, T5> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6>(string key, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            if (string.IsNullOrEmpty(key) || action == null)
                return;
            TryRemove(key, action);
        }

        public static void Broadcast(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action>(out var action))
                    action.Invoke();
        }

        public static void Broadcast<T0>(string key, T0 arg0)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0>>(out var action))
                    action.Invoke(arg0);
        }

        public static void Broadcast<T0, T1>(string key, T0 arg0, T1 arg1)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1>>(out var action))
                    action.Invoke(arg0, arg1);
        }

        public static void Broadcast<T0, T1, T2>(string key, T0 arg0, T1 arg1, T2 arg2)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2>>(out var action))
                    action.Invoke(arg0, arg1, arg2);
        }

        public static void Broadcast<T0, T1, T2, T3>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3);
        }

        public static void Broadcast<T0, T1, T2, T3, T4>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        public static void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
                if (value.TryGetValue<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(out var action))
                    action.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        public static void ClearListener(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (EventTable.TryGetValue(key, out var value))
            {
                value.Dispose();
                EventTable.Remove(key);
            }
        }

        public static void ClearListeners()
        {
            foreach (var value in EventTable.Values)
                value.Dispose();
            EventTable.Clear();
        }
    }
}