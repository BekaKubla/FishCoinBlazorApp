using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishCoinBlazorApp.Data.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");

            // Primary Key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.SubCategoryName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(x => x.Category).WithMany(x => x.SubCategories).HasForeignKey(x => x.CategoryId);

            builder.HasData(
                new SubCategory { Id = 1, SubCategoryName = "სპინინგი", CategoryId = 1 },
                new SubCategory { Id = 2, SubCategoryName = "ფიდერი", CategoryId = 1 },
                new SubCategory { Id = 3, SubCategoryName = "ტივტივა", CategoryId = 1 },
                new SubCategory { Id = 4, SubCategoryName = "აქსესუარი", CategoryId = 1 },
                new SubCategory { Id = 5, SubCategoryName = "საჩუქარი", CategoryId = 1 }
            );
        }
    }
}
