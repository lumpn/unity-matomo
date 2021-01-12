using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public sealed class MatomoTracker
{
    public IEnumerator Foo(string action, string title)
    {
        var payloadAction = Uri.EscapeDataString(action);
        var payloadTitle = Uri.EscapeDataString(title);
        var userId = "1122334455667788";

        var url = $"http://onanotherplanet.net/analytics/matomo/matomo.php?idsite=1&rec=1&apiv=1&debug=1&_id={userId}&url={payloadAction}&action_name={payloadTitle}";
        var created = Uri.TryCreate(url, UriKind.Absolute, out Uri uri);
        Debug.Assert(created);
        Debug.Assert(uri.Scheme == "http");
        Debug.Log(uri.Scheme);

        var request = UnityWebRequest.Get(uri);
        request.certificateHandler = new DummyCertificateHandler();
        var op = request.SendWebRequest();
        yield return op;

        var request2 = op.webRequest;
        var result = request2.result;
        var code = request2.responseCode;
        Debug.Log($"request '{url}', result {result}, code {code}");

        var dl = request2.downloadHandler;
        Debug.Log(dl.text);
    }
}
