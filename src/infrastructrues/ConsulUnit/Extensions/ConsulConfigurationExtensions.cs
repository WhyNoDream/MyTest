using ConsulUnit.Instances;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsulUnit.Extensions
{
    public static class ConsulConfigurationExtensions
    {
        public static IConfigurationBuilder AddConsulBuilder(this IConfigurationBuilder configurationBuilder, Uri consulUrl, List<string> consulPaths)
        {
            configurationBuilder.Add(new ConsulConfigurationSource(consulUrl, consulPaths));
            return configurationBuilder;
        }
    }
}
