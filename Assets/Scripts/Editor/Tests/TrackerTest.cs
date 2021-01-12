using System.Collections;
using UnityEngine.TestTools;

public class TrackerTest
{
    [UnityTest]
    public IEnumerator TrackPageView()
    {
        var tracker = new MatomoTracker();
        return tracker.Request("http://onanotherplanet.net/TrackerTest/TrackPageView", "TrackPageView");
    }
}
