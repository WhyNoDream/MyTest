using CacheInfrastructrue.Contracts.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheInfrastructrue.Build;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCacheInfrastructrue.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加RabbitMQ到服务内 自动获取RabbitMQ节点
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.Configure<CacheConnOption>(configuration.GetSection("CsRedis"));
            AddCSRedis(services, configuration);
            return services;
        }

        /// <summary>
        /// 添加RabbitMQ到服务内 自动获取RabbitMQ节点
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddCSRedis(this IServiceCollection services, IConfiguration configuration)
        {
            new CSRedisBuild(services, configuration);
            return services;
        }
    }
}
