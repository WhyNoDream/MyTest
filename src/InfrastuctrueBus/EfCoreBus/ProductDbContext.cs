using CommonUnit.Config;
using EfCoreUnit;
using Microsoft.EntityFrameworkCore;
using ProductDomain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreBus
{
    public class ProductDbContext: BaseContext
    {
		//public DbSet<Blog> Blogs { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(pc => pc.ToTable("t_tradeber_account_admin").HasKey(k => k.Id));
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//配置连接字符串
			optionsBuilder.UseMySql(ConfigManagerConf.GetValue("ConnectionStrings:Product"));
		}
	}
}
