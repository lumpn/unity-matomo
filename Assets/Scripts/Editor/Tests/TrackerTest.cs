using NUnit.Framework;
using UnityEngine;

public class TrackerTest
{
    [Test]
    public void TrackPageView()
    {
        var tracker = new Piwik.Tracker.PiwikTracker(1, "http://onanotherplanet.net/analytics/matomo");
        tracker.DisableCookieSupport();
        tracker.SetUrl("http://onanotherplanet.net/analytics/matomo/TrackerTest/TrackPageView");
        var response = tracker.DoTrackPageView("TrackPageView");
        Debug.Log(response);
    }
}
