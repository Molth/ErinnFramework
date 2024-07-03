//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;

namespace Erinn
{
    internal sealed class ExcelTableContainer<TKey>
    {
        /// <summary>
        ///     Generic Data Dictionary
        /// </summary>
        [ShowInInspector] public readonly Dictionary<Type, Dictionary<TKey, IExcelDataBase>> KeyDataDict = new();

        /// <summary>
        ///     Add data
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="field">Information</param>
        /// <param name="table">Data sheet</param>
        public void Add(Type type, FieldInfo field, IExcelTableBase table)
        {
            var dataDict = new Dictionary<TKey, IExcelDataBase>();
            for (var i = 0; i < table.Count; ++i)
            {
                var data = table.GetData(i);
                var key = (TKey)field.GetValue(data);
                dataDict.TryAdd(key, data);
            }

            KeyDataDict[type] = dataDict;
        }

        /// <summary>
        ///     Get data
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="TData">Data</typeparam>
        /// <returns>Successfully obtained data</returns>
        public bool Get<TData>(TKey key, out TData result) where TData : IExcelDataBase
        {
            if (KeyDataDict.TryGetValue(typeof(TData), out var dataDict))
                if (dataDict.TryGetValue(key, out var data))
                {
                    result = (TData)data;
                    return true;
                }

            result = default;
            return false;
        }

        /// <summary>
        ///     Clear data
        /// </summary>
        public void Clear() => KeyDataDict.Clear();
    }
}