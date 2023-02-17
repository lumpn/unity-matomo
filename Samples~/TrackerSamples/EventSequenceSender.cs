//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using UnityEngine;

namespace Lumpn.Matomo.Samples
{
    public class EventSequenceSender : MonoBehaviour
    {
        [Header("Matomo")]
        [SerializeField] private MatomoTrackerData trackerData;

        [Header("Event sequence to send over time in play mode")]
        [SerializeField] private string[] events;

        IEnumerator Start()
        {
            var tracker = trackerData.CreateTracker();
            var session = tracker.CreateSession();

            Debug.Log("Sending system info");
            yield return session.SendSystemInfo();
            yield return new WaitForSeconds(1f);

            foreach (var eventName in events)
            {
                Debug.LogFormat(this, "Sending event '{0}'", eventName);
                yield return session.SendEvent(eventName, Time.time);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }
        }
    }
}
