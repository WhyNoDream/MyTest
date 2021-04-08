using CommonUnit.Config;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerUnits.Entity
{
    public static class SwaggerConf
    {
        public static Action<SwaggerOptions> swaggerOptions =
         options =>
         {
             options.OpenApiInfos = new List<OpenApiInfo>
             {
                    new OpenApiInfo
                    {
                        Title =ConfigManagerConf.GetValue("Swagger:Title"),
                        Version =ConfigManagerConf.GetValue("Swagger:Version"),
                        Description = ConfigManagerConf.GetValue("Swagger:Description")
                    }
             };
             options.Files = ConfigManagerConf.GetValue("Swagger:Files")?.Split(',');
         };
    }
}
