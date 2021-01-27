using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConsulUnit.Extensions
{
    public static class HostingExtensions
    {
        /// <summary>
        /// 添加consul配置
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseConsul(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var configuration = hostingContext.Configuration;
                configuration = config.Build();
                var url = configuration.GetValue<Uri>("consul:url");
                if (url != null)
                {
                    var consulUrl = configuration.GetValue<Uri>("consul:url");
                    var consulPahs = configuration.GetSection("consul:path").Get<List<string>>();
                    config.AddConsulBuilder(consulUrl, consulPahs);
                }

                config.Build();
            });
            return builder;
        }
    }
}
