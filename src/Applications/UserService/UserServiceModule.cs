using ABPUnit;
using System;
using System.Collections.Generic;
using System.Text;
using UserDomain;
using Volo.Abp.Modularity;

namespace UserService
{
    [DependsOn(typeof(UserDoaminModule))]
    public class UserServiceModule : BaseModule
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
