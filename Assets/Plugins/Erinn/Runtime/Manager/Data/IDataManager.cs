//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Data Manager
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming int Primary key</param>
        /// <typeparam name="T">Can use all inheritanceI ExcelDataBase Object of type</typeparam>
        /// <returns>Result</returns>
        T Get<T>(int key) where T : IExcelDataBase;

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming string Primary key</param>
        /// <typeparam name="T">Can use all inheritanceI ExcelDataBase Object of type</typeparam>
        /// <returns>Result</returns>
        T Get<T>(string key) where T : IExcelDataBase;

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming Enum Primary key</param>
        /// <typeparam name="T">Can use all inheritanceI ExcelDataBase Object of type</typeparam>
        /// <returns>Result</returns>
        T Get<T>(Enum key) where T : IExcelDataBase;

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming int Primary key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Can use all inheritanceI ExcelDataBase Object of type</typeparam>
        /// <returns>Return a data object</returns>
        bool Get<T>(int key, out T result) where T : IExcelDataBase;

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming string Primary key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Can use all inheritanceI ExcelDataBase Object of type</typeparam>
        /// <returns>Return a data object</returns>
        bool Get<T>(string key, out T result) where T : IExcelDataBase;

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming Enum Primary key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Can use all inheritanceI ExcelDataBase Object of type</typeparam>
        /// <returns>Return a data object</returns>
        bool Get<T>(Enum key, out T result) where T : IExcelDataBase;
    }
}