//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using Lumpn.Matomo.Utils;

namespace Lumpn.Matomo
{
    public sealed class MatomoTracker
    {
        private readonly string matomoUrl;
        private readonly string websiteUrl;
        private readonly int websiteId;

        public MatomoTracker(string matomoUrl, string websiteUrl, int websiteId)
        {
            this.matomoUrl = matomoUrl;
            this.websiteUrl = websiteUrl;
            this.websiteId = websiteId;
        }

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
