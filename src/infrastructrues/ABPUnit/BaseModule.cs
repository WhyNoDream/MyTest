using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Volo.Abp.Modularity;

namespace ABPUnit
{
    public class BaseModule: AbpModule
    {
        //服务配置前
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddMediatR(Assembly.GetExecutingAssembly());
            //...
            // base.PreConfigureServices(context);
        }
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            // base.ConfigureServices(context);

        }
        //服务配置后
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
           // base.PostConfigureServices(context);
        }

    }

}
