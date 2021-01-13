//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//---------------------------------------- 
using System.Security.Cryptography;
using System.Text;

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
            using (var md5 = MD5.Create())
            {
                var userBytes = Encoding.ASCII.GetBytes(userId);
                var userHash = md5.ComputeHash(userBytes);

                return MatomoSession.Create(matomoUrl, websiteUrl, websiteId, userHash);
            }
        }
    }
}
