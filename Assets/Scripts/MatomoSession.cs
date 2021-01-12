using System.Collections;
using System;
using System.Text;

public sealed class MatomoSession
{
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

    public IEnumerator Record(string action)
    {
        return null;
    }
}
