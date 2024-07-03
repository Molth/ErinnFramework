//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Json Encrypted data
    /// </summary>
    [Serializable]
    internal struct JsonData
    {
        /// <summary>
        ///     Vector of encrypted data
        /// </summary>
        public byte[] Iv;

        /// <summary>
        ///     Key values for encrypted data
        /// </summary>
        public byte[] Key;

        /// <summary>
        ///     Generate new Json Encrypted data
        /// </summary>
        /// <returns>New generated Json Encrypted data</returns>
        public static JsonData Create() => new();

        /// <summary>
        ///     Generate new Json Encrypted data
        /// </summary>
        /// <param name="key">Secret key</param>
        /// <param name="iv">Vector</param>
        public static JsonData Create(byte[] key, byte[] iv)
        {
            var jsonData = new JsonData
            {
                Key = key,
                Iv = iv
            };
            return jsonData;
        }

        public bool IsEmpty()
        {
            if (Key == null)
                return true;
            if (Key.Length == 0)
                return true;
            if (Iv == null)
                return true;
            if (Iv.Length == 0)
                return true;
            return false;
        }
    }
}