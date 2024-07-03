//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static Quaternion Append(this Quaternion source, Quaternion quaternion) => quaternion * source;

        public static Quaternion FromToRotation(this Quaternion source, Quaternion target) => Quaternion.Inverse(source) * target;

        public static Quaternion Scale(this Quaternion source, float scale) => Quaternion.SlerpUnclamped(Quaternion.identity, source, scale);

        public static Quaternion Inverse(this Quaternion source) => Quaternion.Inverse(source);
    }
}