//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Security.Cryptography;
#if UNITY_2021_3_OR_NEWER
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Encryptor and Decryptor
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        ///     Encryptor
        /// </summary>
        private static readonly Encryptor CryptoEncryptor = new();

        /// <summary>
        ///     Decryptor
        /// </summary>
        private static readonly Decryptor CryptoDecryptor = new();

        /// <summary>
        ///     Random number generator
        /// </summary>
        private static readonly RandomNumberGenerator CryptoRandom = RandomNumberGenerator.Create();

        /// <summary>
        ///     Random number generator buffer
        /// </summary>
        private static readonly byte[] CryptoRandomBuffer = new byte[4];

        /// <summary>
        ///     Structure
        /// </summary>
        static Crypto() => Initialize("Molth", "Nevin");

        /// <summary>
        ///     Initialization
        /// </summary>
        /// <param name="aesSecret">AesKey</param>
        /// <param name="hmacSecret">HmacKey</param>
        public static void Initialize(string aesSecret, string hmacSecret)
        {
            CryptoEncryptor.Initialize(aesSecret, hmacSecret);
            CryptoDecryptor.Initialize(aesSecret, hmacSecret);
        }

        /// <summary>
        ///     Encryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Encrypted data</returns>
        public static byte[] Encrypt(byte[] data) => CryptoEncryptor.Encrypt(data);

        /// <summary>
        ///     Decryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Decrypted data</returns>
        public static byte[] Decrypt(byte[] data) => CryptoDecryptor.Decrypt(data);

        /// <summary>
        ///     GenerateCookie
        /// </summary>
        public static uint GenerateCookie()
        {
            CryptoRandom.GetBytes(CryptoRandomBuffer);
            return BitConverter.ToUInt32(CryptoRandomBuffer, 0);
        }
    }
}