using CommonUnit.Config;
using Microsoft.EntityFrameworkCore;
using ProductDomain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EfCoreBus
{
	[ConnectionStringName("product")]
	public class ProductDbContext:AbpDbContext<ProductDbContext>
	{
		public ProductDbContext(DbContextOptions<ProductDbContext> options)
		  : base(options)
		{
		}
		public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>(b =>
            {
                b.ToTable("t_product");
                b.ConfigureByConvention();
            });
        }

    }
}
