//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Framework entrance
    /// </summary>
    public static partial class Entry
    {
        /// <summary>
        ///     Initialize framework
        /// </summary>
        internal static void Init()
        {
            InitModule();
            IsRuntime = true;
        }

        /// <summary>
        ///     Module initialization, Priority loading of high priority modules
        /// </summary>
        private static void InitModule()
        {
            InitModule<ResourceManager>();
            InitModule<DataManager>();
            InitModule<JsonManager>();
            InitModule<BytesManager>();
            InitModule<CommandManager>();
            InitModule<UIManager>();
            InitModule<SoundManager>();
            InitModule<TimerManager>();
            InitModule<PoolManager>();
            InitModule<SceneManager>();
            InitModule<EventManager>();
        }

        /// <summary>
        ///     Close and clean all framework modules
        /// </summary>
        internal static void ClearAll()
        {
            IsRuntime = false;
            for (var current = ModuleSingletons.Last; current != null; current = current.Previous)
                current.Value.OnDispose();
            ModuleSingletons.Clear();
            ReferencePool.ClearAll();
        }
    }
}