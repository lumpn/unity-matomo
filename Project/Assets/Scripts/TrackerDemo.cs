//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//---------------------------------------- 
using System.Collections;
using UnityEngine;

namespace Lumpn.Matomo
{
    public class TrackerDemo : MonoBehaviour
    {
        [SerializeField] private MatomoTrackerData trackerData;

        IEnumerator Start()
        {
            var tracker = trackerData.CreateTracker();
            var session = tracker.CreateSession("user1234");
            var op = session.Record("TrackerDemo/Start");
            yield return op;

            var request = op.webRequest;
            Debug.Log(request.responseCode);
        }
    }
}
