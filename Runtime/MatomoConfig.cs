//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEngine;
using Lumpn.Matomo.Utils;

namespace Lumpn.Matomo
{
    [HelpURL("https://matomo.org/faq/how-to/create-and-manage-websites/")]
    [CreateAssetMenu(menuName = "Data/Matomo/MatomoConfig")]
    public sealed class MatomoConfig : ScriptableObject
    {
        [Header("Matomo")]
        [Tooltip("Matomo installation URL. Must contain the `matomo.php` file.")]
        [SerializeField] private string matomoUrl = "http://matomo.example.com";

        [Header("Project")]
        [Tooltip("The project to track. In Matomo referred to as `website`.")]
        [SerializeField] private string websiteUrl = "http://example.com";
        [SerializeField] private int websiteId = 1;

        public MatomoSession CreateSession(string userId = null)
        {
            var userHash = string.IsNullOrEmpty(userId) ? GetRandomBytes() : HashUtils.HashMD5(userId);
            return MatomoSession.Create(matomoUrl, websiteUrl, websiteId, userHash);
        }

        private static byte[] GetRandomBytes()
        {
            var random = new System.Random();

            var bytes = new byte[16];
            random.NextBytes(bytes);
            return bytes;
        }
    }
}
