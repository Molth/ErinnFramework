//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Erinn
{
    /// <summary>
    ///     Unity loop
    /// </summary>
    public static class EngineLoop
    {
        /// <summary>
        ///     Add to PlayerLoop
        /// </summary>
        public static void AddToPlayerLoop(PlayerLoopSystem.UpdateFunction action)
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            AddToPlayerLoop(action, typeof(EngineLoop), ref playerLoop, typeof(Update));
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        /// <summary>
        ///     Remove to PlayerLoop
        /// </summary>
        public static void RemoveToPlayerLoop(PlayerLoopSystem.UpdateFunction action)
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            RemoveToPlayerLoop(action, typeof(EngineLoop), ref playerLoop, typeof(Update));
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        /// <summary>
        ///     Add to PlayerLoop
        /// </summary>
        private static bool AddToPlayerLoop(PlayerLoopSystem.UpdateFunction function, Type ownerType, ref PlayerLoopSystem playerLoop, Type playerLoopSystemType)
        {
            if (playerLoop.type == playerLoopSystemType)
            {
                if (Array.FindIndex(playerLoop.subSystemList, s => s.updateDelegate == function) != -1)
                    return true;
                playerLoop.subSystemList ??= Array.Empty<PlayerLoopSystem>();
                var system = new PlayerLoopSystem { type = ownerType, updateDelegate = function };
                var tempQualifier = playerLoop.subSystemList;
                var length = tempQualifier.Length;
                Array.Resize(ref tempQualifier, length + 1);
                tempQualifier[length] = system;
                playerLoop.subSystemList = tempQualifier;
                return true;
            }

            if (playerLoop.subSystemList != null)
            {
                for (var i = 0; i < playerLoop.subSystemList.Length; ++i)
                {
                    if (AddToPlayerLoop(function, ownerType, ref playerLoop.subSystemList[i], playerLoopSystemType))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Remove to PlayerLoop
        /// </summary>
        private static bool RemoveToPlayerLoop(PlayerLoopSystem.UpdateFunction function, Type ownerType, ref PlayerLoopSystem playerLoop, Type playerLoopSystemType)
        {
            if (playerLoop.type == playerLoopSystemType)
            {
                if (playerLoop.subSystemList != null)
                {
                    var index = Array.FindIndex(playerLoop.subSystemList, s => s.updateDelegate == function);
                    if (index != -1)
                    {
                        var length1 = playerLoop.subSystemList.Length;
                        if (index < length1 && index >= 0)
                        {
                            var length2 = length1 - 1;
                            var destinationArray = new PlayerLoopSystem[length2];
                            if (index == 0)
                            {
                                Array.Copy(playerLoop.subSystemList, 1, destinationArray, 0, length2);
                            }
                            else if (index == length2)
                            {
                                Array.Copy(playerLoop.subSystemList, 0, destinationArray, 0, length2);
                            }
                            else
                            {
                                Array.Copy(playerLoop.subSystemList, 0, destinationArray, 0, index);
                                Array.Copy(playerLoop.subSystemList, index + 1, destinationArray, index, length2 - index);
                            }

                            playerLoop.subSystemList = destinationArray;
                        }

                        return true;
                    }
                }
            }

            if (playerLoop.subSystemList != null)
            {
                for (var i = 0; i < playerLoop.subSystemList.Length; ++i)
                {
                    if (RemoveToPlayerLoop(function, ownerType, ref playerLoop.subSystemList[i], playerLoopSystemType))
                        return true;
                }
            }

            return false;
        }
    }
}