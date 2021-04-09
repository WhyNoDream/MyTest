using ABPUnit;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace ABPEFCoreMySqlUnit
{
    [DependsOn(typeof(BaseModule))]//
    public class ABPEFCoreMySqlUnitModule : AbpModule
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
             base.ConfigureServices(context);
        }
    }
}
