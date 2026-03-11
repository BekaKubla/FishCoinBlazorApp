using FishCoinBlazorApp.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishCoinBlazorApp.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.HasOne(x => x.ProductCategory).WithMany(x => x.Products).HasForeignKey(x => x.ProductCategoryId);
        }
    }
}
