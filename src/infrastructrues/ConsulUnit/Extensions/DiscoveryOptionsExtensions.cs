using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ConsulUnit.Extensions
{
    public static  class DiscoveryOptionsExtensions
    {
        public static IServiceCollection AddServiceDiscoveryOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceDiscoveryOptions>((o)=> {
                o.ConnectionStringName = configuration.GetValue<string>("ServiceDiscovery:ConnectionStringName");
                o.Endpoints = configuration.GetValue<string[]>("ServiceDiscovery:Consul:DnsEndpoint");
                o.HealthCheckTemplate = configuration.GetValue<string>("ServiceDiscovery:HealthCheckTemplate");
                o.Namespace = configuration.GetValue<string>("ServiceDiscovery:Namespace");
                o.ServiceName = configuration.GetValue<string>("ServiceDiscovery:ServiceName");
                o.Version = configuration.GetValue<string>("ServiceDiscovery:Version");
            });
            services.AddHealthChecks();
            return services;

        }
    }
}
