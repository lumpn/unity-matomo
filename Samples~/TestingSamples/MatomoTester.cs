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

                Debug.Log("Sending system info");
                yield return session.SendSystemInfo();
            }

            Debug.LogFormat("Sending event with {0} parameters", parameters.Length);
            var dict = parameters.ToDictionary(p => p.key, p => p.value);
            yield return session.Send(eventName, Time.time, dict);
        }
    }
}
