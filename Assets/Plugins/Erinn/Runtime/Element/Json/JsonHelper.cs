//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Erinn
{
    /// <summary>
    ///     JsonConversion auxiliary device
    /// </summary>
    internal sealed partial class JsonHelper : JsonConverter
    {
        /// <summary>
        ///     Read JsonData
        /// </summary>
        /// <param name="reader">Reader </param>
        /// <param name="objectType">Type</param>
        /// <param name="existingValue">Value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>Data read</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!JsonHelperSettings.ContainsType(objectType))
                return null;
            var data = serializer.Deserialize(reader);
            return data != null ? JsonConvert.DeserializeObject(data.ToString(), objectType) : null;
        }

        /// <summary>
        ///     Write JsonData
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                return;
            var type = value.GetType();
            var action = JsonHelperSettings.GetWriterParse(type);
            if (action != null)
            {
                writer.WriteStartObject();
                action.Invoke(writer, value);
                writer.WriteEndObject();
            }
        }
    }
}