using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SwaggerUnits.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwaggerUnits.Extensions
{
    public static class AddSwaggerExtension
    {
        public static IServiceCollection AddSwagger(
                 this IServiceCollection services,
                 Action<SecurityOptions> action = null,
                 Type codeEnumType = null)
        {
            services.AddSwaggerGen(c =>
            {
                var swaggerOptions = new SwaggerOptions();
                SwaggerConf.swaggerOptions(swaggerOptions);
                services.Configure(SwaggerConf.swaggerOptions);
                swaggerOptions.OpenApiInfos
                    .ToList().
                    ForEach(info =>
                    {
                        c.SwaggerDoc(info.Version, info);
                    });
                swaggerOptions.Files
                    .ToList()
                    .ForEach(xmlFile =>
                    {
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            c.IncludeXmlComments(xmlPath, true);
                        }
                    });
                //Bearer 的scheme定义
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    //参数添加在头部
                    In = ParameterLocation.Header,
                    //使用Authorize头部
                    Type = SecuritySchemeType.Http,
                    //内容为以 bearer开头
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                //把所有方法配置为增加bearer头部信息
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        new string[] {}
                    }
                };

                //注册到swagger中
                c.AddSecurityDefinition("bearerAuth", securityScheme);
                c.AddSecurityRequirement(securityRequirement);
            });

            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }


    }

}
