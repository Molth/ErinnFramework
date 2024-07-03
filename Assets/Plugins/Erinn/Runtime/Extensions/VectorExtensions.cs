//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static Vector3 Flip(this Vector3 t) => new(-1f * t.x, t.y, t.z);

        public static Vector3 FlipXY(this Vector3 t) => new(-1f * t.x, -1f * t.y, t.z);

        public static Vector3 Rotate(this Vector3 v, float degrees)
        {
            var num1 = MathV.Sin(degrees * ((float)Math.PI / 180f));
            var num2 = MathV.Cos(degrees * ((float)Math.PI / 180f));
            var x = v.x;
            var y = v.y;
            v.x = (float)(num2 * (double)x - num1 * (double)y);
            v.y = (float)(num1 * (double)x + num2 * (double)y);
            return v;
        }

        public static int Sign(this float origin, IEnumerable<float> points)
        {
            var foundClosest = false;
            var closestDistance = 0f;
            var closestPoint = 0f;
            foreach (var point in points)
            {
                var distance = MathV.Abs(point - origin);
                if (!foundClosest)
                {
                    foundClosest = true;
                    closestDistance = distance;
                    closestPoint = point;
                    continue;
                }

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }

            return MathV.Sign(closestPoint - origin);
        }

        public static bool Sign(this float origin, IEnumerable<float> points, out int sign)
        {
            var foundClosest = false;
            var closestDistance = 0f;
            var closestPoint = 0f;
            foreach (var point in points)
            {
                var distance = MathV.Abs(point - origin);
                if (!foundClosest)
                {
                    foundClosest = true;
                    closestDistance = distance;
                    closestPoint = point;
                    continue;
                }

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }

            sign = MathV.Sign(closestPoint - origin);
            return sign != 0;
        }

        public static Vector3 ClampPositionX(this Vector3 t, float min, float max)
        {
            t.x = MathV.Clamp(t.x, min, max);
            return t;
        }

        public static Vector3 ClampPositionY(this Vector3 t, float min, float max)
        {
            t.y = MathV.Clamp(t.y, min, max);
            return t;
        }

        public static Vector3 ClampPositionZ(this Vector3 t, float min, float max)
        {
            t.z = MathV.Clamp(t.z, min, max);
            return t;
        }

        public static Vector3 CirclePosition(this Vector3 center, float radius, float angle)
        {
            var f = (float)(angle * Math.PI / 180.0);
            return center + new Vector3(radius * Mathf.Cos(f), 0.0f, radius * Mathf.Sin(f));
        }

        public static Vector3 ClosestCirclePoint(this Vector3 t, Vector3 center, float radius)
        {
            var dir = (t - center).normalized;
            return center + dir * radius;
        }

        public static List<Vector3> GetOrthogonalVectors(this Vector3 source, int numVectors)
        {
            var vector3 = Math.Abs(Math.Abs(source.normalized.y) - 1.0) > 0.0 ? Vector3.Cross(source, Vector3.up) : Vector3.Cross(source, Vector3.right);
            var num = 360.0 / numVectors;
            var orthogonalVectors = new List<Vector3>();
            for (var index = 0; index < numVectors; ++index)
                orthogonalVectors.Add(Quaternion.AngleAxis((float)(num * index), source) * vector3);
            return orthogonalVectors;
        }

        public static bool HasLength(this Vector3 source) => source.x != 0.0 || source.y != 0.0 || source.z != 0.0;
    }
}