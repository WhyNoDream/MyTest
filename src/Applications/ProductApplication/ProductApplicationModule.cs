using ABPUnit;
using ProductDomain;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace ProductApplication
{
    [DependsOn(typeof(ProductDomainModule))]
    public class ProductApplicationModule : BaseModule
    {
        //服务配置
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }


}
