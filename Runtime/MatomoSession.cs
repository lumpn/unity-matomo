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
        private bool isFirstRequest = true;

        public static MatomoSession Create(string matomoUrl, string websiteUrl, int websiteId, byte[] userHash)
        {
            var sb = new StringBuilder(matomoUrl);
            sb.Append("/matomo.php?apiv=1&rec=1&send_image=0&idsite=");
            sb.Append(websiteId);
            sb.Append("&_id=");
            HexUtils.AppendHex(sb, userHash);
            sb.Append("&url=");
            sb.Append(EscapeDataString(websiteUrl));
            sb.Append(EscapeDataString("/"));

            var url = sb.ToString();
            return new MatomoSession(url);
        }

        private MatomoSession(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public UnityWebRequest CreateWebRequest(string title, string page, float timespanSeconds)
        {
            var timespanMilliseconds = timespanSeconds * 1000;
            var url = BuildUrl(title, page, (int)timespanMilliseconds, isFirstRequest);
            isFirstRequest = false;

            Debug.Log(url);

            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, null, null);
            request.SetRequestHeader("Accept-Language", GetLanguageCode(Application.systemLanguage));
            return request;
        }

        private string BuildUrl(string title, string page, int timespanMilliseconds, bool isNewVisit)
        {
            var sb = stringBuilder;
            sb.Clear();

            sb.Append(baseUrl);
            sb.Append(EscapeDataString(page));
            //stringBuilder.Append("&action_name=");
            //stringBuilder.Append(EscapeDataString(title));
            sb.Append("&gt_ms=");
            sb.Append(timespanMilliseconds * 10);
            sb.Append("&pf_net=");
            sb.Append(timespanMilliseconds);
            sb.Append("&rand=");
            sb.Append(Random.Range(0, 100000));

            if (isNewVisit)
            {
                sb.Append("&new_visit=1");
                sb.Append("&res=");
                sb.Append(Screen.width);
                sb.Append("x");
                sb.Append(Screen.height);
                sb.Append("&ua=");
                sb.Append(EscapeDataString(GenerateUserAgent()));
                sb.Append("&dimension1=");
                sb.Append(EscapeDataString(SystemInfo.graphicsDeviceName));
                sb.Append("&dimension2=");
                sb.Append(EscapeDataString(SystemInfo.processorType));
            }

            var url = sb.ToString();
            sb.Clear();

            return url;
        }

        private static string EscapeDataString(string str)
        {
            return System.Uri.EscapeDataString(str);
        }

        private static string GenerateUserAgent()
        {
            Debug.Log(Application.unityVersion);
            Debug.Log(Application.platform);
            Debug.Log(SystemInfo.operatingSystem);

            // TODO: generate from Application.unityVersion, Application.platform, SystemInfo.operatingSystem
            return "UnityPlayer/2019.4 (Playstation 4)";
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
    }
}
