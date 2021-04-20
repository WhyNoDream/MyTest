using CacheInfrastructrue.Contracts;
using CacheInfrastructrue.Contracts.Config;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCacheInfrastructrue.Build
{
    public class CSRedisBuild
    {
        /// <summary>
        /// 构建mq服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationRoot"></param>
        public CSRedisBuild(IServiceCollection services, IConfiguration configurationRoot)
        {
            var redisConnOption = services.BuildServiceProvider().GetRequiredService<IOptions<CacheConnOption>>();

            var csredis = new CSRedis.CSRedisClient(redisConnOption?.Value?.ConnectionString);
            RedisHelper.Initialization(csredis);
            services.TryAddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            services.TryAddSingleton<ICacheHelper, CacheHelper>();
        }
    }
}
