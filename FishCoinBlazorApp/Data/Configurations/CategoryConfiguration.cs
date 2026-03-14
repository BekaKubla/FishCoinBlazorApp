using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FishCoinBlazorApp.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CategoryName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasData(
                new Category { Id = 1, CategoryName = "სათევზაო" },
                new Category { Id = 2, CategoryName = "სანადირო" },
                new Category { Id = 3, CategoryName = "საჩუქარი" }
            );
        }
    }
}
