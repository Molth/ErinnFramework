//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.IO;
using UnityEngine;

namespace Erinn
{
    public static class TextureExtensions
    {
        public static Texture2D CreateTexture2D(this RenderTexture renderTexture, FilterMode filterMode)
        {
            RenderTexture.active = renderTexture;
            var texture2D = new Texture2D(renderTexture.width, renderTexture.height)
            {
                filterMode = filterMode
            };
            texture2D.ReadPixels(new Rect(0.0f, 0.0f, renderTexture.width, renderTexture.height), 0, 0);
            RenderTexture.active = null;
            texture2D.Apply();
            return texture2D;
        }

        public static Sprite CreateSprite(this Texture2D texture, Vector2 pivot, float pixelsPerUnit) => Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), pivot, pixelsPerUnit);

        public static void SaveToPNGFile(this Texture2D texture, string fileName)
        {
            var png = texture.EncodeToPNG();
            File.WriteAllBytes(fileName, png);
        }

        public static void SaveToPNGFile(this RenderTexture renderTexture, FilterMode filterMode, string fileName) => renderTexture.CreateTexture2D(filterMode).SaveToPNGFile(fileName);
    }
}