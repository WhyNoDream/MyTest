using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        private const string ServerAddress = "http://*:8085";
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls();
                })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                //ͨ�������������������ļ�
                config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddEnvironmentVariables();
            });
    }
}
