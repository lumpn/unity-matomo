//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Lumpn.Matomo.Utils;
using UnityEngine.Networking;

namespace Lumpn.Matomo
{
    public sealed class MatomoSession
    {
        private readonly Random random = new Random();
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private readonly string baseUrl;

        public static MatomoSession Create(string matomoUrl, string websiteUrl, int websiteId, byte[] userId)
        {
            var sb = new StringBuilder(matomoUrl);
            sb.Append("/matomo.php?apiv=1&rec=1&send_image=0&idsite=");
            sb.Append(websiteId);
            sb.Append("&_id=");
            HexUtils.AppendHex(sb, userId);
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

        public UnityWebRequest CreateWebRequest(string page, IDictionary<string, string> parameters)
        {
            var url = BuildUrl(page, parameters);
            UnityEngine.Debug.Log(url);

            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, new DownloadHandlerBuffer(), null);
            return request;
        }

        private string BuildUrl(string page, IDictionary<string, string> parameters)
        {
            var sb = stringBuilder;
            sb.Clear();

            sb.Append(baseUrl);
            sb.Append(EscapeDataString(page));

            foreach (var parameter in parameters)
            {
                sb.Append("&");
                sb.Append(parameter.Key);
                sb.Append("=");
                sb.Append(EscapeDataString(parameter.Value));
            }

            sb.Append("&debug=1&rand=");
            sb.Append(random.Next());


            var url = sb.ToString();
            sb.Clear();

            return url;
        }

        private static string EscapeDataString(string str)
        {
            return Uri.EscapeDataString(str);
        }
    }
}
