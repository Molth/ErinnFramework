//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;
#if UNITY_2021_3_OR_NEWER
using System;
#endif

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Encryption base class
    /// </summary>
    public abstract class CryptoBase : IDisposable
    {
        /// <summary>
        ///     AesEncryptor
        /// </summary>
        protected Aes AesEncryptor;

        /// <summary>
        ///     HMACSHA256Encryptor
        /// </summary>
        protected HMACSHA256 Hmacsha256;

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose()
        {
            Release();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release
        /// </summary>
        ~CryptoBase() => Release();

        /// <summary>
        ///     Initialization
        /// </summary>
        /// <param name="aesSecret">AesKey</param>
        /// <param name="hmacSecret">HmacKey</param>
        public void Initialize(string aesSecret, string hmacSecret)
        {
#if !UNITY_2021_3_OR_NEWER
            var hash1 = SHA256.HashData(Encoding.UTF8.GetBytes(aesSecret));
            var hash2 = SHA256.HashData(Encoding.UTF8.GetBytes(hmacSecret));
#else
            byte[] hash1;
            byte[] hash2;
            using (var shA256 = SHA256.Create())
            {
                hash1 = shA256.ComputeHash(Encoding.UTF8.GetBytes(aesSecret));
                hash2 = shA256.ComputeHash(Encoding.UTF8.GetBytes(hmacSecret));
            }
#endif
            Initialize(hash1, hash2);
        }

        /// <summary>
        ///     Initialization
        /// </summary>
        /// <param name="aesSecret">AesKey</param>
        /// <param name="hmacSecret">HmacKey</param>
        public void Initialize(byte[] aesSecret, byte[] hmacSecret)
        {
            AesEncryptor = Aes.Create();
            AesEncryptor.Key = aesSecret;
            Hmacsha256 = new HMACSHA256(hmacSecret);
        }

        /// <summary>
        ///     Release
        /// </summary>
        private void Release()
        {
            if (AesEncryptor != null)
            {
                AesEncryptor.Dispose();
                AesEncryptor = null;
            }

            if (Hmacsha256 != null)
            {
                Hmacsha256.Dispose();
                Hmacsha256 = null;
            }
        }
    }
}