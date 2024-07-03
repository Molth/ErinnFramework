//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    internal sealed partial class DataManager
    {
        T IDataManager.Get<T>(int key) => Get<T>(key);

        T IDataManager.Get<T>(string key) => Get<T>(key);

        T IDataManager.Get<T>(Enum key) => Get<T>(key);

        bool IDataManager.Get<T>(int key, out T result) => Get(key, out result);

        bool IDataManager.Get<T>(string key, out T result) => Get(key, out result);

        bool IDataManager.Get<T>(Enum key, out T result) => Get(key, out result);

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming int Primary key</param>
        /// <typeparam name="T">Can use all inheritanceIExcelDataBaseObject of type</typeparam>
        /// <returns>Result</returns>
        public static T Get<T>(int key) where T : IExcelDataBase
        {
            IntDataDict.Get<T>(key, out var result);
            return result;
        }

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming string Primary key</param>
        /// <typeparam name="T">To obtain the type of data, Must inherit fromI ExcelDataBase</typeparam>
        /// <returns>Result</returns>
        public static T Get<T>(string key) where T : IExcelDataBase
        {
            StrDataDict.Get<T>(key, out var result);
            return result;
        }

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming Enum Primary key</param>
        /// <typeparam name="T">To obtain the type of data, Must inherit fromI ExcelDataBase</typeparam>
        /// <returns>Result</returns>
        public static T Get<T>(Enum key) where T : IExcelDataBase
        {
            EnumDataDict.Get<T>(key, out var result);
            return result;
        }

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming int Primary key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Can use all inheritanceIExcelDataBaseObject of type</typeparam>
        /// <returns>Return a data object</returns>
        public static bool Get<T>(int key, out T result) where T : IExcelDataBase => IntDataDict.Get(key, out result);

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming string Primary key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">To obtain the type of data, Must inherit fromI ExcelDataBase</typeparam>
        /// <returns>Return a data object</returns>
        public static bool Get<T>(string key, out T result) where T : IExcelDataBase => StrDataDict.Get(key, out result);

        /// <summary>
        ///     Obtain the primary key for the corresponding type of data as KeyData for
        /// </summary>
        /// <param name="key">Incoming Enum Primary key</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">To obtain the type of data, Must inherit fromI ExcelDataBase</typeparam>
        /// <returns>Return a data object</returns>
        public static bool Get<T>(Enum key, out T result) where T : IExcelDataBase => EnumDataDict.Get(key, out result);
    }
}