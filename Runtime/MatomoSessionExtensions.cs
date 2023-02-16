using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn.Matomo
{
    public static class MatomoSessionExtensions
    {
        private static readonly Dictionary<string, string> emptyParameters = new Dictionary<string, string>();

        public static IEnumerator SendSystemInfo(this MatomoSession session)
        {
            var parameters = new Dictionary<string, string>
            {
                { "new_visit", "1"},
                { "ua", GetUserAgent(Application.unityVersion, Application.platform) },
                { "lang", GetLanguageCode(Application.systemLanguage) },
                { "res", string.Format("{0}x{1}", Screen.width, Screen.height)},
                { "dimension1", SystemInfo.processorType},
                { "dimension2", SystemInfo.graphicsDeviceName},
            };

            return session.Send("SystemInfo", parameters);
        }

        public static IEnumerator SendEvent(this MatomoSession session, string eventName)
        {
            return session.Send(eventName, emptyParameters);
        }

        private static IEnumerator Send(this MatomoSession session, string page, IDictionary<string, string> parameters)
        {
            using (var request = session.CreateWebRequest(page, parameters))
            {
                yield return request.SendWebRequest();

                Debug.AssertFormat(request.responseCode == 204, "{0}: {1}", request.error);
            }
        }

        private static string GetLanguageCode(SystemLanguage language)
        {
            switch (language)
            {
                case SystemLanguage.German: return "de";
                case SystemLanguage.English: return "en";
                default: return "*";
            }
        }

        private static string GetUserAgent(string unityVersion, RuntimePlatform platform)
        {
            return string.Format("UnityPlayer/{0} ({1})", GetUnityVersion(unityVersion), GetPlatform(platform));
        }

        private static string GetUnityVersion(string unityVersion)
        {
            return unityVersion.Substring(0, 6);
        }

        private static string GetPlatform(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "OS X";
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "Windows";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                    return "Linux";
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
                    return null;
            }
        }

        public static void Foo()
        {
            //var timespanMilliseconds = timespanSeconds * 1000;
            //var url = BuildUrl(title, page, (int)timespanMilliseconds, isFirstRequest);

            //stringBuilder.Append("&action_name=");
            //stringBuilder.Append(EscapeDataString(title));
            //sb.Append("&gt_ms=");
            //sb.Append(timespanMilliseconds * 10);
            //sb.Append("&pf_net=");
            //sb.Append(timespanMilliseconds);
        }
    }
}
