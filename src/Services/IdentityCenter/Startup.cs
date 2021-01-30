using CommonUnit.Config;
using ConsulUnit;
using ConsulUnit.Extensions;
using IdentityCenter.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedisUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigManagerConf.SetConfiguration(Configuration);

            services.AddIdentityServer() // 注册IdentityServer4
                .AddDeveloperSigningCredential() // 采用开发者凭证
                .AddInMemoryApiResources(AuthConfig.GetApiResources()) // 添加Api资源
                .AddInMemoryClients(AuthConfig.GetClients()) // 添加客户端
                .AddInMemoryIdentityResources(AuthConfig.GetIdentityResources()) // 添加身份资源
                .AddResourceOwnerValidator<ResourceOwnerValidator>();  //身份验证

            services.AddCsRedisConf();
            services.AddServiceDiscoveryOptions(Configuration);
            services.AddConsulService(Configuration);
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用consul
            app.UseConsul();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
