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
        [SerializeField] private MatomoTrackerData trackerData;
        [SerializeField] private string[] events;

        IEnumerator Start()
        {
            var tracker = trackerData.CreateTracker();
            var session = tracker.CreateSession("user" + Random.Range(0, 1000));

            yield return session.SendSystemInfo();

            foreach(var eventName in events)
            {
                yield return session.SendEvent(eventName, Time.time);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }
        }

        private static IEnumerator Send(MatomoSession session, string eventName)
        {
            yield break;
        }
    }
}
