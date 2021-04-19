using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQInfrastructrue.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQInfrastructrue
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
            services.Configure<MQConnOption>(configuration.GetSection("RabbitMQ"));
            AddRabbitMQ(services, configuration);
            return services;
        }

        /// <summary>
        /// 添加RabbitMQ到服务内 自动获取RabbitMQ节点
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            new RabbitMQBuild(services, configuration);
            return services;
        }
    }
}
