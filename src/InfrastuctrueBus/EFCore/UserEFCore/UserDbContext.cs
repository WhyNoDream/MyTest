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
    [ConnectionStringName("user")]
    public class UserDbContext : AbpDbContext<UserDbContext>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
        {
        }
        public virtual DbSet<Domain.User.Entitys.User> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Domain.User.Entitys.User>(b =>
            {
                b.ToTable("t_user");
                b.ConfigureByConvention();
            });
            builder.Entity<UserRoles>(pc => pc.ToTable("t_user_roles").HasKey(k => k.Id));
            builder.Entity<Domain.User.Entitys.User>(pc => pc.ToTable("t_user").HasKey(k => k.Id));
            builder.Entity<Domain.User.Entitys.User>().HasMany(o => o.UserRole).WithOne().HasForeignKey(o => o.UserId);



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
