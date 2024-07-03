//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    internal sealed partial class ResourceManager
    {
        private static T LoadCompleted<T>(T result) where T : Object => result is GameObject ? Object.Instantiate(result) : result;
    }
}