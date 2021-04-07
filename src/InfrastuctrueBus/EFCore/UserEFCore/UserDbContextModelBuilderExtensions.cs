using Domain.User.Entitys;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EFCore.User
{
    public static class UserDbContextModelBuilderExtensions
    {
        public static void ConfigureUserDB(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Domain.User.Entitys.User>(b =>
            {
                b.ToTable("t_user");
                b.ConfigureByConvention();
                b.HasMany<UserRoles>().WithOne().HasForeignKey(o => o.UserId);
            });

            builder.Entity<Domain.User.Entitys.UserRoles>(b =>
            {
                b.ToTable("t_user_roles");
                b.ConfigureByConvention();
            });
        }
    }
}