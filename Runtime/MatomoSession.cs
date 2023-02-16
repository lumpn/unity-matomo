//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Text;
using Lumpn.Matomo.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Lumpn.Matomo
{
    public sealed class MatomoSession
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private readonly string baseUrl;
        private readonly System.Random random;

        public static MatomoSession Create(string matomoUrl, string websiteUrl, int websiteId, byte[] userHash)
        {
            var sb = new StringBuilder(matomoUrl);
            sb.Append("/matomo.php?apiv=1&rec=1&send_image=0&new_visit=1&idsite=");
            sb.Append(websiteId);
            sb.Append("&_id=");
            HexUtils.AppendHex(sb, userHash);
            sb.Append("&res=");
            sb.Append(Screen.width);
            sb.Append("x");
            sb.Append(Screen.height);
            sb.Append("&ua=");
            sb.Append(EscapeDataString("UnityPlayer/2019.6 (Playstation 5)"));
            sb.Append("&url=");
            sb.Append(EscapeDataString(websiteUrl));
            sb.Append(EscapeDataString("/"));

            var url = sb.ToString();
            var seed = CalculateSeed(userHash);
            return new MatomoSession(url, seed);
        }

        private MatomoSession(string baseUrl, int seed)
        {
            this.baseUrl = baseUrl;
            this.random = new System.Random(seed);
        }

        public UnityWebRequest CreateWebRequest(string title, string page, float timespanSeconds)
        {
            var timespanMilliseconds = timespanSeconds * 1000;
            var url = BuildUrl(title, page, (int)timespanMilliseconds);

            Debug.Log(url);
            Debug.Log(Application.unityVersion);
            Debug.Log(Application.systemLanguage);
            Debug.Log(Application.platform);
            Debug.Log(Application.version);
            Debug.Log(SystemInfo.graphicsDeviceName);
            Debug.Log(SystemInfo.graphicsDeviceVendor);
            Debug.Log(SystemInfo.operatingSystem);
            Debug.Log(SystemInfo.operatingSystemFamily);
            Debug.Log(SystemInfo.processorType);

            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, null, null);
            request.SetRequestHeader("Accept-Language", "de"); // system language
            return request;
        }

        private string BuildUrl(string title, string page, int timespanMilliseconds)
        {
            stringBuilder.Clear();

            stringBuilder.Append(baseUrl);
            stringBuilder.Append(EscapeDataString(page));
            stringBuilder.Append("&action_name=");
            stringBuilder.Append(EscapeDataString(title));
            stringBuilder.Append("&gt_ms=");
            stringBuilder.Append(timespanMilliseconds);
            stringBuilder.Append("&pf_net=");
            stringBuilder.Append(timespanMilliseconds);
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

        private static string EscapeDataString(string str)
        {
            return System.Uri.EscapeDataString(str);
        }
    }
}
