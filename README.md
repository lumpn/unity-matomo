# unity-matomo
[Matomo Analytics](https://matomo.org) for Unity.

## Installation
Download the entire repository from https://github.com/lumpn/unity-matomo and use Unity's built in package manager to [Add package from disk](https://docs.unity3d.com/Manual/upm-ui-local.html).

## Usage
```csharp
    public MatomoTrackerData trackerData;

    void Start()
    {
        var tracker = trackerData.CreateTracker();
        var session = tracker.CreateSession("user id here");

        session.Record("MyGameEvent");
        session.Record("AnotherGameEvent");
    }
```

## Notes
* See `Demo` project for details.
