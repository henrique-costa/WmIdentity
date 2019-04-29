using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Models;

namespace WmIdentity.Services
{
    public class WmIdentityDbContext : IdentityDbContext<MyUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }

        public WmIdentityDbContext(DbContextOptions<WmIdentityDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductSubcategory>().ToTable("ProductSubcategories");

            modelBuilder.Entity<ProductSubcategory>()
                .HasKey(s => new { s.SubCategoryId, s.ProductId });


        }
    }
}
