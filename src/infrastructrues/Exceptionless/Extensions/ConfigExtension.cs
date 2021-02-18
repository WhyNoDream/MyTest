using CommonUnit.Config;
using Exceptionless;
using ExceptionlessUnit.Instances;
using ExceptionlessUnit.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionlessUnit.Extensions
{
    public static class ConfigExtension
    {
        public static IApplicationBuilder UseExceptionLessLog(this IApplicationBuilder app)
        {
            app.UseExceptionless(ConfigManagerConf.GetValue("Exceptionless:ApiKey"));
            var submittingEvent = app.ApplicationServices.GetRequiredService<ISubmittingEvent>();
            ExceptionlessClient.Default.SubmittingEvent += submittingEvent.OnSubmittingEvent;
            return app;
        }
    }
}
