//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    public partial interface IEventManager
    {
        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener(string key, Action action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0>(string key, Action<T0> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1>(string key, Action<T0, T1> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2>(string key, Action<T0, T1, T2> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3>(string key, Action<T0, T1, T2, T3> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4>(string key, Action<T0, T1, T2, T3, T4> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5>(string key, Action<T0, T1, T2, T3, T4, T5> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6>(string key, Action<T0, T1, T2, T3, T4, T5, T6> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action);

        /// <summary>
        ///     Subscription to regular event
        /// </summary>
        void AddListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener(string key, Action action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0>(string key, Action<T0> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1>(string key, Action<T0, T1> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2>(string key, Action<T0, T1, T2> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3>(string key, Action<T0, T1, T2, T3> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4>(string key, Action<T0, T1, T2, T3, T4> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5>(string key, Action<T0, T1, T2, T3, T4, T5> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6>(string key, Action<T0, T1, T2, T3, T4, T5, T6> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action);

        /// <summary>
        ///     Remove ordinary event
        /// </summary>
        void RemoveListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast(string key);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0>(string key, T0 arg0);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1>(string key, T0 arg0, T1 arg1);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2>(string key, T0 arg0, T1 arg1, T2 arg2);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);

        /// <summary>
        ///     Broadcast ordinary event
        /// </summary>
        void Broadcast<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);

        /// <summary>
        ///     Clear ordinary event
        /// </summary>
        void ClearListener(string key);

        /// <summary>
        ///     Clear all normal event
        /// </summary>
        void ClearListeners();
    }
}