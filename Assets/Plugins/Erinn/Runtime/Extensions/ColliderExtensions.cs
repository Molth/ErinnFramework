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
        public static bool CompareLayer(this Collider2D hit, int layer) => hit.gameObject.layer == layer;

        public static bool CompareLayer(this Collider2D hit, IEnumerable<int> layers)
        {
            var hitLayer = hit.gameObject.layer;
            foreach (var layer in layers)
                if (hitLayer == layer)
                    return true;
            return false;
        }

        public static bool CompareLayer(this Collider hit, int layer) => hit.gameObject.layer == layer;

        public static bool CompareLayer(this Collider hit, IEnumerable<int> layers)
        {
            var hitLayer = hit.gameObject.layer;
            foreach (var layer in layers)
                if (hitLayer == layer)
                    return true;
            return false;
        }
    }
}