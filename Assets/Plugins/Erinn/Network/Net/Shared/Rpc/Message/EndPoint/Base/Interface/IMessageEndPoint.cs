//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System.Threading.Tasks;
#endif

namespace Erinn
{
    /// <summary>
    ///     Message endPoint interface
    /// </summary>
    public interface IMessageEndPoint
    {
        /// <summary>
        ///     Acquire message async
        /// </summary>
        /// <param name="timeout">Timeout</param>
        /// <param name="serialNumber">SerialNumber</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Message</returns>
        Task<MessageResult<T>> AcquireAsync<T>(int timeout, out uint serialNumber);

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        bool Checked(uint serialNumber);

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="serialNumber">SerialNumber</param>
        /// <param name="obj">object</param>
        bool SetResult(uint serialNumber, object obj);

        /// <summary>
        ///     Clear
        /// </summary>
        void Clear();
    }
}