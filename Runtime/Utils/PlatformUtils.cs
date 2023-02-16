//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Matomo.Utils
{
    public static class PlatformUtils
    {
        public static string GetDevice(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.LinuxEditor:
                    return "Palm OS"; // routes all editor traffic to fake device
                case RuntimePlatform.OSXPlayer:
                    return "Mac OSX";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.LinuxPlayer:
                    return "Linux";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.PS4:
                    return "PlayStation 4";
                case RuntimePlatform.PS5:
                    return "PlayStation 5";
                case RuntimePlatform.XboxOne:
                case RuntimePlatform.GameCoreXboxOne:
                    return "Xbox One";
                case RuntimePlatform.GameCoreXboxSeries:
                    return "Xbox Series X";
                case RuntimePlatform.Switch:
                    return "Nintendo Switch";
                default:
                    return "";
            }
        }
    }
}
