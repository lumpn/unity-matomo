//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System;
using System.Text;
using Lumpn.Matomo.Utils;
using UnityEngine.Networking;

namespace Lumpn.Matomo
{
    public sealed class MatomoSession
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private readonly string baseUrl;
        private readonly Random random;

        public static MatomoSession Create(string matomoUrl, string websiteUrl, int websiteId, byte[] userHash)
        {
            var sb = new StringBuilder(matomoUrl);
            sb.Append("/matomo.php?apiv=1&rec=1&send_image=0&new_visit=1&idsite=");
            sb.Append(websiteId);
            sb.Append("&_id=");
            HexUtils.AppendHex(sb, userHash);
            sb.Append("&url=");
            sb.Append(Uri.EscapeDataString(websiteUrl));
            sb.Append(Uri.EscapeDataString("/"));

            var url = sb.ToString();
            var seed = CalculateSeed(userHash);
            return new MatomoSession(url, seed);
        }

        private MatomoSession(string baseUrl, int seed)
        {
            this.baseUrl = baseUrl;
            this.random = new Random(seed);
        }

        public UnityWebRequest CreateWebRequest(string title, string page, float timespanSeconds)
        {
            var timespanMilliseconds = timespanSeconds * 1000;
            var url = BuildUrl(title, page, (int)timespanMilliseconds);

            UnityEngine.Debug.Log(url);
            UnityEngine.Debug.Log(UnityEngine.Application.unityVersion);
            UnityEngine.Debug.Log(UnityEngine.Application.systemLanguage);
            UnityEngine.Debug.Log(UnityEngine.Application.platform);
            UnityEngine.Debug.Log(UnityEngine.Application.version);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceName);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceVendor);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.operatingSystem);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.operatingSystemFamily);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.processorType);

            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, null, null);
            request.SetRequestHeader("Accept-Language", "de"); // system language
            request.SetRequestHeader("Model", "PlayStation 5");
            request.SetRequestHeader("Platform", "PlayStation 5");
            return request;
        }

        [System.Serializable]
        public struct ClientHints
        {
            public string Model;
            public string Platform;
        }

        private string BuildUrl(string title, string page, int timespanMilliseconds)
        {
            stringBuilder.Clear();

            var clientHints = new ClientHints
            {
                Model = "PlayStation 5",
                Platform = "PlayStation 5",
            };

            stringBuilder.Append(baseUrl);
            stringBuilder.Append(Uri.EscapeDataString(page));
            stringBuilder.Append("&action_name=");
            stringBuilder.Append(Uri.EscapeDataString(title));
            stringBuilder.Append("&gt_ms=");
            stringBuilder.Append(timespanMilliseconds);
            stringBuilder.Append("&pf_net=");
            stringBuilder.Append(timespanMilliseconds);
            stringBuilder.Append("&res=");
            stringBuilder.Append(UnityEngine.Screen.width);
            stringBuilder.Append("x");
            stringBuilder.Append(UnityEngine.Screen.height);
            //stringBuilder.Append("&ua=");
            //stringBuilder.Append(Uri.EscapeDataString("UnityPlayer/2019.6 (Playstation 5)"));
            stringBuilder.Append("&uadata=");
            stringBuilder.Append(Uri.EscapeDataString(UnityEngine.JsonUtility.ToJson(clientHints)));
            stringBuilder.Append("&rand=");
            stringBuilder.Append(random.Next());

            var url = stringBuilder.ToString();
            stringBuilder.Clear();

            return url;
        }

        private static int CalculateSeed(byte[] bytes)
        {
            unchecked
            {
                int result = 17;
                foreach (var value in bytes)
                {
                    result = result * 23 + value;
                }
                return result;
            }
        }
    }
}
