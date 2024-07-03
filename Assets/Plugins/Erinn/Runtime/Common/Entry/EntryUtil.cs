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
        ///     Get framework module
        ///     Adopting lazy loading mode
        ///     If unable to obtain, return empty directly, Ensure thread safety
        /// </summary>
        /// <typeparam name="T">The type of framework module to obtain</typeparam>
        /// <returns>The framework module to be obtained</returns>
        /// <remarks>If the framework module to be obtained does not exist, Then return empty</remarks>
        private static T GetModule<T>() where T : ModuleSingleton, new()
        {
            var moduleType = typeof(T);
            for (var current = ModuleSingletons.First; current != null; current = current.Next)
                if (current.Value.GetType() == moduleType)
                    return (T)current.Value;
            return null;
        }

        /// <summary>
        ///     Get framework module(Using interfaces, Instead of directly calling, Ensure data security)
        /// </summary>
        /// <typeparam name="T">The type of framework module to obtain</typeparam>
        /// <returns>The framework module to be obtained</returns>
        /// <remarks>If the framework module to be obtained does not exist, Then automatically create the framework module</remarks>
        private static void InitModule<T>() where T : ModuleSingleton, new()
        {
            var moduleType = typeof(T);
            foreach (var module in ModuleSingletons)
                if (module.GetType() == moduleType)
                    return;
            CreateModule<T>();
        }

        /// <summary>
        ///     Create framework module
        ///     Initialize modules with higher priority first
        /// </summary>
        /// <typeparam name="T">The type of framework module to be created</typeparam>
        /// <returns>The framework module to be created</returns>
        private static void CreateModule<T>() where T : ModuleSingleton, new()
        {
            var module = new T();
            var current = ModuleSingletons.First;
            while (current != null)
            {
                if (module.Priority > current.Value.Priority)
                    break;
                current = current.Next;
            }

            if (current != null)
                ModuleSingletons.AddBefore(current, module);
            else
                ModuleSingletons.AddLast(module);
            module.OnInit();
        }
    }
}