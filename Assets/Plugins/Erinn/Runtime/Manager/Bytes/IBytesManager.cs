//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     ByteManager
    /// </summary>
    public interface IByteManager
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Text</returns>
        byte[] Serialize<T>(T obj);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Text</returns>
        byte[] SerializeObject(object obj);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="obj">object</param>
        /// <returns>Text</returns>
        byte[] SerializeObject(Type type, object obj);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="text">Text</param>
        /// <returns>Element</returns>
        T Deserialize<T>(byte[] text);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="text">Text</param>
        /// <returns>Element</returns>
        object DeserializeObject(Type type, byte[] text);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        void Save<T>(string name, T obj);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        void Save<T>(string name, T obj, Action onComplete);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="obj">Data</param>
        void SaveObject(string name, object obj);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="ob"></param>
        /// <param name="onComplete">Call upon completion</param>
        void SaveObject(string name, object ob, Action onComplete);

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Did it load successfully</returns>
        bool Load<T>(string name, out T result);

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="type">Type</param>
        /// <param name="result">Result</param>
        /// <returns>Did it load successfully</returns>
        bool LoadObject(string name, Type type, out object result);

        /// <summary>
        ///     Delete data
        /// </summary>
        /// <param name="name">Name</param>
        void Delete(string name);
    }
}