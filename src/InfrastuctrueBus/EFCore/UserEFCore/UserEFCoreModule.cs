using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EFCore.User
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    public class UserEFCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<UserDbContext>();

            //...
        }
    }
}
