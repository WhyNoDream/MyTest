using Domain.User.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EFCore.User
{
    [ConnectionStringName("Default")]
    public class UserDbContext : AbpDbContext<UserDbContext>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
        {
        }
        public virtual DbSet<Domain.User.Entitys.User> User { get; set; }
        public virtual DbSet<Domain.User.Entitys.UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //配置映射
            builder.ConfigureUserDB();

            //builder.Entity<Domain.User.Entitys.User>(b =>
            //{
            //    b.ToTable("t_user");
            //    b.ConfigureByConvention();
            //    b.HasMany<UserRoles>().WithOne().HasForeignKey(o => o.UserId);
            //});

            //builder.Entity<Domain.User.Entitys.UserRoles>(b =>
            //{
            //    b.ToTable("t_user_roles");
            //    b.ConfigureByConvention();
            //});
        }

    }
}
