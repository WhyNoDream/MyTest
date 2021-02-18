using ExceptionlessUnit.Instances;
using ExceptionlessUnit.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionlessUnit.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddExceptionLessLog(this IServiceCollection services)
        {
            services.TryAddSingleton<ILogger, ExceptionLessLogger>();
            services.TryAddSingleton<ISubmittingEvent, SubmittingEvent>();
            return services;
        }
    }
}
