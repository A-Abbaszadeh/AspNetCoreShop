using System;

namespace Infrastructure.CacheHelpers
{
    public static class CacheHelper
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(10);
        public static string GenerateHomePageCacheKey()
        {
            return "HomePage";
        }
    }
}
