//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Erinn
{
    internal sealed class UIRaycastChecker
    {
        public static void BeginRaycastCheck()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null)
            {
                Debug.LogWarning("<color=#FF0000>Please open a prefab!</color>");
                return;
            }

            var hasRef = new HashSet<Graphic>();
            var builder = new StringBuilder();
            var selectableArr = prefabStage.prefabContentsRoot.GetComponentsInChildren<Selectable>();
            foreach (var selectable in selectableArr)
                if (selectable.targetGraphic != null)
                    hasRef.Add(selectable.targetGraphic);
            var raycastArr = prefabStage.prefabContentsRoot.GetComponentsInChildren<ICanvasRaycastFilter>();
            var count = 0;
            foreach (var raycast in raycastArr)
            {
                var graphic = raycast as Graphic;
                if (graphic == null || !graphic.raycastTarget || hasRef.Contains(graphic))
                    continue;
                count++;
                builder.Append("{ " + $"<color=#00FFFF>{count}</color> -> <color=#00FF00>" + graphic.name + "</color> }");
                builder.Append("  ");
            }

            if (count == 0)
                Debug.Log("No extra Raycast Target !");
            else
                Debug.Log("Excess<color=#FF1100> Raycast Target</color> Quantity: <color=#FFAA00>" + count + " </color> => " + builder);
            hasRef.Clear();
        }
    }
}