using System.Collections;
using UnityEngine.TestTools;
using Matomo;

public class TrackerTest
{
    [UnityTest]
    public IEnumerator TrackPageView()
    {
        var tracker = new MatomoTracker();
        return tracker.Request("http://onanotherplanet.net/TrackerTest/TrackPageView", "TrackPageView");
    }
}
