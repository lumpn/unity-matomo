// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using Lumpn.Matomo.Utils;
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
                { "lang", LanguageUtils.GetLanguageCode(Application.systemLanguage) },
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

        public static IEnumerator SendEvent(this MatomoSession session, string eventName, float time)
        {
            var parameters = new Dictionary<string, string>
            {
                {"pf_srv", ((int)time).ToString() },
            };

            return session.Send(eventName, parameters);
        }

        public static IEnumerator Send(this MatomoSession session, string page, IDictionary<string, string> parameters)
        {
            using (var request = session.CreateWebRequest(page, parameters))
            {
                yield return request.SendWebRequest();

                Debug.Assert(request.responseCode == 204, request.error);
            }
        }

        private static string GetUserAgent(string unityVersion, RuntimePlatform platform)
        {
            return string.Format("UnityPlayer/{0} ({1})", GetUnityVersion(unityVersion), PlatformUtils.GetDevice(platform));
        }

        private static string GetUnityVersion(string unityVersion)
        {
            return unityVersion.Substring(0, 6);
        }
    }
}
