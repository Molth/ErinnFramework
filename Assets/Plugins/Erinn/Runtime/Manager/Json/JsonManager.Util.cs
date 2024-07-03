//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace Erinn
{
    internal sealed partial class JsonManager
    {
        /// <summary>
        ///     Converter Helper
        /// </summary>
        private static readonly JsonHelper JsonConvertHelper = new();

        /// <summary>
        ///     Cancel Dictionary
        /// </summary>
        private static readonly Dictionary<string, CancellationTokenSource> CancelSourceDict = new();

        string IJsonManager.SerializeObject(object obj) => SerializeObject(obj);

        T IJsonManager.DeserializeObject<T>(string text) => DeserializeObject<T>(text);

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Text</returns>
        public static string SerializeObject(object obj) => JsonConvert.SerializeObject(obj, JsonConvertHelper);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="text">Text</param>
        /// <returns>Element</returns>
        public static T DeserializeObject<T>(string text) => JsonConvert.DeserializeObject<T>(text, JsonConvertHelper);

        /// <summary>
        ///     Format Path
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Formatted path</returns>
        private static string FormatName(string name) => name.Replace("/", "").Replace("\\", "");

        /// <summary>
        ///     Asynchronous writing of text
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="content">Text</param>
        /// <returns>Task</returns>
        private static async UniTask WriteTextAsync(string path, string content)
        {
            try
            {
                if (CancelSourceDict.TryGetValue(path, out var source))
                {
                    source?.Cancel();
                    CancelSourceDict.Remove(path);
                }

                using var cancelSource = new CancellationTokenSource();
                CancelSourceDict[path] = source;
                await File.WriteAllTextAsync(path, content, cancelSource.Token);
            }
            catch
            {
                await UniTask.Yield();
            }
            finally
            {
                CancelSourceDict.Remove(path);
            }
        }
    }
}