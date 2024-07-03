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

namespace Erinn
{
    /// <summary>
    ///     Encryptor
    /// </summary>
    public sealed class Encryptor : CryptoBase
    {
        /// <summary>
        ///     Encryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Encrypted data</returns>
        public byte[] Encrypt(byte[] data)
        {
            var numArray = new byte[data.Length + 16 + 32 + (16 - data.Length % 16) + 1];
            numArray[0] = 2;
            var offset = 1;
            Encrypt(data, data.Length, numArray, ref offset);
            var src = FinishHMACThreadSafe(numArray, 0, offset);
            Buffer.BlockCopy(src, 0, numArray, offset, src.Length);
            return numArray;
        }

        /// <summary>
        ///     Encryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="srcOffset">Deviation</param>
        /// <param name="length">Length</param>
        /// <param name="output">Output</param>
        /// <param name="outputOffset">Output offset</param>
        public void Encrypt(byte[] data, int srcOffset, int length, byte[] output, ref int outputOffset)
        {
            byte[] iv;
            ICryptoTransform encryptor;
            lock (AesEncryptor)
            {
                AesEncryptor.GenerateIV();
                iv = AesEncryptor.IV;
                encryptor = AesEncryptor.CreateEncryptor();
            }

            using (encryptor)
            {
                using (var memoryStream = new MemoryStream(output, outputOffset, output.Length - outputOffset))
                {
                    memoryStream.Write(iv, 0, iv.Length);
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, srcOffset, length);
                        cryptoStream.FlushFinalBlock();
                        outputOffset += (int)memoryStream.Position;
                    }
                }
            }
        }

        /// <summary>
        ///     Encryption
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="length">Length</param>
        /// <param name="output">Output</param>
        /// <param name="offset">Deviation</param>
        public void Encrypt(byte[] data, int length, byte[] output, ref int offset) => Encrypt(data, 0, length, output, ref offset);

        /// <summary>
        ///     CompleteHMAC
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Capacity</param>
        /// <returns>Data</returns>
        public byte[] FinishHMAC(byte[] data, int offset, int count) => Hmacsha256.ComputeHash(data, offset, count);

        /// <summary>
        ///     CompleteHMACThread Safe
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="offset">Deviation</param>
        /// <param name="count">Capacity</param>
        /// <returns>Data</returns>
        public byte[] FinishHMACThreadSafe(byte[] data, int offset, int count)
        {
            lock (Hmacsha256)
                return Hmacsha256.ComputeHash(data, offset, count);
        }

        /// <summary>
        ///     Obtain encrypted data capacity
        /// </summary>
        /// <param name="dataLength"></param>
        /// <returns>Obtained encrypted data capacity</returns>
        public static int EncryptedDataSize(int dataLength)
        {
            var num = dataLength % 16;
            return num == 0 ? 32 : dataLength + (16 - num);
        }

        /// <summary>
        ///     Obtain encrypted data capacity
        /// </summary>
        /// <param name="dataLength"></param>
        /// <returns>Obtained encrypted data capacity</returns>
        public static int EncryptedDataSizeWithIV(int dataLength) => EncryptedDataSize(dataLength) + 16;
    }
}