using Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Applicatiion.UserServiceContracts
{
    [DependsOn(typeof(UserDoaminModule))]
    public class UserServiceContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            //...
        }
    }
}
