//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static bool Contains(this Rect rect, float x, float y) => x > (double)rect.xMin && x < (double)rect.xMax && y > (double)rect.yMin && y < (double)rect.yMax;

        public static bool Contains(this Rect rect, Vector2 point, bool collide) => collide ? rect.xMin <= (double)point.x && rect.yMin <= (double)point.y && rect.xMax >= (double)point.x && rect.yMax >= (double)point.y : rect.xMin < (double)point.x && rect.yMin < (double)point.y && rect.xMax > (double)point.x && rect.yMax > (double)point.y;

        public static bool Contains(this Rect rect1, Rect rect2) => rect1.xMin <= (double)rect2.xMin && rect1.yMin <= (double)rect2.yMin && rect1.xMax >= (double)rect2.xMax && rect1.yMax >= (double)rect2.yMax;

        public static bool Contains(this Rect rect1, Rect rect2, bool collide) => collide ? rect1.xMin <= (double)rect2.xMin && rect1.yMin <= (double)rect2.yMin && rect1.xMax >= (double)rect2.xMax && rect1.yMax >= (double)rect2.yMax : rect1.xMin < (double)rect2.xMin && rect1.yMin < (double)rect2.yMin && rect1.xMax > (double)rect2.xMax && rect1.yMax > (double)rect2.yMax;

        public static bool IntersectsWith(this Rect rect1, Rect rect2) => rect2.xMin <= (double)rect1.xMax && rect2.xMax >= (double)rect1.xMin && rect2.yMin <= (double)rect1.yMax && rect2.yMax >= (double)rect1.yMin;

        public static Rect Union(this Rect rect1, Rect rect2) => Rect.MinMaxRect(MathV.Min(rect1.xMin, rect2.xMin), MathV.Min(rect1.yMin, rect2.yMin), MathV.Max(rect1.xMax, rect2.xMax), MathV.Max(rect1.yMax, rect2.yMax));

        public static Rect MinSize(this Rect rect, float minWidth, float minHeight) => new(rect.x, rect.y, MathV.Max(rect.width, minWidth), MathV.Max(rect.height, minHeight));

        public static Rect MinSize(this Rect rect, Vector2 minSize) => new(rect.x, rect.y, MathV.Max(rect.width, minSize.x), MathV.Max(rect.height, minSize.y));

        public static Vector2 MinCorner(this Rect rect) => new(rect.xMin, rect.yMin);

        public static Vector2 MaxCorner(this Rect rect) => new(rect.xMax, rect.yMax);

        public static Rect Scale(this Rect rect, float scale) => new(rect.xMin * scale, rect.yMin * scale, rect.width * scale, rect.height * scale);

        public static Rect Scale(this Rect rect, Vector2 scale) => new(rect.xMin * scale.x, rect.yMin * scale.y, rect.width * scale.x, rect.height * scale.y);

        public static Rect Translate(this Rect rect, Vector2 translateVector) => new(rect.position + translateVector, rect.size);

        public static Rect FromCenterSize(Vector2 center, Vector2 size) => new(center.x - size.x / 2f, center.y - size.y / 2f, size.x, size.y);

        public static Rect InPlaceScale(this Rect rect, float scale) => new(rect.xMin, rect.yMin, rect.width * scale, rect.height * scale);

        public static Rect InPlaceScale(this Rect rect, float newWidth, float newHeight)
        {
            var num1 = newWidth - rect.width;
            var num2 = newHeight - rect.height;
            rect.xMin -= num1 / 2f;
            rect.yMin -= num2 / 2f;
            rect.width = newWidth;
            rect.height = newHeight;
            return rect;
        }

        public static Vector2 Clamp(this Rect area, Vector2 position) => new(MathV.Max(MathV.Min(position.x, area.xMax), area.xMin), MathV.Max(MathV.Min(position.y, area.yMax), area.yMin));

        public static float GetSize(this Rect rect) => rect.width * rect.height;
    }
}