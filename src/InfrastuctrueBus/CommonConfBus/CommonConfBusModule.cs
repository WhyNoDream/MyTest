using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CommonConfBus
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class CommonConfBusModule: AbpModule
    {

    }
}
