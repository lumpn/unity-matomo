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
            return new MatomoSession(url);
        }

        private MatomoSession(string cachedUrl)
        {
            this.cachedUrl = cachedUrl;
        }

        public UnityWebRequestAsyncOperation Record(string action)
        {
            var url = BuildUrl(action);
            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, null, null);
            var op = request.SendWebRequest();
            return op;
        }

        private string BuildUrl(string action)
        {
            builder.Append(cachedUrl);
            builder.Append(Uri.EscapeDataString(action));
            var url = builder.ToString();
            builder.Clear();

            return url;
        }
    }
}
