//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Erinn
{
    /// <summary>
    ///     Resource Manager
    /// </summary>
    internal sealed partial class ResourceManager : ModuleSingleton, IResourceManager
    {
        public override int Priority => 10;

        public static Dictionary<string, AsyncOperationHandle>.KeyCollection Assets => AssetDict.Keys;
    }
}