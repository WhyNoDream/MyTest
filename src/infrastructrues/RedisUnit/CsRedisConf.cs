using CommonUnit.Config;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisUnit
{
    public static class CsRedisConf
    {
        public static IServiceCollection AddCsRedisConf(this IServiceCollection services)
        {
            var csredis = new CSRedis.CSRedisClient(ConfigManagerConf.GetValue("CsRedis:ConnectionString"));
            RedisHelper.Initialization(csredis);
            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            return services;
        }
    }
}
