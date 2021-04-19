using CommonUnit.Config;
using ConsulUnit;
using ConsulUnit.Extensions;
using ExceptionlessUnit.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQInfrastructrue;
using SwaggerUnits.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonConfBus
{
    public static class WebConf
    {
        public static IServiceCollection WebServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigManagerConf.SetConfiguration(configuration);
            services.AddSwagger();
            services.AddServiceDiscoveryOptions();
            services.AddConsulService();
            services.AddExceptionLessLog();
            services.AddRabbitMQ();
            return services;
        }
        public static IApplicationBuilder WebConfigExtensions(this IApplicationBuilder app)
        {
            app.UseExceptionLessLog();
            app.UseSwaggerConf();
            app.UseConsul();
            
            return app;
        }
    }
}
