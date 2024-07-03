//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static bool Contains(this LayerMask mask, int layer) => mask == (mask | (1 << layer));

        public static LayerMask Add(this LayerMask mask, int layer)
        {
            mask |= 1 << layer;
            return mask;
        }

        public static LayerMask Add(this LayerMask mask, LayerMask layers)
        {
            mask |= layers;
            return mask;
        }

        public static LayerMask Remove(this LayerMask mask, int layer) => mask & ~(1 << layer);

        public static LayerMask Remove(this LayerMask mask, LayerMask layers) => mask & ~layers;

        public static int[] ToArray(this LayerMask mask)
        {
            var intList = new List<int>();
            for (var layer = 0; layer < 32; ++layer)
                if (mask.Contains(layer))
                    intList.Add(layer);
            return intList.ToArray();
        }
    }
}