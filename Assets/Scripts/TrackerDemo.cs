using System.Collections;
using UnityEngine;

public class TrackerDemo : MonoBehaviour
{
    void Start2()
    {
        var tracker = new Piwik.Tracker.PiwikTracker(1, "http://onanotherplanet.net/analytics/matomo");
        tracker.DisableCookieSupport();
        tracker.SetUrl("http://onanotherplanet.net/analytics/TrackerDemo/Start");
        var response = tracker.DoTrackPageView("Start");
        Debug.Log(response);
    }

    IEnumerator Start()
    {
        var tracker = new MatomoTracker();
        return tracker.Foo("http://onanotherplanet.net/TrackerDemo/Start", "TrackerDemo");
    }
}
