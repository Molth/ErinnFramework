//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.IO;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Screen shot
    /// </summary>
    public static class Screenshot
    {
        public static void Capture(string directory, FilterMode filterMode)
        {
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fileName = (string.IsNullOrEmpty(directory) ? "" : directory + "/") + ConfigInfo.GetCurrentTimeFully() + "Screenshot" + ".png";
            var texture = Capture(filterMode);
            if (texture == null)
                return;
            texture.SaveToPNGFile(fileName);
            if (!Application.isPlaying)
                return;
            Object.Destroy(texture);
        }

        public static Texture2D Capture(FilterMode filterMode)
        {
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 16);
            var main = Camera.main;
            if (main == null)
                return null;
            var targetTexture = main.targetTexture;
            main.targetTexture = renderTexture;
            main.Render();
            main.targetTexture = targetTexture;
            return renderTexture.CreateTexture2D(filterMode);
        }
    }
}