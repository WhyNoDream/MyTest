using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace ABPUnit
{
    public class BaseModule: AbpModule
    {
        //服务配置前
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
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
