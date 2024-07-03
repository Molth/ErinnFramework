//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
#if UNITY_2021_3_OR_NEWER
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Reference Pool
    /// </summary>
    public static class ReferencePool
    {
        /// <summary>
        ///     Reference Pool Dictionary
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ReferenceCollection> ReferenceDict = new();

        /// <summary>
        ///     Obtain the number of reference pools
        /// </summary>
        public static int Count => ReferenceDict.Count;

        /// <summary>
        ///     Clear all reference pools
        /// </summary>
        public static void ClearAll()
        {
            foreach (var referenceCollection in ReferenceDict)
                referenceCollection.Value.RemoveAll();
            ReferenceDict.Clear();
        }

        /// <summary>
        ///     Get references from reference pool
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <returns>Quote</returns>
        public static T Acquire<T>() where T : class, IReference, new() => GetReferenceCollection(typeof(T)).Acquire<T>();

        /// <summary>
        ///     Get references from reference pool
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        /// <returns>Quote</returns>
        public static IReference Acquire(Type referenceType) => GetReferenceCollection(referenceType).Acquire();

        /// <summary>
        ///     Returning references to the reference pool
        /// </summary>
        /// <param name="reference">Quote</param>
        public static void Release(IReference reference)
        {
            if (!Entry.IsRuntime)
                return;
            if (reference == null)
                return;
            GetReferenceCollection(reference.GetType()).Release(reference);
        }

        /// <summary>
        ///     Returning references to the reference pool
        /// </summary>
        /// <param name="reference">Quote</param>
        public static void Release<T>(T reference) where T : class, IReference, new()
        {
            if (!Entry.IsRuntime)
                return;
            if (reference == null)
                return;
            GetReferenceCollection(typeof(T)).Release(reference);
        }

        /// <summary>
        ///     Returning references to the reference pool
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        /// <param name="reference">Quote</param>
        public static void Release(Type referenceType, IReference reference)
        {
            if (reference == null)
                return;
            GetReferenceCollection(referenceType).Release(reference);
        }

        /// <summary>
        ///     Append a specified number of references to the reference pool
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <param name="count">Additional quantity</param>
        public static void Add<T>(int count) where T : class, IReference, new() => GetReferenceCollection(typeof(T)).Add<T>(count);

        /// <summary>
        ///     Append a specified number of references to the reference pool
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        /// <param name="count">Additional quantity</param>
        public static void Add(Type referenceType, int count) => GetReferenceCollection(referenceType).Add(count);

        /// <summary>
        ///     Remove a specified number of references from the reference pool
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <param name="count">Remove quantity</param>
        public static void Remove<T>(int count) where T : class, IReference => GetReferenceCollection(typeof(T)).Remove(count);

        /// <summary>
        ///     Remove a specified number of references from the reference pool
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        /// <param name="count">Remove quantity</param>
        public static void Remove(Type referenceType, int count) => GetReferenceCollection(referenceType).Remove(count);

        /// <summary>
        ///     Remove all references from the reference pool
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        public static void RemoveAll<T>() where T : class, IReference => GetReferenceCollection(typeof(T)).RemoveAll();

        /// <summary>
        ///     Remove all references from the reference pool
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        public static void RemoveAll(Type referenceType) => GetReferenceCollection(referenceType).RemoveAll();

        /// <summary>
        ///     Get the corresponding reference pool by type
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        private static ReferenceCollection GetReferenceCollection(Type referenceType)
        {
            if (ReferenceDict.TryGetValue(referenceType, out var referenceCollection))
                return referenceCollection;
            referenceCollection = new ReferenceCollection(referenceType);
            ReferenceDict[referenceType] = referenceCollection;
            return referenceCollection;
        }
    }
}