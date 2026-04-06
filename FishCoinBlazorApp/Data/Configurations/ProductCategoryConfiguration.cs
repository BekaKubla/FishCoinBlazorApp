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

            #region Spinning (SubCategoryId = 1)
                new ProductCategory { Id = 1, ProductCategoryName = "ჯოხი", SubCategoryId = 1 },
                new ProductCategory { Id = 2, ProductCategoryName = "კოჭა", SubCategoryId = 1 },
                new ProductCategory { Id = 3, ProductCategoryName = "ნემსკავი", SubCategoryId = 1 },
                new ProductCategory { Id = 4, ProductCategoryName = "ძუა/წნული/ფლურო", SubCategoryId = 1 },
                new ProductCategory { Id = 5, ProductCategoryName = "ვობლერი", SubCategoryId = 1 },
                new ProductCategory { Id = 6, ProductCategoryName = "ტრიალა", SubCategoryId = 1 },
                new ProductCategory { Id = 7, ProductCategoryName = "ყანყალა", SubCategoryId = 1 },
                new ProductCategory { Id = 8, ProductCategoryName = "ჯიგთავი", SubCategoryId = 1 },
                new ProductCategory { Id = 9, ProductCategoryName = "სილიკონი", SubCategoryId = 1 },
                new ProductCategory { Id = 10, ProductCategoryName = "აქსესუარები", SubCategoryId = 1 },
            #endregion

            #region Feeder (SubCategoryId = 2)
                new ProductCategory { Id = 11, ProductCategoryName = "ჯოხი", SubCategoryId = 2 },
                new ProductCategory { Id = 12, ProductCategoryName = "კოჭა", SubCategoryId = 2 },
                new ProductCategory { Id = 13, ProductCategoryName = "ნემსკავი/სადავე", SubCategoryId = 2 },
                new ProductCategory { Id = 14, ProductCategoryName = "ძუა/წნული", SubCategoryId = 2 },
                new ProductCategory { Id = 15, ProductCategoryName = "დასაკვები/დანამატები", SubCategoryId = 2 },
                new ProductCategory { Id = 16, ProductCategoryName = "საკვებურა", SubCategoryId = 2 },
                new ProductCategory { Id = 17, ProductCategoryName = "აღჭურვილობა (სკამი/ბადე)", SubCategoryId = 2 },
                new ProductCategory { Id = 18, ProductCategoryName = "აქსესუარები", SubCategoryId = 2 },
            #endregion

            #region Floating (SubCategoryId = 3)
                new ProductCategory { Id = 19, ProductCategoryName = "ჯოხი", SubCategoryId = 3 },
                new ProductCategory { Id = 20, ProductCategoryName = "კოჭა", SubCategoryId = 3 },
                new ProductCategory { Id = 21, ProductCategoryName = "ტივტივა", SubCategoryId = 3 },
                new ProductCategory { Id = 22, ProductCategoryName = "ნემსკავი/ძუა/სიმძიმე", SubCategoryId = 3 },
                new ProductCategory { Id = 23, ProductCategoryName = "აქსესუარები", SubCategoryId = 3 }
                #endregion
);
        }
    }
}
