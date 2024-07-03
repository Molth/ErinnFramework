//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    internal sealed class SortHelper
    {
        public delegate int SortCompare(string lString, string rString);

        public static HashSet<string> sortedGuid = new();
        public static Dictionary<string, SortType> sortedAsset = new();
        public static SortType curSortType = SortType.None;
        public static SortType pathType = SortType.None;
        public static SortType nameType = SortType.None;

        public static Dictionary<SortType, SortCompare> compareFunction = new()
        {
            { SortType.AscByPath, CompareWithPath },
            { SortType.DescByPath, CompareWithPathDesc },
            { SortType.AscByName, CompareWithName },
            { SortType.DescByName, CompareWithNameDesc }
        };

        public static void Init()
        {
            sortedGuid.Clear();
            sortedAsset.Clear();
        }

        public static void ChangeSortType(short sortGroup, Dictionary<SortType, SortType> handler, ref SortType recoverType)
        {
            if (SortConfig.sortTypeGroup[curSortType] == sortGroup)
            {
                curSortType = handler[curSortType];
            }
            else
            {
                curSortType = recoverType;
                if (curSortType == SortType.None) curSortType = handler[curSortType];
            }

            recoverType = curSortType;
        }

        public static void SortByName() => ChangeSortType(SortConfig.typeByNameGroup, SortConfig.sortTypeChangeByNameHandler, ref nameType);

        public static void SortByPath() => ChangeSortType(SortConfig.typeByPathGroup, SortConfig.sortTypeChangeByPathHandler, ref pathType);

        public static void SortChild(ReferenceFinderData.AssetDescription data)
        {
            if (data == null) return;
            if (sortedAsset.ContainsKey(data.path))
            {
                if (sortedAsset[data.path] == curSortType) return;
                var oldSortType = sortedAsset[data.path];
                if (SortConfig.sortTypeGroup[oldSortType] == SortConfig.sortTypeGroup[curSortType])
                {
                    FastSort(data.dependencies);
                    FastSort(data.references);
                }
                else
                {
                    NormalSort(data.dependencies);
                    NormalSort(data.references);
                }

                sortedAsset[data.path] = curSortType;
            }
            else
            {
                NormalSort(data.dependencies);
                NormalSort(data.references);
                sortedAsset.Add(data.path, curSortType);
            }
        }

        public static void NormalSort(List<string> strList)
        {
            var curCompare = compareFunction[curSortType];
            strList.Sort((l, r) => curCompare(l, r));
        }

        public static void FastSort(List<string> strList)
        {
            var i = 0;
            var j = strList.Count - 1;
            while (i < j)
            {
                (strList[i], strList[j]) = (strList[j], strList[i]);
                i++;
                j--;
            }
        }

        public static int CompareWithName(string lString, string rString)
        {
            var asset = ResourceReferenceInfo.data.assetDict;
            return string.Compare(asset[lString].name, asset[rString].name, StringComparison.Ordinal);
        }

        public static int CompareWithNameDesc(string lString, string rString) => 0 - CompareWithName(lString, rString);

        public static int CompareWithPath(string lString, string rString)
        {
            var asset = ResourceReferenceInfo.data.assetDict;
            return string.Compare(asset[lString].path, asset[rString].path, StringComparison.Ordinal);
        }

        public static int CompareWithPathDesc(string lString, string rString) => 0 - CompareWithPath(lString, rString);
    }
}