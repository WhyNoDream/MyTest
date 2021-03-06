﻿using Applicatiion.UserService;
using Applicatiion.UserServiceContracts;
using CommonConfBus;
using Domain.User;
using EFCore.User;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace UserService
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule),typeof(UserApplicationServiceModule), typeof(UserEFCoreModule)
    )]
    public class UserServiceModule : AbpModule 
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            base.ConfigureServices(context);
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            //配置自动映射
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<UserServiceModule>();
            });

            context.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                //忽略空值
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //服务初始化注入
            context.Services.WebServiceExtensions(configuration);

            //添加MediatR事件处理者所在程序集
            context.Services.AddMediatR(typeof(UserApplicationServiceModule).Assembly);

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            //配置初始化
            app.WebConfigExtensions();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
