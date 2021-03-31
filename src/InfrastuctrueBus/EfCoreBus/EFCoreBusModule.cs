using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EfCoreBus
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    public class EFCoreBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProductDbContext>();

            //...
        }
    }
}
