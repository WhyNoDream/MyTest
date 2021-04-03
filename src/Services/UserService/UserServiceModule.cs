using Applicatiion.UserService;
using Applicatiion.UserServiceContracts;
using Domain.User;
using EFCore.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace UserService
{
    //typeof(UserDoaminModule), typeof(UserEFCoreModule), typeof(UserServiceContractsModule), 
    [DependsOn(typeof(UserApplicationServiceModule)
    )]
    public class UserServiceModule : AbpModule 
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<UserServiceModule>();
            });
           // base.ConfigureServices(context);
        }
    }
}
