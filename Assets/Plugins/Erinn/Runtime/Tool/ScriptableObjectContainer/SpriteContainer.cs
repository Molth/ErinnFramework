//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Elf container
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectContainers.MenuName + nameof(SpriteContainer), fileName = nameof(SpriteContainer), order = 0)]
    public sealed class SpriteContainer : ScriptableObjectContainer<Sprite>
    {
    }
}