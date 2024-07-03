//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_EDITOR
//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Global Manager
    /// </summary>
    public sealed partial class GlobalManager
    {
        [HideIf("@_data == null")] [ShowInInspector] [ReadOnly] [LabelText("Data")]
        private static GlobalData _data;

        [HideInPlayMode] [SerializeField] [LabelText("Enable debugging")]
        private bool _enableConfig = true;
    }
}
#endif