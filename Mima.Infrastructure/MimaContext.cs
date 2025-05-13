using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mima.Domain.Model;

namespace Mima.Infrastructure
{
    public class MimaContext : IdentityDbContext<User>

    {
        public MimaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SaleProduct> SaleProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired(false);

            builder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sales)
                .WithMany(s => s.SalesProducts)
                .HasForeignKey(sp => sp.SalesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);
        }

    }
}
