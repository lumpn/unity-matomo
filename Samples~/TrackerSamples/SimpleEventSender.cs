//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using UnityEngine;

namespace Lumpn.Matomo.Samples
{
    public class SimpleEventSender : MonoBehaviour
    {
        [SerializeField] private MatomoTrackerData trackerData;
        [SerializeField] private int numRecords = 10;

        IEnumerator Start()
        {
            var tracker = trackerData.CreateTracker();
            var session = tracker.CreateSession("user" + Random.Range(0, 1000));

            for (int i = 0; i < numRecords; i++)
            {
                using (var request = session.CreateWebRequest("Start", "TrackerDemo/Start", Random.value))
                {
                    yield return request.SendWebRequest();
                    Debug.Log(request.responseCode);
                    foreach (var header in request.GetResponseHeaders())
                    {
                        UnityEngine.Debug.LogFormat("{0}: {1}", header.Key, header.Value);
                    }
                }
            }
        }
    }
}
