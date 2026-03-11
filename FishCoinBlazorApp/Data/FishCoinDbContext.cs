using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FishCoinBlazorApp.Data
{
    public class FishCoinDbContext : DbContext
    {
        public FishCoinDbContext(DbContextOptions<FishCoinDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
