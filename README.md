# Matomo Analytics
[Matomo Analytics](https://matomo.org) for Unity. [GDPR compliant](https://matomo.org/gdpr-analytics/) by default. No need to ask the user for consent.

## Installation
Download the entire repository from https://github.com/lumpn/unity-matomo and use Unity's built in package manager to [Add package from disk](https://docs.unity3d.com/Manual/upm-ui-local.html).

## Usage
```csharp
    public MatomoTrackerData trackerData;

    IEnumerator Start()
    {
        var tracker = trackerData.CreateTracker();
        var session = tracker.CreateSession();

        yield return session.RecordEvent("MyGameEvent", Time.time);
        yield return session.RecordEvent("AnotherGameEvent", Time.time);
    }
```

## Installing Matomo
Follow Matomo's guide to [installing Matomo on-premise](https://matomo.org/faq/on-premise/installing-matomo/), then set up a website to track, create a `MatomoTrackerData` asset, and paste the website URL and id into the `MatomoTrackerData` asset.

![Copy website URL](https://static.matomo.org/wp-content/uploads/2008/11/6-setup-website1.png)

### Nomenclature
Matomo is built for website tracking, but we will use it to track game events. The names of things are different, but the results are just the same.

| Game | Matomo |
|------|--------|
| Project | Website |
| Event | Page |
| Event name | Page URL |
| Event time | Page load time |

You can make up any website URL for your project. The domain does not have to exist. I tend to use `https://<project-name>.game` because it's short.

## Samples
See `Samples` for details.
