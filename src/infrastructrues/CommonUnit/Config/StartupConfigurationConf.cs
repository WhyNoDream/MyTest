using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUnit.Config
{
    public static class StartupConfigurationConf
    {
        public static IServiceCollection Conf(this IServiceCollection services)
        {
            return services;
        }
    }
}
