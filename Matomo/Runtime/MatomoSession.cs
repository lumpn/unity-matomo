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
        private readonly StringBuilder builder = new StringBuilder();
        private readonly string cachedUrl;
        private readonly Random random;

        public static MatomoSession Create(string matomoUrl, string websiteUrl, int websiteId, byte[] userHash)
        {
            var builder = new StringBuilder(matomoUrl);
            builder.Append("/matomo.php?apiv=1&rec=1&idsite=");
            builder.Append(websiteId);
            builder.Append("&_id=");
            HexUtils.AppendHex(builder, userHash);
            builder.Append("&url=");
            builder.Append(Uri.EscapeDataString(websiteUrl));
            builder.Append(Uri.EscapeDataString("/"));

            var url = builder.ToString();
            var seed = CalculateSeed(userHash);
            return new MatomoSession(url, seed);
        }

        private MatomoSession(string cachedUrl, int seed)
        {
            this.cachedUrl = cachedUrl;
            this.random = new Random(seed);
        }

        public UnityWebRequestAsyncOperation Record(string title, string page, float timespanSeconds)
        {
            var timespanMilliseconds = timespanSeconds * 1000;
            var url = BuildUrl(title, page, (int)timespanMilliseconds);
            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, null, null);
            var op = request.SendWebRequest();
            UnityEngine.Debug.Log(url);
            return op;
        }

        private string BuildUrl(string title, string page, int timespanMilliseconds)
        {
            builder.Append(cachedUrl);
            builder.Append(Uri.EscapeDataString(page));
            builder.Append("&action_name=");
            builder.Append(Uri.EscapeDataString(title));
            builder.Append("&pf_net=");
            builder.Append(timespanMilliseconds);
            builder.Append("&rand=");
            builder.Append(random.Next());
            var url = builder.ToString();
            builder.Clear();

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
