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

        public MatomoSession CreateSession(string userId)
        {
            var userHash = HashUtils.HashMD5(userId);
            return MatomoSession.Create(matomoUrl, websiteUrl, websiteId, userHash);
        }
    }
}
