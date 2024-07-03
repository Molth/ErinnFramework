//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     JsonManager
    /// </summary>
    internal sealed partial class JsonManager : ModuleSingleton, IJsonManager
    {
        public override int Priority => 7;

        public override void OnDispose()
        {
            _folder = null;
            _configFolder = null;
            CancelSourceDict.Clear();
        }
    }
}