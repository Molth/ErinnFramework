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
        ///     Resource Manager
        /// </summary>
        private static IResourceManager _resource;

        /// <summary>
        ///     Data Manager
        /// </summary>
        private static IDataManager _data;

        /// <summary>
        ///     JsonManager
        /// </summary>
        private static IJsonManager _json;

        /// <summary>
        ///     ByteManager
        /// </summary>
        private static IByteManager _byte;

        /// <summary>
        ///     Command Manager
        /// </summary>
        private static ICommandManager _command;

        /// <summary>
        ///     UIManager
        /// </summary>
        private static IUIManager _ui;

        /// <summary>
        ///     Audio Manager
        /// </summary>
        private static ISoundManager _sound;

        /// <summary>
        ///     Timer Manager
        /// </summary>
        private static ITimerManager _timer;

        /// <summary>
        ///     Object Pool Manager
        /// </summary>
        private static IPoolManager _pool;

        /// <summary>
        ///     Scene Manager
        /// </summary>
        private static ISceneManager _scene;

        /// <summary>
        ///     Event Manager
        /// </summary>
        private static IEventManager _event;

        /// <summary>
        ///     Resource Manager
        /// </summary>
        public static IResourceManager Resource => _resource ??= GetModule<ResourceManager>();

        /// <summary>
        ///     Data Manager
        /// </summary>
        public static IDataManager Data => _data ??= GetModule<DataManager>();

        /// <summary>
        ///     JsonManager
        /// </summary>
        public static IJsonManager Json => _json ??= GetModule<JsonManager>();

        /// <summary>
        ///     BytesManager
        /// </summary>
        public static IByteManager Bytes => _byte ??= GetModule<BytesManager>();

        /// <summary>
        ///     Command Manager
        /// </summary>
        public static ICommandManager Command => _command ??= GetModule<CommandManager>();

        /// <summary>
        ///     UIManager
        /// </summary>
        public static IUIManager UI => _ui ??= GetModule<UIManager>();

        /// <summary>
        ///     Audio Manager
        /// </summary>
        public static ISoundManager Sound => _sound ??= GetModule<SoundManager>();

        /// <summary>
        ///     Timer Manager
        /// </summary>
        public static ITimerManager Timer => _timer ??= GetModule<TimerManager>();

        /// <summary>
        ///     Object Pool Manager
        /// </summary>
        public static IPoolManager Pool => _pool ??= GetModule<PoolManager>();

        /// <summary>
        ///     Scene Manager
        /// </summary>
        public static ISceneManager Scene => _scene ??= GetModule<SceneManager>();

        /// <summary>
        ///     Event Manager
        /// </summary>
        public static IEventManager Event => _event ??= GetModule<EventManager>();
    }
}