using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsulUnit
{
     public static class SimpConsulConf
    {
        public static void SetConsul(IConfiguration Configuration)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(Configuration["ServiceDiscovery:Consul:HttpEndpoint"]);
            });

            // 2、获取服务内部地址



            // 3、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = Configuration["ServiceDiscovery:ServiceName"],
                Address = Configuration["ServiceDiscovery:BindAddress"],
                Port = int.Parse(Configuration["ServiceDiscovery:Port"]),
                Tags = new string[] { "qqq", "www" },
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = $"http://{Configuration["ServiceDiscovery:BindAddress"]}:{Configuration["ServiceDiscovery:Port"]}/api/Health/Check",
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(3),
                }
            };

            var allService = consulClient.Catalog.Service(Configuration["ServiceDiscovery:ServiceName"]).GetAwaiter().GetResult();
            if (!allService.Response.Any(x => x.Address == registration.Address && x.ServicePort == registration.Port))
            {
                // 4、注册服务
                Console.WriteLine($"注册的serviceid：{registration.ID}");
                consulClient.Agent.ServiceRegister(registration).Wait();
            }

            // 5、关闭连接
            consulClient.Dispose();
        }
    }
}
