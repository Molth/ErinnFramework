//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    [HideMonoScript]
    internal sealed class TimeText : MonoBehaviour
    {
        private GUISkin _skin;

        private void OnGUI()
        {
            SetupGUISkin();
            GUILayout.BeginArea(new Rect(Screen.width - 140, Screen.height - 40, 120, 80));
            DrawTextOutlined(MathV.GetCurrentTime(), ColorType.Aquamarine);
            GUILayout.EndArea();
        }

        private void SetupGUISkin()
        {
            if (_skin == null)
                _skin = Instantiate(GUI.skin);
            _skin.label.fontStyle = FontStyle.Bold;
            _skin.label.alignment = TextAnchor.MiddleRight;
            _skin.label.fontSize = 18;
            GUI.skin = _skin;
        }

        private void DrawTextOutlined(string text, Color color)
        {
            var rect = GUILayoutUtility.GetRect(new GUIContent(text), _skin.label);
            rect.y += 1;
            rect.x += 1;
            _skin.label.normal.textColor = Color.black;
            _skin.label.hover = _skin.label.normal;
            GUI.Label(rect, text);
            rect.y -= 1;
            rect.x -= 1;
            _skin.label.normal.textColor = color;
            _skin.label.hover = _skin.label.normal;
            GUI.Label(rect, text);
        }
    }
}