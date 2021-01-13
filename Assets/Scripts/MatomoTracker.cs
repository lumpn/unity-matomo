using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Security.Cryptography;

namespace Matomo
{
    [CreateAssetMenu]
    public sealed class MatomoTracker : ScriptableObject
    {
        [SerializeField] private string matomoUrl = "http://matomo.example.com";

        [SerializeField] private string websiteUrl = "http://example.com";
        [SerializeField] private int websiteId = 1;

        public MatomoSession CreateSession(string userId)
        {
            using (var md5 = MD5.Create())
            {
                var userBytes = Encoding.ASCII.GetBytes(userId);
                var userHash = md5.ComputeHash(userBytes);

                return MatomoSession.Create(matomoUrl, websiteUrl, websiteId, userHash);
            }
        }

        public IEnumerator Request(string action, string title)
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
}
