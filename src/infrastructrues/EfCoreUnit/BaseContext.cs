using CommonUnit.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreUnit
{
    public class BaseContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //配置连接字符串
            optionsBuilder.UseMySql(ConfigManagerConf.GetValue("ConnectionStrings:Default"));
        }
    }
}
