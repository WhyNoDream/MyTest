using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQInfrastructrue
{
    public static class RabbitMQMiddlewareExtensions
    {
        public static async Task<IApplicationBuilder> UseRabbitMQ(this IApplicationBuilder builder)
        {
            return builder;
        }
    }
}
