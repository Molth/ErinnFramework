//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Erinn
{
    /// <summary>
    ///     Global data
    /// </summary>
    internal sealed class GlobalData
    {
        [ShowInInspector]
        [FoldoutGroup("Allocation")]
        [LabelText("Time scaling")]
        private static float TimeScale => Time.timeScale;

        [ShowInInspector]
        [FoldoutGroup("Polling ")]
        [LabelText("Update")]
        private static ActionContainer OnUpdate => GlobalManager.OnUpdate;

        [ShowInInspector]
        [FoldoutGroup("Polling ")]
        [LabelText("LateUpdate")]
        private static ActionContainer OnLateUpdate => GlobalManager.OnLateUpdate;

        [ShowInInspector]
        [FoldoutGroup("Polling ")]
        [LabelText("FixedUpdate")]
        private static ActionContainer OnFixedUpdate => GlobalManager.OnFixedUpdate;

        [ShowInInspector]
        [FoldoutGroup("Object Pooling")]
        [LabelText("Any type")]
        private static Dictionary<Type, IPool> PoolDict => PoolManager.PoolDict;

        [ShowInInspector]
        [FoldoutGroup("Object Pooling")]
        [LabelText("Object")]
        private static Dictionary<string, HashPool<GameObject>> GameObjectPoolDict => PoolManager.GameObjectPoolDict;

        [ShowInInspector]
        [FoldoutGroup("Resource")]
        [LabelText("Cache Path")]
        private static Dictionary<string, AsyncOperationHandle>.KeyCollection Assets => ResourceManager.Assets;

        [ShowInInspector]
        [FoldoutGroup("Resource")]
        [LabelText("Available Paths")]
        private static Dictionary<string, HashSet<string>> PathDict => ResourceManager.PathDict;

        [ShowInInspector]
        [FoldoutGroup("Data")]
        [LabelText("Int")]
        private static Dictionary<Type, Dictionary<int, IExcelDataBase>> IntDataDict => DataManager.IntData;

        [ShowInInspector]
        [FoldoutGroup("Data")]
        [LabelText("String")]
        private static Dictionary<Type, Dictionary<string, IExcelDataBase>> StrDataDict => DataManager.StrData;

        [ShowInInspector]
        [FoldoutGroup("Data")]
        [LabelText("Enum")]
        private static Dictionary<Type, Dictionary<Enum, IExcelDataBase>> EnumDataDict => DataManager.EnumData;

        [ShowInInspector]
        [FoldoutGroup("Audio frequency")]
        [LabelText("Set up")]
        private static AudioSetting AudioManagerSetting => SoundManager.Setting;

        [ShowInInspector]
        [FoldoutGroup("Command")]
        [LabelText("Type&Pool")]
        private static Dictionary<Type, CommandPool> CommandDict => CommandManager.CommandDict;

        [ShowInInspector]
        [FoldoutGroup("Observer events")]
        [LabelText("Type")]
        private static Dictionary<Type, ObserverContainer> ObserverDict => EventManager.ObserverDict;

        [ShowInInspector]
        [FoldoutGroup("Ordinary events")]
        [LabelText("Key")]
        private static Dictionary<string, EventContainer> EventTable => EventManager.EventTable;

        [ShowInInspector]
        [FoldoutGroup("UI")]
        [LabelText("Type&Panel")]
        private static Dictionary<Type, UIBase> UIDict => UIManager.UIDict;
    }
}
#endif