using Domain.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace EFCore.User
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule), typeof(UserDoaminModule))]
    public class UserEFCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<UserDbContext>();
            //context.Services.AddAbpDbContext<UserDbContext>(options =>
            //{
            //    options.AddDefaultRepositories(includeAllEntities: true);
            //}) ;

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();
            });

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = "Server=localhost;port=3306;database=my-test-user;uid=root;password=admin;Convert Zero Datetime=True;Allow User Variables=True;pooling=true";
            });
            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Auto;
            });
            //base.ConfigureServices(context);
            //...
        }
    }
}
