using StackExchange.Redis;
using System;

namespace MicrosoftAzureRedisCache
{
    public class CacheManager
    {
        private static Lazy<ConnectionMultiplexer>
            lazyConnection = new Lazy<ConnectionMultiplexer>(
                () =>
                {
                    return ConnectionMultiplexer
                    .Connect("ittalentcache.redis.cache.windows.net:6380,password=CpRUVGF8Hr9PICqhhOOXWVfCwA0isw4xqSIfoCI7gjU=,ssl=True,abortConnect=False");

                });

        public static ConnectionMultiplexer Connection
        {
            get { return lazyConnection.Value; }
        }
    }
}
