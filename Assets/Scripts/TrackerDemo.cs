using System.Collections;
using UnityEngine;

public class TrackerDemo : MonoBehaviour
{
    IEnumerator Start()
    {
        var tracker = new MatomoTracker();
        return tracker.Request("http://onanotherplanet.net/TrackerDemo/Update", "");
    }
}
