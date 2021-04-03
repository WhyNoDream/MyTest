using ABPUnit;
using Applicatiion.UserServiceContracts;
using Domain.User;
using EFCore.User;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Applicatiion.UserService
{
    [DependsOn(typeof(UserDoaminModule), typeof(UserEFCoreModule), typeof(UserServiceContractsModule))]
    public class UserApplicationServiceModule : AbpModule
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<UserApplicationServiceModule>();
            });
           // base.ConfigureServices(context);
        }
    }
}
