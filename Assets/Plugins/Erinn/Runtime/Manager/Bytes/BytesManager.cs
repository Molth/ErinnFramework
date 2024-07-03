//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Bytes Manager
    /// </summary>
    internal sealed partial class BytesManager : ModuleSingleton, IByteManager
    {
        public override int Priority => 8;

        public override void OnDispose()
        {
            _folder = null;
            CancelSourceDict.Clear();
        }
    }
}