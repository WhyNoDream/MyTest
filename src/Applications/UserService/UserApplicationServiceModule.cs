using ABPUnit;
using Applicatiion.UserServiceContracts;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Applicatiion.UserService
{
    [DependsOn(typeof(UserDoaminModule),  typeof(UserServiceContractsModule), typeof(AbpAutoMapperModule))]
    public class UserApplicationServiceModule : AbpModule
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<UserApplicationServiceModule>(validate: true);
                options.AddProfile<AutoMapperProfile>(validate: true);
            });
        }
    }
}
