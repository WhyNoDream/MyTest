using ConsulUnit.Instances;
using ConsulUnit.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsulUnit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsulOptions>(o=> {
                o.DnsEndpoint = new DnsEndpoint()
                {
                    Address = configuration.GetValue<string>("ServiceDiscovery:Consul:Address"),
                    Port = Convert.ToInt32(configuration.GetValue<string>("ServiceDiscovery:Consul:Port"))
                };
                o.HttpEndpoint = configuration.GetValue<string>("ServiceDiscovery:Consul:HttpEndpoint");
            });
            services.TryAddSingleton<IRegistryService, ConsulRegistryService>();
            return services;
        }
    }
}
