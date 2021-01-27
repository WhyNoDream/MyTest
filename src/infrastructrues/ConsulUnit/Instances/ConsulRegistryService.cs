using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ConsulUnit.Interface;
using ConsulUnit.Entitys;
using CommonUnit.StringUnit;

namespace ConsulUnit.Instances
{
    public class ConsulRegistryService : IRegistryService
    {
        private const string VERSION_PREFIX = "version-";

        private readonly ConsulClient _consul;

        public ConsulRegistryService(IOptions<ConsulOptions> options)
        {
            _consul = new ConsulClient(config =>
            {
                config.Address = new Uri(options.Value.HttpEndpoint);
                if (!string.IsNullOrEmpty(options.Value.Datacenter))
                {
                    config.Datacenter = options.Value.Datacenter;
                }
            });
        }

        private string GetVersionFromStrings(IEnumerable<string> strings)
        {
            return strings
                ?.FirstOrDefault(x => x.StartsWith(VERSION_PREFIX, StringComparison.Ordinal))
                .TrimStart(VERSION_PREFIX);
        }

        public async Task<IList<RegistryInfo>> FindServiceInstancesAsync()
        {
            return await FindServiceInstancesAsync(nameTagsPredicate: x => true, RegistryInfoPredicate: x => true);
        }

        public async Task<IList<RegistryInfo>> FindServiceInstancesAsync(string name)
        {
            var queryResult = await _consul.Health.Service(name, tag: "", passingOnly: true);
            var instances = queryResult.Response.Select(serviceEntry => new RegistryInfo
            {
                Name = serviceEntry.Service.Service,
                Address = serviceEntry.Service.Address,
                Port = serviceEntry.Service.Port,
                Version = GetVersionFromStrings(serviceEntry.Service.Tags),
                Tags = serviceEntry.Service.Tags ?? Enumerable.Empty<string>(),
                Id = serviceEntry.Service.ID
            });

            return instances.ToList();
        }

        public async Task<IList<RegistryInfo>> FindServiceInstancesWithVersionAsync(string name, string version)
        {
            var instances = await FindServiceInstancesAsync(name);
            var range = new SemVer.Range(version);

            return instances.Where(x => range.IsSatisfied(x.Version)).ToArray();
        }

        private async Task<IDictionary<string, string[]>> GetServicesCatalogAsync()
        {
            var queryResult = await _consul.Catalog.Services(); // local agent datacenter is implied
            var services = queryResult.Response;

            return services;
        }

        public async Task<IList<RegistryInfo>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> nameTagsPredicate, Predicate<RegistryInfo> RegistryInfoPredicate)
        {
            return (await GetServicesCatalogAsync())
                .Where(kvp => nameTagsPredicate(kvp))
                .Select(kvp => kvp.Key)
                .Select(FindServiceInstancesAsync)
                .SelectMany(task => task.Result)
                .Where(x => RegistryInfoPredicate(x))
                .ToList();
        }

        public async Task<IList<RegistryInfo>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> predicate)
        {
            return await FindServiceInstancesAsync(nameTagsPredicate: predicate, RegistryInfoPredicate: x => true);
        }

        public async Task<IList<RegistryInfo>> FindServiceInstancesAsync(Predicate<RegistryInfo> predicate)
        {
            return await FindServiceInstancesAsync(nameTagsPredicate: x => true, RegistryInfoPredicate: predicate);
        }

        public async Task<IList<RegistryInfo>> FindAllServicesAsync()
        {
            var queryResult = await _consul.Agent.Services();
            var instances = queryResult.Response.Select(serviceEntry => new RegistryInfo
            {
                Name = serviceEntry.Value.Service,
                Id = serviceEntry.Value.ID,
                Address = serviceEntry.Value.Address,
                Port = serviceEntry.Value.Port,
                Version = GetVersionFromStrings(serviceEntry.Value.Tags),
                Tags = serviceEntry.Value.Tags
            });

            return instances.ToList();
        }

        public async Task<RegistryInfo> RegisterServiceAsync(string serviceName, string version, Uri uri, Uri healthCheckUri = null, IEnumerable<string> tags = null, int checkSeconds = 30)
        {
            var serviceId = serviceName;
            string check = healthCheckUri?.ToString() ?? $"{uri}".TrimEnd('/') + "/health";

            string versionLabel = $"{VERSION_PREFIX}{version}";
            var tagList = (tags ?? Enumerable.Empty<string>()).ToList();
            tagList.Add(versionLabel);

            var registration = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = serviceName,
                Tags = tagList.ToArray(),
                Address = uri.Host,
                Port = uri.Port,
                Check = new AgentServiceCheck { HTTP = check, Interval = TimeSpan.FromSeconds(checkSeconds) }
            };
            await _consul.Agent.ServiceRegister(registration);

            return new RegistryInfo
            {
                Name = registration.Name,
                Id = registration.ID,
                Address = registration.Address,
                Port = registration.Port,
                Version = version,
                Tags = tagList
            };
        }

        public async Task<bool> DeregisterServiceAsync(string serviceId)
        {
            var writeResult = await _consul.Agent.ServiceDeregister(serviceId);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            return isSuccess;
        }

        private string GetCheckId(string serviceId, Uri uri)
        {
            return $"{serviceId}_{uri.GetPath().Replace("/", "")}";
        }

        public async Task<string> RegisterHealthCheckAsync(string serviceName, string serviceId, Uri checkUri, TimeSpan? interval = null, string notes = null)
        {
            if (checkUri == null)
            {
                throw new ArgumentNullException(nameof(checkUri));
            }

            var checkId = GetCheckId(serviceId, checkUri);
            var checkRegistration = new AgentCheckRegistration
            {
                ID = checkId,
                Name = serviceName,
                Notes = notes,
                ServiceID = serviceId,
                HTTP = checkUri.ToString(),
                Interval = interval
            };
            var writeResult = await _consul.Agent.CheckRegister(checkRegistration);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            return checkId;
        }

        public async Task<bool> DeregisterHealthCheckAsync(string checkId)
        {
            var writeResult = await _consul.Agent.CheckDeregister(checkId);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            return isSuccess;
        }
    }
}
