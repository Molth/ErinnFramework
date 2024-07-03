//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class DragAreaGetObject
    {
        public static Object[] GetOjbects(string meg = null)
        {
            var aEvent = Event.current;
            GUI.contentColor = Color.white;
            if (aEvent.type is EventType.DragUpdated or EventType.DragPerform)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                var needReturn = false;
                if (aEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    needReturn = true;
                }

                Event.current.Use();
                if (needReturn) return DragAndDrop.objectReferences;
            }

            return null;
        }
    }
}