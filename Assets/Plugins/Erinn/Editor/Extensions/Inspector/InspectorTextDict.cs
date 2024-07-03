//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    internal readonly struct InspectorTextDict
    {
        public static string SetColor(string color, string text) => "<color=#" + color + ">" + text + "</color>";
    }
}