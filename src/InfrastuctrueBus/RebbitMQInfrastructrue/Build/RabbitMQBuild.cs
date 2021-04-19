using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MQInfrastructrue.Contracts;
using RebbitMQInfrastructrue;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQInfrastructrue
{
    public class RabbitMQBuild
    {
        /// <summary>
        /// 构建mq服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationRoot"></param>
        public RabbitMQBuild(IServiceCollection services, IConfiguration configurationRoot)
        {
            var mQConnOption= services.BuildServiceProvider().GetRequiredService<IOptions<MQConnOption>>();
            RabbitMqConf.SetConnectionFactory(mQConnOption?.Value);
            services.TryAddSingleton<IMQHelper, RabbitMQHelper>();
        }
    }
}
