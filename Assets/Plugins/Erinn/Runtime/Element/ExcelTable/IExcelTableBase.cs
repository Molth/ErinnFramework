//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     ExcelData Table Base Class Interface
    /// </summary>
    public interface IExcelTableBase
    {
        /// <summary>
        ///     Data quantity
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Add data
        /// </summary>
        void AddData(object data);

        /// <summary>
        ///     Get data
        /// </summary>
        IExcelDataBase GetData(int index);
    }
}