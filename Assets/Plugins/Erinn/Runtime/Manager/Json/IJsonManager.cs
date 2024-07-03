//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     JsonManager
    /// </summary>
    public interface IJsonManager
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Text</returns>
        string SerializeObject(object obj);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="text">Text</param>
        /// <returns>Element</returns>
        T DeserializeObject<T>(string text);

        /// <summary>
        ///     Save configuration data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="obj">Data</param>
        void SaveConfig(string name, object obj);

        /// <summary>
        ///     Save configuration data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="obj">Data</param>
        /// <param name="onComplete">Call upon completion</param>
        void SaveConfig(string name, object obj, Action onComplete);

        /// <summary>
        ///     Load configuration data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Did it load successfully</returns>
        bool LoadConfig<T>(string name, out T result);

        /// <summary>
        ///     Delete configuration data
        /// </summary>
        /// <param name="name">Name</param>
        void DeleteConfig(string name);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="obj">Data</param>
        void Save(string name, object obj);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="ob"></param>
        /// <param name="onComplete">Call upon completion</param>
        void Save(string name, object ob, Action onComplete);

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Did it load successfully</returns>
        bool Load<T>(string name, out T result);

        /// <summary>
        ///     Delete data
        /// </summary>
        /// <param name="name">Name</param>
        void Delete(string name);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        void SaveString(string name, string obj);

        /// <summary>
        ///     Storing data
        /// </summary>
        /// <param name="obj">Data saved</param>
        /// <param name="name">Save Name</param>
        /// <param name="onComplete">Call upon completion</param>
        void SaveString(string name, string obj, Action onComplete);

        /// <summary>
        ///     Load data
        /// </summary>
        /// <param name="name">The name of the loaded data</param>
        /// <param name="result">Result</param>
        /// <returns>Return loaded data</returns>
        bool LoadString(string name, out string result);
    }
}