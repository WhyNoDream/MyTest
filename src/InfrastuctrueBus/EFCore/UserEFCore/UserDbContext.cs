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
        public virtual DbSet<Domain.User.Entitys.User> UserDB { get; set; }
        public virtual DbSet<Domain.User.Entitys.UserRoles> UserRolesDB { get; set; }
        public virtual DbSet<Domain.User.Entitys.Department> DepartmentDB { get; set; }
        public virtual DbSet<Domain.User.Entitys.Role> RoleDB { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Domain.User.Entitys.User>(b =>
            {
                b.ToTable("t_user");
                b.ConfigureByConvention();
                b.Property(x => x.Id).IsRequired().HasMaxLength(20);
               // b.HasMany(o => o.UserRole).WithOne().HasForeignKey(o => o.UserId);
            });

            builder.Entity<Domain.User.Entitys.UserRoles>(b =>
            {
                b.ToTable("t_user_roles");
                b.ConfigureByConvention();
                b.Property(x => x.Id).IsRequired().HasMaxLength(20);
            });

            //builder.Entity<User>().OwnsOne(p => p.UserRole
            //  , of =>
            //  {
            //      of.Property(p => p.ProductId).HasColumnName("ProductId");
            //      of.Property(p => p.SkuId).HasColumnName("SkuId");
            //      of.OwnsOne(oo => oo.OriginalSize, ooItem =>
            //      {
            //          ooItem.Property(p => p.OriginalWeight).HasColumnName("OriginalWeight");
            //          ooItem.Property(p => p.OriginalLength).HasColumnName("OriginalLength");
            //          ooItem.Property(p => p.OriginalWidth).HasColumnName("OriginalWidth");
            //          ooItem.Property(p => p.OriginalHeight).HasColumnName("OriginalHeight");
            //      });
            //  });

        }

    }
}
