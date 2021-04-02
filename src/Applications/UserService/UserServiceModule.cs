using ABPUnit;
using Domain.User;
using EFCore.User;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Applicatiion.UserService
{
    [DependsOn(typeof(UserDoaminModule), typeof(UserEFCoreModule))]
    public class UserServiceModule : BaseModule
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
