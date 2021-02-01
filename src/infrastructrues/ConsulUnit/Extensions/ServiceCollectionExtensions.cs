using CommonUnit.Config;
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
        public static IServiceCollection AddConsulService(this IServiceCollection services)
        {
            services.Configure<ConsulOptions>(o=> {
                o.DnsEndpoint = new DnsEndpoint()
                {
                    Address = ConfigManagerConf.GetValue("ServiceDiscovery:Consul:DnsEndpoint:Address"),
                    Port = Convert.ToInt32(ConfigManagerConf.GetValue("ServiceDiscovery:Consul:DnsEndpoint:Port"))
                };
                o.HttpEndpoint = ConfigManagerConf.GetValue("ServiceDiscovery:Consul:HttpEndpoint");
            });
            services.TryAddSingleton<IRegistryService, ConsulRegistryService>();
            return services;
        }
    }
}
