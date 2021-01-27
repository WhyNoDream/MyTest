using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonUnit.Config
{
    public static class SwaggerConf
    {
        public static IServiceCollection TradeberAddSwagger(
            this IServiceCollection services,
            Action<SwaggerOptions> options,
            Action<SecurityOptions> action = null,
            Type codeEnumType = null)
        {
            services.AddSwaggerGen(c =>
            {
                if (options == null)
                {
                    throw new ArgumentNullException(nameof(options));
                }
                var swaggerOptions = new SwaggerOptions();
                options(swaggerOptions);
                services.Configure(options);
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



    public class SecurityOptions
    {
        public SecurityOptions()
        {
            Name = "Bearer";

            OpenApiSecurityScheme = new OpenApiSecurityScheme
            {
                Description = "JWT Bearer 授权 \"Authorization:     Bearer+空格+token\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            };
        }

        public string Name { get; set; }

        public OpenApiSecurityScheme OpenApiSecurityScheme { get; set; }

        public IEnumerable<string> Scopes { get; set; }
    }

    public class SwaggerOptions
    {
        public IEnumerable<OpenApiInfo> OpenApiInfos { get; set; }

        public IEnumerable<string> Files { get; set; }
    }
}
