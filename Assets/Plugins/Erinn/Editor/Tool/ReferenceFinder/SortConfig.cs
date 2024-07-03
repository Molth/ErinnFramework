//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;

namespace Erinn
{
    internal sealed class SortConfig
    {
        public static Dictionary<SortType, SortType> sortTypeChangeByNameHandler = new()
        {
            { SortType.None, SortType.AscByName },
            { SortType.AscByName, SortType.DescByName },
            { SortType.DescByName, SortType.AscByName }
        };

        public static Dictionary<SortType, SortType> sortTypeChangeByPathHandler = new()
        {
            { SortType.None, SortType.AscByPath },
            { SortType.AscByPath, SortType.DescByPath },
            { SortType.DescByPath, SortType.AscByPath }
        };

        public static Dictionary<SortType, short> sortTypeGroup = new()
        {
            { SortType.None, 0 },
            { SortType.AscByPath, 1 },
            { SortType.DescByPath, 1 },
            { SortType.AscByName, 2 },
            { SortType.DescByName, 2 }
        };

        public static short typeByNameGroup = 2;
        public static short typeByPathGroup = 1;
    }
}