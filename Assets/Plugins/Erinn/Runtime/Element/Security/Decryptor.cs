//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Security.Cryptography;
#if UNITY_2021_3_OR_NEWER
using System;
using System.IO;
#endif

// ReSharper disable InconsistentNaming

#pragma warning disable CS8603
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Decryptor
    /// </summary>
    public sealed class Decryptor : CryptoBase
    {
        /// <summary>
        ///     Decryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Decrypted data</returns>
        public byte[] Decrypt(byte[] data) => Decrypt(data, 0, data.Length);

        /// <summary>
        ///     Decryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="length">Length</param>
        /// <returns>Decrypted data</returns>
        public byte[] Decrypt(byte[] data, int offset, int length)
        {
            if (!CheckHMACThreadSafe(data, offset, length))
                return null;
            return DecryptBufferWithIV(data, offset + 1, length - 32 - 1);
        }

        /// <summary>
        ///     Decryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="result">Decrypted data</param>
        /// <returns>Decryption successful</returns>
        public bool Decrypt(byte[] data, out byte[] result) => Decrypt(data, 0, data.Length, out result);

        /// <summary>
        ///     Decryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="length">Length</param>
        /// <param name="result">Decrypted data</param>
        /// <returns>Decryption successful</returns>
        public bool Decrypt(byte[] data, int offset, int length, out byte[] result)
        {
            if (!CheckHMACThreadSafe(data, offset, length))
            {
                result = null;
                return false;
            }

            result = DecryptBufferWithIV(data, offset + 1, length - 32 - 1);
            return true;
        }

        /// <summary>
        ///     Decryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="length">Length</param>
        /// <returns>Decrypted data</returns>
        public byte[] DecryptBufferWithIV(byte[] data, int offset, int length)
        {
            var dst = new byte[16];
            Buffer.BlockCopy(data, offset, dst, 0, 16);
            ICryptoTransform decryptor;
            lock (AesEncryptor)
            {
                AesEncryptor.IV = dst;
                decryptor = AesEncryptor.CreateDecryptor();
            }

            using (decryptor)
            {
                using (var memoryStream1 = new MemoryStream(data, offset + 16, length - 16))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream1, decryptor, CryptoStreamMode.Read))
                    {
                        using (var memoryStream2 = new MemoryStream(length - 16))
                        {
                            var buffer = new byte[16];
                            int count;
                            do
                            {
                                count = cryptoStream.Read(buffer, 0, 16);
                                if (count != 0)
                                    memoryStream2.Write(buffer, 0, count);
                            } while (count != 0);

                            return memoryStream2.ToArray();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     InspectHMAC
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>WhetherHMAC</returns>
        public bool CheckHMAC(byte[] data) => CheckHMAC(data, 0, data.Length);

        /// <summary>
        ///     InspectHMAC
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="length">Length</param>
        /// <returns>WhetherHMAC</returns>
        public bool CheckHMAC(byte[] data, int offset, int length)
        {
            var hash = Hmacsha256.ComputeHash(data, offset, length - 32);
            var flag = true;
            for (var index1 = 0; (index1 < 4) & flag; ++index1)
            {
                var index2 = offset + length - 32 + index1 * 8;
                var index3 = index1 * 8;
                flag = data[index2] == hash[index3] && data[index2 + 1] == hash[index3 + 1] && data[index2 + 2] == hash[index3 + 2] && data[index2 + 3] == hash[index3 + 3] && data[index2 + 4] == hash[index3 + 4] && data[index2 + 5] == hash[index3 + 5] && data[index2 + 6] == hash[index3 + 6] && data[index2 + 7] == hash[index3 + 7];
            }

            return flag;
        }

        /// <summary>
        ///     InspectHMACThread Safe
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>WhetherHMAC</returns>
        public bool CheckHMACThreadSafe(byte[] data) => CheckHMACThreadSafe(data, 0, data.Length);

        /// <summary>
        ///     InspectHMACThread Safe
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="length">Length</param>
        /// <returns>WhetherHMAC</returns>
        public bool CheckHMACThreadSafe(byte[] data, int offset, int length)
        {
            byte[] hash;
            lock (Hmacsha256)
                hash = Hmacsha256.ComputeHash(data, offset, length - 32);
            var flag = true;
            for (var index1 = 0; (index1 < 4) & flag; ++index1)
            {
                var index2 = offset + length - 32 + index1 * 8;
                var index3 = index1 * 8;
                flag = data[index2] == hash[index3] && data[index2 + 1] == hash[index3 + 1] && data[index2 + 2] == hash[index3 + 2] && data[index2 + 3] == hash[index3 + 3] && data[index2 + 4] == hash[index3 + 4] && data[index2 + 5] == hash[index3 + 5] && data[index2 + 6] == hash[index3 + 6] && data[index2 + 7] == hash[index3 + 7];
            }

            return flag;
        }
    }
}