using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerUnits.Entity
{
    public class SwaggerOptions
    {
        public IEnumerable<OpenApiInfo> OpenApiInfos { get; set; }

        public IEnumerable<string> Files { get; set; }
    }
}
