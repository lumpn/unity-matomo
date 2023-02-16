//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using NUnit.Framework;
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

            using (var request = session.CreateWebRequest("TrackPageView", "TrackerTest/TrackPageView", 0.1f))
            {
                yield return request.SendWebRequest();

                Assert.AreEqual(200, request.responseCode);
                foreach (var header in request.GetResponseHeaders())
                {
                    UnityEngine.Debug.LogFormat("{0}: {1}", header.Key, header.Value);
                }
            }
        }
    }
}
