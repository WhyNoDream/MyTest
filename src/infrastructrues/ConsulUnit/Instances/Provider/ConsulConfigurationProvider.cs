﻿using Consul;
using ConsulUnit.Entitys;
using ConsulUnit.ValueObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsulUnit.Instances.Provider
{
    public class ConsulConfigurationProvider : ConfigurationProvider
    {
        private const string ConsulIndexHeader = "X-Consul-Index";
        private readonly IEnumerable<string> _paths;
        private readonly HttpClient _httpClient;
        private readonly ConsulClient _consulClient;
        private readonly Uri _consulUrl;
        private readonly Task _configurationListeningTask;
        private int _consulUrlIndex;
        private int _failureCount;
        private int _consulConfigurationIndex;
        public ConsulConfigurationProvider(Uri consulUrl, IEnumerable<string> paths)
        {
            _paths = paths;
            _consulUrl = consulUrl;
            if (_consulUrl == null)
            {
                throw new ArgumentOutOfRangeException(nameof(consulUrl));
            }

            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }, true);

            _consulClient = new ConsulClient(config =>
            {
                config.Address = consulUrl;
            });
            _configurationListeningTask = new Task(ListenToConfigurationChanges);
        }
        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        private async Task LoadAsync()
        {
            var keyValues = await ExecuteQueryAsync();
            Data = await GetServiceUrls(keyValues);

            if (_configurationListeningTask.Status == TaskStatus.Created)
                _configurationListeningTask.Start();
        }
        private async void ListenToConfigurationChanges()
        {
            while (true)
            {
                try
                {

                    await Task.Delay(TimeSpan.FromMinutes(10));
                    Data = await ExecuteQueryAsync(true);
                    OnReload();
                }
                catch (TaskCanceledException)
                {
                    _failureCount = 0;
                }
            }
        }
        private async Task<Dictionary<string, string>> ExecuteQueryAsync(bool isBlocking = false)
        {
            var keyValues = new Dictionary<string, string>();
            foreach (var path in _paths)
            {
                if (string.IsNullOrEmpty(path)) continue;
                var consulUrl = new Uri(_consulUrl, $"v1/kv/{path}");
                var requestUri = isBlocking ? $"?recurse=true&index={_consulConfigurationIndex}" : "?recurse=true";
                
                using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(consulUrl, requestUri));
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.Headers.Contains(ConsulIndexHeader))
                {
                    var indexValue = response.Headers.GetValues(ConsulIndexHeader).FirstOrDefault();
                    int.TryParse(indexValue, out _consulConfigurationIndex);
                }
                var tokens = JsonConvert.DeserializeObject<List<ConsulConfigurationEnitiy>>(await response.Content.ReadAsStringAsync());
                foreach (var keyValue in tokens
                    .Select(k => new KeyValuePair<string, JToken>
                    (
                      k.Key.Substring(path.Length),
                      k.Value != null ? JToken.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(k.Value))) : null
                    ))
                  .Where(v => !string.IsNullOrWhiteSpace(v.Key))
                  .SelectMany(Flatten)
                  .ToDictionary(v => ConfigurationPath.Combine(v.Key.Split('/')), v => v.Value, StringComparer.OrdinalIgnoreCase))
                {
                    keyValues.Add(keyValue.Key, keyValue.Value);
                }
            }
            return keyValues;
        }

        private async Task<Dictionary<string, string>> GetServiceUrls(Dictionary<string, string> keyValues)
        {
            var queryResult = await _consulClient.Agent.Services();
            var instances = queryResult.Response.Select(serviceEntry => new RegistryInfo
            {
                Name = serviceEntry.Value.Service,
                Id = serviceEntry.Value.ID,
                Address = serviceEntry.Value.Address,
                Port = serviceEntry.Value.Port,
                Tags = serviceEntry.Value.Tags
            });
            foreach (var service in instances.Select(x => x.Name).Distinct())
            {
                var addresses = instances.Where(x => x.Name.Equals(service)).Select(x => $"{x.Address}:{x.Port}").Distinct().ToList();
                if (addresses != null)
                {
                    keyValues.Add($"{ConsulConsts.ConsulServiceUrl}:{service}", JsonConvert.SerializeObject(addresses));
                }
            }

            return keyValues;
        }
        private static IEnumerable<KeyValuePair<string, string>> Flatten(KeyValuePair<string, JToken> tuple)
        {
            if (!(tuple.Value is JObject value))
            {
                if (tuple.Value is JArray values)
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        foreach (var item in Flatten(new KeyValuePair<string, JToken>($"{tuple.Key}:{i}", values[i])))
                            yield
                                return item;
                    }
                }
                else
                {
                    var propertyKey = $"{tuple.Key}";
                    var str = tuple.Value.Value<string>();
                    yield
                        return new KeyValuePair<string, string>(propertyKey, str);
                }
            }
            else
            {
                foreach (var property in value)
                {
                    var propertyKey = $"{tuple.Key}/{property.Key}";
                    switch (property.Value.Type)
                    {
                        case JTokenType.Object:
                            foreach (var item in Flatten(new KeyValuePair<string, JToken>(propertyKey, property.Value)))
                                yield
                                    return item;
                            break;
                        case JTokenType.Array:
                            if (property.Value is JArray values)
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    foreach (var item in Flatten(new KeyValuePair<string, JToken>($"{propertyKey}:{i.ToString()}", values[i])))
                                        yield
                                            return item;
                                }
                            }
                            break;
                        default:
                            yield
                                return new KeyValuePair<string, string>(propertyKey, property.Value.Value<string>());
                            break;
                    }
                }
            }
        }
    }
}
