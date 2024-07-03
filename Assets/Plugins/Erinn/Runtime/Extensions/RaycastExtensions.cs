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
        public static bool CompareTag(this RaycastHit2D hit, string tag) => hit.transform.CompareTag(tag);

        public static bool CompareLayer(this RaycastHit2D hit, int layer) => hit.transform.gameObject.layer == layer;

        public static bool CompareLayer(this RaycastHit2D hit, IEnumerable<int> layers)
        {
            var hitLayer = hit.transform.gameObject.layer;
            foreach (var layer in layers)
                if (hitLayer == layer)
                    return true;
            return false;
        }

        public static T GetComponent<T>(this RaycastHit2D hit) => hit.transform.GetComponent<T>();

        public static bool TryGetComponent<T>(this RaycastHit2D hit, out T component) => hit.transform.TryGetComponent(out component);

        public static bool CompareTag(this RaycastHit hit, string tag) => hit.transform.CompareTag(tag);

        public static bool CompareLayer(this RaycastHit hit, int layer) => hit.transform.gameObject.layer == layer;

        public static bool CompareLayer(this RaycastHit hit, IEnumerable<int> layers)
        {
            var hitLayer = hit.transform.gameObject.layer;
            foreach (var layer in layers)
                if (hitLayer == layer)
                    return true;
            return false;
        }

        public static T GetComponent<T>(this RaycastHit hit) => hit.transform.GetComponent<T>();

        public static bool TryGetComponent<T>(this RaycastHit hit, out T component) => hit.transform.TryGetComponent(out component);
    }
}