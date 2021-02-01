using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CommonUnit.Config;

namespace ConsulUnit.Extensions
{
    public static  class DiscoveryOptionsExtensions
    {
        public static IServiceCollection AddServiceDiscoveryOptions(this IServiceCollection services)
        {
            services.Configure<ServiceDiscoveryOptions>((o)=> {
                o.ConnectionStringName = ConfigManagerConf.GetValue("ServiceDiscovery:ConnectionStringName");
                o.Endpoints = ConfigManagerConf.GetReferenceValue("ServiceDiscovery:Consul:DnsEndpoint").ToArray();
                o.HealthCheckTemplate = ConfigManagerConf.GetValue("ServiceDiscovery:HealthCheckTemplate");
                o.Namespace = ConfigManagerConf.GetValue("ServiceDiscovery:Namespace");
                o.ServiceName = ConfigManagerConf.GetValue("ServiceDiscovery:ServiceName");
                o.Version = ConfigManagerConf.GetValue("ServiceDiscovery:Version");
            });
            services.AddHealthChecks();
            return services;

        }
    }
}
