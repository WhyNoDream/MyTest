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

            services.AddIdentityServer() // ע��IdentityServer4
                .AddDeveloperSigningCredential() // ���ÿ�����ƾ֤
                .AddInMemoryApiResources(AuthConfig.GetApiResources()) // ���Api��Դ
                .AddInMemoryClients(AuthConfig.GetClients()) // ��ӿͻ���
                .AddInMemoryIdentityResources(AuthConfig.GetIdentityResources()) // ��������Դ
                .AddResourceOwnerValidator<ResourceOwnerValidator>();  //�����֤

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
            //����consul
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
