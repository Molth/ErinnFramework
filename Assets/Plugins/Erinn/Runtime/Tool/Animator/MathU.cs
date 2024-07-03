//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Animator Function Library
    /// </summary>
    public readonly ref struct MathU
    {
        /// <summary>
        ///     Get Animator Hash Dictionary
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained Animator Hash Dictionary</returns>
        public static Dictionary<int, int> ToAnimatorHashDictionary<T>() where T : struct, Enum
        {
            var array = Enum.GetValues(typeof(T));
            var dictionary = new Dictionary<int, int>(array.Length);
            foreach (var value in array)
                dictionary.Add((int)value, Animator.StringToHash(value.ToString()));
            return dictionary;
        }

        /// <summary>
        ///     Get Animator enumeration hash dictionary
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained animator enumeration hash dictionary</returns>
        public static Dictionary<T, int> ToAnimatorEnumHashDictionary<T>() where T : struct, Enum
        {
            var array = Enum.GetValues(typeof(T));
            var dictionary = new Dictionary<T, int>(array.Length);
            foreach (var value in array)
                dictionary.Add((T)value, Animator.StringToHash(value.ToString()));
            return dictionary;
        }
    }
}