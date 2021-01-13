using System.Collections;
using Matomo;
using UnityEngine;

public class TrackerDemo : MonoBehaviour
{
    IEnumerator Start()
    {
        var tracker = new MatomoTracker();
        var session = tracker.CreateSession("user1234");
        var op = session.Record("TrackerDemo/Start");
        yield return op;

        var request = op.webRequest;
        Debug.Log(request.responseCode);
    }
}
