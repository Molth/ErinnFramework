//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Configuration information
    /// </summary>
    public static class ConfigInfo
    {
        /// <summary>
        ///     Device identifier
        /// </summary>
        public static string DeviceUniqueIdentifier => SystemInfo.deviceUniqueIdentifier;

        /// <summary>
        ///     Running Platform
        /// </summary>
        public static RuntimePlatform Platform => Application.platform;

        /// <summary>
        ///     Current time
        /// </summary>
        public static string CurrentDateTime => GetCurrentTimeFully();

        /// <summary>
        ///     Local IPAddress v4
        /// </summary>
        public static string LocalIpv4 => MathV.GetLocalIP();

        /// <summary>
        ///     Local IPAddress v6
        /// </summary>
        public static string LocalIpv6 => MathV.GetLocalIPOnlyV6();

        /// <summary>
        ///     Networking IPAddress
        /// </summary>
        public static Task<string> OnlineIP => !IsOffline ? MathV.GetOnlineIP() : null;

        /// <summary>
        ///     Network connection status
        /// </summary>
        public static NetworkReachability InternetReachability => Application.internetReachability;

        /// <summary>
        ///     Is there no network connection
        /// </summary>
        public static bool IsOffline => InternetReachability == NetworkReachability.NotReachable;

        /// <summary>
        ///     Target frame rate
        /// </summary>
        public static int TargetFrameRate => Application.targetFrameRate;

        /// <summary>
        ///     Support resolution
        /// </summary>
        public static Resolution[] Resolutions => Screen.resolutions;

        /// <summary>
        ///     Current resolution
        /// </summary>
        public static Resolution CurrentResolution => Screen.currentResolution;

        /// <summary>
        ///     Full screen or not
        /// </summary>
        public static bool FullScreen => Screen.fullScreen;

        /// <summary>
        ///     Full screen mode
        /// </summary>
        public static FullScreenMode FullScreenMode => Screen.fullScreenMode;

        /// <summary>
        ///     Current quality
        /// </summary>
        public static int QualityLevel => QualitySettings.GetQualityLevel();

        /// <summary>
        ///     Current time
        /// </summary>
        public static string GetCurrentTimeFully()
        {
            var now = DateTime.Now;
            return $"{(object)now.Year:D4}-{(object)now.Month:D2}-{(object)now.Day:D2} {(object)now.Hour:D2}:{(object)now.Minute:D2}:{(object)now.Second:D2}";
        }

        /// <summary>
        ///     Set target frame rate
        /// </summary>
        /// <param name="targetFrameRate">Target frame rate</param>
        public static void SetTargetFrameRate(int targetFrameRate) => Application.targetFrameRate = targetFrameRate;

        /// <summary>
        ///     Improve quality
        /// </summary>
        public static void IncreaseLevel() => QualitySettings.IncreaseLevel();

        /// <summary>
        ///     Reduce quality
        /// </summary>
        public static void DecreaseLevel() => QualitySettings.DecreaseLevel();

        /// <summary>
        ///     Get Resolution Selection
        /// </summary>
        public static List<string> GetResolutionOptions()
        {
            var list = new List<string>();
            foreach (var resolution in Resolutions)
                list.Add(resolution.ToString());
            list.Reverse();
            return list;
        }

        /// <summary>
        ///     Get current resolution selection
        /// </summary>
        public static int GetResolutionSelect()
        {
            for (var i = 0; i < Resolutions.Length; ++i)
                if (CurrentResolution.Equals(Resolutions[i]))
                    return Resolutions.Length - 1 - i;
            return 0;
        }

        /// <summary>
        ///     Set Resolution
        /// </summary>
        public static void SetResolution(int select)
        {
            if (select < 0 || select >= Resolutions.Length)
                return;
            select = Resolutions.Length - 1 - select;
            var resolution = Resolutions[select];
            Screen.SetResolution(resolution.width, resolution.height, FullScreen, resolution.refreshRate);
        }

        /// <summary>
        ///     Set Resolution
        /// </summary>
        public static void SetResolution(int select, bool isFullScreen)
        {
            if (select < 0 || select >= Resolutions.Length)
                return;
            select = Resolutions.Length - 1 - select;
            var resolution = Resolutions[select];
            Screen.SetResolution(resolution.width, resolution.height, isFullScreen, resolution.refreshRate);
        }

        /// <summary>
        ///     Set Resolution
        /// </summary>
        public static void SetResolution(int select, FullScreenMode mode)
        {
            if (select < 0 || select >= Resolutions.Length)
                return;
            select = Resolutions.Length - 1 - select;
            var resolution = Resolutions[select];
            Screen.SetResolution(resolution.width, resolution.height, mode, resolution.refreshRate);
        }

        /// <summary>
        ///     Set whether to display full screen
        /// </summary>
        public static void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;

        /// <summary>
        ///     Set full screen mode
        /// </summary>
        public static void SetFullScreenMode(FullScreenMode mode) => Screen.fullScreenMode = mode;
    }
}