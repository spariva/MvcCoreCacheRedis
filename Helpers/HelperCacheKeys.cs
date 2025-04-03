namespace MvcCoreCacheRedis.Helpers
{
    public static class HelperCacheKeys
    {
        public static IConfiguration Configuration { get; set; }

        public static string CacheRedisKeys => Configuration.GetValue<string>("AzureKeys:CacheRedis");
    }
}
