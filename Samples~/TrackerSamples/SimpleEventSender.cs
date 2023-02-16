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

            yield return session.SendSystemInfo();

            for (int i = 0; i < numRecords; i++)
            {
                yield return session.SendEvent("TrackerDemo/Start");
            }
        }
    }
}
