//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Lumpn.Matomo.Samples
{
    [HelpURL("https://developer.matomo.org/api-reference/tracking-api")]
    public sealed class MatomoTester : MonoBehaviour
    {
        [System.Serializable]
        private struct Parameter
        {
            public string key;
            public string value;
        }

        [Header("Matomo")]
        [SerializeField] private MatomoTrackerData trackerData;

        [Tooltip("Set user id to track across sessions. Leave blank to randomize across sessions.")]
        [SerializeField] private string userId;

        [Header("Event data")]
        [SerializeField] private string eventName;
        [SerializeField] private Parameter[] parameters;

        [System.NonSerialized] private MatomoSession session;

        public IEnumerator Run()
        {
            if (session == null)
            {
                var tracker = trackerData.CreateTracker();
                session = tracker.CreateSession(userId);
            }

            var dict = parameters.ToDictionary(p => p.key, p => p.value);
            using (var request = session.CreateWebRequest(eventName, 0, dict, true))
            {
                Debug.Log(request.url);
                yield return request.SendWebRequest();

                switch (request.responseCode)
                {
                    case 204:
                        break; // all good
                    case 200:
                        Debug.Log(request.downloadHandler.text);
                        break;
                    default:
                        Debug.LogErrorFormat("Error {0}: {1}", request.responseCode, request.error);
                        break;
                }
            }
        }
    }
}
