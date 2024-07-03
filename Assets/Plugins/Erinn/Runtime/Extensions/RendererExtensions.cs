//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static void FixToScreen(this Renderer renderer)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            var targetAspectRatio = screenWidth / screenHeight;
            var scaleFactor = targetAspectRatio / (1920f / 1080f);
            switch (scaleFactor)
            {
                case > 1f:
                {
                    var trans = renderer.transform;
                    var scale = trans.localScale;
                    scale.x *= scaleFactor;
                    trans.localScale = scale;
                    break;
                }
                case < 1f:
                {
                    var trans = renderer.transform;
                    var scale = trans.localScale;
                    scale.y /= scaleFactor;
                    trans.localScale = scale;
                    break;
                }
            }
        }

        public static void FixToScreen(this Renderer renderer, float width, float height)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            var targetAspectRatio = screenWidth / screenHeight;
            var scaleFactor = targetAspectRatio / (width / height);
            switch (scaleFactor)
            {
                case > 1f:
                {
                    var trans = renderer.transform;
                    var scale = trans.localScale;
                    scale.x *= scaleFactor;
                    trans.localScale = scale;
                    break;
                }
                case < 1f:
                {
                    var trans = renderer.transform;
                    var scale = trans.localScale;
                    scale.y /= scaleFactor;
                    trans.localScale = scale;
                    break;
                }
            }
        }

        public static void FixedToScreenBottom(this Renderer renderer)
        {
            var main = Camera.main;
            if (main == null)
                return;
            var height = renderer.bounds.size.y;
            var screenBottomPosY = main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y;
            var newY = screenBottomPosY + height * 0.5f;
            renderer.transform.position = new Vector3(0f, newY, 0f);
        }

        public static void FixedToScreenHorizontal(this Renderer renderer)
        {
            var main = Camera.main;
            if (main == null)
                return;
            var num1 = main.orthographicSize * 2.0;
            var num2 = (float)num1 * main.aspect;
            var size = renderer.bounds.size;
            var x1 = size.x;
            var x2 = num2 / x1;
            renderer.transform.position = Vector3.zero;
            renderer.transform.eulerAngles = Vector3.zero;
            renderer.transform.localScale = new Vector3(x2, renderer.transform.localScale.y, 1f);
        }

        public static void FixedToScreenVertical(this Renderer renderer)
        {
            var cam = Camera.main;
            if (cam == null)
                return;
            var cameraHeight = cam.orthographicSize * 2f;
            var size = renderer.bounds.size;
            var height = size.y;
            var scaleY = cameraHeight / height;
            var trans = renderer.transform;
            trans.position = Vector3.zero;
            trans.eulerAngles = Vector3.zero;
            trans.localScale = new Vector3(renderer.transform.localScale.x, scaleY, 1f);
        }

        public static void FillScreen(this Renderer renderer)
        {
            var cam = Camera.main;
            if (cam == null)
                return;
            var cameraHeight = cam.orthographicSize * 2f;
            var cameraWidth = cameraHeight * cam.aspect;
            var size = renderer.bounds.size;
            var height = size.y;
            var width = size.x;
            var scaleY = cameraHeight / height;
            var scaleX = cameraWidth / width;
            var trans = renderer.transform;
            trans.position = Vector3.zero;
            trans.eulerAngles = Vector3.zero;
            trans.localScale = new Vector3(scaleX, scaleY, 1f);
        }

        public static void FillScreenPreserved(this Renderer renderer)
        {
            var cam = Camera.main;
            if (cam == null)
                return;
            var cameraHeight = cam.orthographicSize * 2f;
            var cameraWidth = cameraHeight * cam.aspect;
            var size = renderer.bounds.size;
            var height = size.y;
            var width = size.x;
            var scaleY = cameraHeight / height;
            var scaleX = cameraWidth / width;
            var scale = MathV.Max(scaleY, scaleX);
            var trans = renderer.transform;
            trans.position = Vector3.zero;
            trans.eulerAngles = Vector3.zero;
            trans.localScale = new Vector3(scale, scale, 1f);
        }

        public static bool IsClickDown(this Renderer renderer)
        {
            var cam = Camera.main;
            if (cam == null)
                return false;
            var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var pos = new Vector3(mousePosition.x, mousePosition.y, renderer.transform.position.z);
            var bounds = renderer.bounds;
            return bounds.Contains(pos);
        }

        public static bool IsClose(this Renderer renderer, Vector2 targetPos, float delta)
        {
            if (delta <= 0f)
                return false;
            var size = renderer.bounds.size;
            var deltaWidth = size.x * delta;
            var deltaHeight = size.y * delta;
            var pos = renderer.transform.position;
            var hor = pos.x.IsClamped(targetPos.x - deltaWidth, targetPos.x + deltaWidth);
            var vec = pos.y.IsClamped(targetPos.y - deltaHeight, targetPos.y + deltaHeight);
            return hor && vec;
        }
    }
}