using CommonConfBus;
using CommonUnit.Config;
using EfCoreBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                //���Կ�ֵ
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            //�����ʼ��
            services.WebServiceExtensions(Configuration);

            services.AddAbpDbContext<ProductDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });
            //services.AddDbContextPool<ProductDbContext>(options => options.UseMySql(ConfigManagerConf.GetValue("ConnectionStrings:product"), b => b.MigrationsAssembly("ProductService"))); //����mariadb�����ַ���

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���ó�ʼ��
            app.WebConfigExtensions();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}