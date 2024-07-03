//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    internal sealed partial class JsonHelper
    {
        /// <summary>
        ///     Readable
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        ///     Writable
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        ///     Determine if it contains a type
        /// </summary>
        /// <param name="objectType">Type</param>
        /// <returns>Can it be converted</returns>
        public override bool CanConvert(Type objectType) => JsonHelperSettings.ContainsType(objectType);
    }
}