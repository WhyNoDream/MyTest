using Microsoft.AspNetCore.Builder;
using SwaggerUnits.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwaggerUnits.Extensions
{
    public static class SwaggerConfigExtension
    {
        public static IApplicationBuilder UseSwaggerConf(
          this IApplicationBuilder app
              ) => app.UseSwagger()
          .UseSwaggerUI(c =>
          {
              var swaggerOptions = new SwaggerOptions();
              SwaggerConf.swaggerOptions(swaggerOptions);
                //ApiVersions为自定义的版本枚举
                swaggerOptions.OpenApiInfos
                  .ToList()
                  .ForEach(info =>
                  {
                        //版本控制
                        c.SwaggerEndpoint($"/swagger/{info.Version}/swagger.json", $"{info.Title} {info.Version}");
                  });
          });
    }
}
