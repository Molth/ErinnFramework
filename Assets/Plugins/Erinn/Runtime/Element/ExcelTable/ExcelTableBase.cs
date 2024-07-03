//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     ExcelData Table Base Class
    /// </summary>
    [Serializable]
    public abstract class ExcelTableBase<T> : ScriptableObject, IExcelTableBase where T : IExcelDataBase
    {
        /// <summary>
        ///     Data List
        /// </summary>
        [SerializeField] private List<T> _dataList = new();

        /// <summary>
        ///     Data quantity
        /// </summary>
        int IExcelTableBase.Count => _dataList.Count;

        /// <summary>
        ///     Add data
        /// </summary>
        void IExcelTableBase.AddData(object data) => AddData((T)data);

        /// <summary>
        ///     Get data
        /// </summary>
        IExcelDataBase IExcelTableBase.GetData(int index) => GetData(index);

        /// <summary>
        ///     Add data
        /// </summary>
        private void AddData(T data) => _dataList.Add(data);

        /// <summary>
        ///     Get data
        /// </summary>
        private T GetData(int index) => _dataList[index];
    }
}