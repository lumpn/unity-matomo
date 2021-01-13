//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//---------------------------------------- 
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Lumpn.Matomo.Test
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
            var op = session.Record("TrackerTest/TrackPageView");
            yield return op;

            var request = op.webRequest;
            Assert.AreEqual(200, request.responseCode);
        }
    }
}
