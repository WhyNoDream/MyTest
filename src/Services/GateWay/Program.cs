using ConsulUnit.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWay
{
    public class Program
    {
        private const string ServerAddress = "http://*:8081";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                          .AddJsonFile("appsettings.json", true, true)
                         .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                         .AddJsonFile($"ocelot.json", true, true)
                         .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                         .AddEnvironmentVariables();
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseUrls(ServerAddress);
                    //webBuilder.UseKestrel();
                    webBuilder.UseConsul();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
