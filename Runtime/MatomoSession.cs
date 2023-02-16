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
            sb.Append("/matomo.php?apiv=1&rec=1&send_image=0&idsite=");
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

        [System.Serializable]
        private struct ClientHints
        {
            public string Sec_CH_UA_Model;
            public string Sec_CH_UA_Platform;
        }

        public UnityWebRequest CreateWebRequest(string title, string page, float timespanSeconds)
        {
            var timespanMilliseconds = timespanSeconds * 1000;
            var url = BuildUrl(title, page, (int)timespanMilliseconds);

            UnityEngine.Debug.Log(url);
            UnityEngine.Debug.Log(UnityEngine.Application.systemLanguage);
            UnityEngine.Debug.Log(UnityEngine.Application.platform);
            UnityEngine.Debug.Log(UnityEngine.Application.version);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.deviceModel);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.deviceType);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.deviceName);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.deviceUniqueIdentifier);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceID);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceName);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceType);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsMemorySize);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsShaderLevel);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceVendor);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceVersion);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.graphicsDeviceVendorID);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.operatingSystem);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.operatingSystemFamily);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.processorType);
            UnityEngine.Debug.Log(UnityEngine.SystemInfo.systemMemorySize);

            var hints = new ClientHints
            {
                Sec_CH_UA_Model = "Acer",
                Sec_CH_UA_Platform = "Windows",
            };

            var json = UnityEngine.JsonUtility.ToJson(hints);
            UnityEngine.Debug.Log(json);

            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, null, null);
            request.SetRequestHeader("Accept-Language", "en"); // system language
            //request.SetRequestHeader("Sec-CH-UA-Platform", UnityEngine.Application.platform.ToString());
            return request;
        }

        private string BuildUrl(string title, string page, int timespanMilliseconds)
        {
            stringBuilder.Clear();

            stringBuilder.Append(baseUrl);
            stringBuilder.Append(Uri.EscapeDataString(page));
            stringBuilder.Append("&action_name=");
            stringBuilder.Append(Uri.EscapeDataString(title));
            stringBuilder.Append("&gt_ms=");
            stringBuilder.Append(timespanMilliseconds);
            stringBuilder.Append("&pf_net=");
            stringBuilder.Append(timespanMilliseconds);
            stringBuilder.Append("&ua=");
            //stringBuilder.Append(Uri.EscapeDataString("Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0"));
            stringBuilder.Append(Uri.EscapeDataString("UnityPlayer/2020.3 (Windows 11  (10.0.22621) 64bit)"));
            //stringBuilder.Append("&uadata=");
            //stringBuilder.Append(Uri.EscapeDataString("{\"model\":\"Acer\",\"platform\":\"Windows\",\"platformversion\":\"11.0.0\"}"));
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
