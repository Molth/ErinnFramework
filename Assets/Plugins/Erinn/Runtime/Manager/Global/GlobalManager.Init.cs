//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Global Manager
    /// </summary>
    public sealed partial class GlobalManager
    {
        /// <summary>
        ///     Initialize framework
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            //If the framework has already been initialized, return, Prevent multiple initialization of the framework
            if (Entry.IsRuntime)
                return;
            //Initialize framework module
            Entry.Init();
            //Initialize Framework Module Manager
            InitManager();
#if UNITY_EDITOR
            //Initialize debugging data
            if (Instance._enableConfig)
                _data = new GlobalData();
#endif
            //Do not delete objects
            DontDestroyOnLoad(Instance.gameObject);
        }

        /// <summary>
        ///     Initialize Framework Module Manager
        /// </summary>
        private static void InitManager()
        {
            //Initialize Data Table
            DataManager.ExcelTableInit();
            //InitializationUI
            UIManager.InitCanvasLayerGroup(Instance.transform.Find("UICanvas"));
            //Initialize audio
            SoundManager.SoundSourceInit(Instance.transform.Find("Sound").gameObject);
        }
    }
}