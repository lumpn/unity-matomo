//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using UnityEngine.TestTools;

namespace Lumpn.Matomo.Demo
{
    public sealed class TrackerTest
    {
        private const string matomoUrl = "http://onanotherplanet.net/analytics/matomo";
        private const string websiteUrl = "http://onanotherplanet.net";
        private const int websiteId = 1;
        private const string userId = "user1234";

        [UnityTest]
        public IEnumerator TrackPageView()
        {
            var tracker = new MatomoTracker(matomoUrl, websiteUrl, websiteId);
            var session = tracker.CreateSession(userId);

            yield return session.SendSystemInfo();
            yield return session.SendEvent("TrackerTest/TrackPageView");
        }
    }
}
