//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    internal sealed partial class DataManager
    {
        /// <summary>
        ///     Storage intA data dictionary for primary key types
        /// </summary>
        private static readonly ExcelTableContainer<int> IntDataDict = new();

        /// <summary>
        ///     Storage stringA data dictionary for primary keys
        /// </summary>
        private static readonly ExcelTableContainer<string> StrDataDict = new();

        /// <summary>
        ///     Storage enumA data dictionary for primary keys
        /// </summary>
        private static readonly ExcelTableContainer<Enum> EnumDataDict = new();

        public override int Priority => 9;

        /// <summary>
        ///     Storage intA data dictionary for primary key types
        /// </summary>
        public static Dictionary<Type, Dictionary<int, IExcelDataBase>> IntData => IntDataDict.KeyDataDict;

        /// <summary>
        ///     Storage stringA data dictionary for primary keys
        /// </summary>
        public static Dictionary<Type, Dictionary<string, IExcelDataBase>> StrData => StrDataDict.KeyDataDict;

        /// <summary>
        ///     Storage enumA data dictionary for primary keys
        /// </summary>
        public static Dictionary<Type, Dictionary<Enum, IExcelDataBase>> EnumData => EnumDataDict.KeyDataDict;
    }
}