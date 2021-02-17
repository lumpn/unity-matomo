//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Matomo
{
    [CreateAssetMenu(menuName = "Data/Matomo/MatomoTrackerData")]
    public sealed class MatomoTrackerData : ScriptableObject
    {
        [Tooltip("The path to the Matomo installation. Must contain the matomo.php file.")]
        [SerializeField] private string matomoUrl = "http://matomo.example.com";

        [Tooltip("The project to track. In Matomo referred to as website.")]
        [SerializeField] private string websiteUrl = "http://example.com";
        [SerializeField] private int websiteId = 1;

        public MatomoTracker CreateTracker()
        {
            return new MatomoTracker(matomoUrl, websiteUrl, websiteId);
        }
    }
}
