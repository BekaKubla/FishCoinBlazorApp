using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishCoinBlazorApp.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            // Primary Key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProductCategoryName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(x => x.SubCategory).WithMany(x => x.ProductCategories).HasForeignKey(x => x.SubCategoryId);

            builder.HasData(
                new ProductCategory { Id = 1, ProductCategoryName = "ჯოხები", SubCategoryId = 1 },
                new ProductCategory { Id = 2, ProductCategoryName = "კოჭები", SubCategoryId = 1 },
                new ProductCategory { Id = 3, ProductCategoryName = "სატყუარები", SubCategoryId = 1 },
                new ProductCategory { Id = 4, ProductCategoryName = "აქსესუარები", SubCategoryId = 1 }
            );
        }
    }
}
