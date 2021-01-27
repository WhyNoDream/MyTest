using ConsulUnit.Instances.Provider;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsulUnit.Instances
{
    public class ConsulConfigurationSource : IConfigurationSource
    {
        public Uri ConsulUrl { get; }
        public List<string> Paths { get; }
        public ConsulConfigurationSource(Uri consulUrl, List<string> consulPaths)
        {
            ConsulUrl = consulUrl;
            Paths = consulPaths;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(ConsulUrl, Paths);
        }
    }
}
