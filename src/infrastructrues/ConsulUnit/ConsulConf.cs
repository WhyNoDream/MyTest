using CommonUnit.Config;
using Consul;
using ConsulUnit.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsulUnit
{

    public static class ConsulConf
    {
        private const string VERSION_PREFIX = "version-";
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>() ??
               throw new ArgumentNullException(nameof(IHostApplicationLifetime));

            var environment = app.ApplicationServices.GetRequiredService<IHostEnvironment>() ??
               throw new ArgumentNullException(nameof(IHostEnvironment));

            var options = app.ApplicationServices.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var consulOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulOptions>>().Value;

            if (consulOptions == null)
                throw new ArgumentNullException(nameof(consulOptions));

            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("Service Builder");


            var addresses = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses.Select(p => new Uri(GetServiceUrl(p))).ToArray();

            foreach (var address in addresses)
            {
                var serviceAddress = address;
                if (environment.IsProduction())
                {
                    serviceAddress = new Uri($"http://{options.ServiceName}.{options.Namespace}:{address.Port}");
                }
                logger.LogInformation($"Address: {serviceAddress}");

                Uri healthCheck = null;
                if (!string.IsNullOrEmpty(options.HealthCheckTemplate))
                {
                    healthCheck = new Uri(options.HealthCheckTemplate);
                }
                var registryId = app.AddTenant(options.ServiceName, options.Version, serviceAddress, healthCheckUri: healthCheck, tags: new[] { $"urlprefix-/{options.ServiceName}" });

                applicationLifetime.ApplicationStopping.Register(() =>
                {
                    app.RemoveTenant(registryId);
                });
            }
            return app;
        }

        private static string GetServiceUrl(string url)
        {
            var serverAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily.Equals(AddressFamily.InterNetwork))?.ToString();

            return url.Replace("*", serverAddress).Replace("*", serverAddress).Replace("localhost", serverAddress);
        }

        private static string AddTenant(this IApplicationBuilder app, string serviceName, string version, Uri uri, Uri healthCheckUri = null, IEnumerable<string> tags = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var consulClient = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(ConfigManagerConf.GetValue("ServiceDiscovery:Consul:HttpEndpoint"));
            });

            string check = healthCheckUri?.ToString() ?? $"{uri}".TrimEnd('/') + "/health";

            string versionLabel = $"{VERSION_PREFIX}{version}";
            var tagList = (tags ?? Enumerable.Empty<string>()).ToList();
            tagList.Add(versionLabel);

            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = serviceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = tagList.ToArray(),
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止_后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(10),
                    // 3.3、consul健康检查地址
                    HTTP = check,
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            string id = registration.ID;
            var allService = consulClient.Catalog.Service(ConfigManagerConf.GetValue("ServiceDiscovery:ServiceName")).GetAwaiter().GetResult();
            var regService = allService.Response.FirstOrDefault(x =>
                x.Address == registration.Address && x.ServicePort == registration.Port);
            if (regService == null)
            {
                Console.WriteLine($"注册的serviceid：{registration.ID}");
                consulClient.Agent.ServiceRegister(registration).Wait();
            }
            else
            {
                id = regService.ServiceID;
            }

            consulClient.Dispose();

            return id;
        }

        private static bool RemoveTenant(this IApplicationBuilder app, string serviceId)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (string.IsNullOrEmpty(serviceId))
            {
                throw new ArgumentNullException(nameof(serviceId));
            }

            var serviceRegistry = app.ApplicationServices.GetRequiredService<IRegistryService>();
            return serviceRegistry.DeregisterServiceAsync(serviceId)
                .Result;
        }

    }


}
