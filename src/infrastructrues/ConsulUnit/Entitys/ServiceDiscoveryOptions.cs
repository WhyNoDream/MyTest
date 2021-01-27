using System;
using System.Collections.Generic;
using System.Text;

namespace ConsulUnit
{
    public class ServiceDiscoveryOptions
    {
        public string ServiceName { get; set; }
        public string Namespace { get; set; }
        public string Version { get; set; }
        public string ConnectionStringName { get; set; }
        public string HealthCheckTemplate { get; set; }
        public string[] Endpoints { get; set; }
    }
}
