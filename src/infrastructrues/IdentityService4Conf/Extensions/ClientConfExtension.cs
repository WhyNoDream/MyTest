using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService4Unit.Extensions
{
    public static class ClientConfExtension
    {
        public static IServiceCollection AddIdentityServer4Auth(this IServiceCollection services)
        {
            //services.AddHttpClient();
            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = $"http://localhost:8086";
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false
                        };
                    });
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "http://localhost:8086"; // issuer地址
            //    options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
            //    options.ApiName = "server4";
            //    options.RequireHttpsMetadata = false; // 启用http
            //});
            return services;
        }
    }
}
