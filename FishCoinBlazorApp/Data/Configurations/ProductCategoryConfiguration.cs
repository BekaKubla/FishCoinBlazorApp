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

            #region Carp (SubCategoryId = 2)
                new ProductCategory { Id = 11, ProductCategoryName = "ჯოხი", SubCategoryId = 2 },
                new ProductCategory { Id = 12, ProductCategoryName = "კოჭა", SubCategoryId = 2 },
                new ProductCategory { Id = 13, ProductCategoryName = "ნემსკავი/რიგები", SubCategoryId = 2 },
                new ProductCategory { Id = 14, ProductCategoryName = "ძუა/ლიდკორი", SubCategoryId = 2 },
                new ProductCategory { Id = 15, ProductCategoryName = "სვიველი/PVA/სიმძიმე", SubCategoryId = 2 },
                new ProductCategory { Id = 16, ProductCategoryName = "სატყუარა/ბოილი/პელეტსი", SubCategoryId = 2 },
                new ProductCategory { Id = 17, ProductCategoryName = "პოდი/სიგნალიზატორი", SubCategoryId = 2 },
                new ProductCategory { Id = 18, ProductCategoryName = "ინვენტარი (სკამი/ბადე)", SubCategoryId = 2 },
                new ProductCategory { Id = 19, ProductCategoryName = "აქსესუარები", SubCategoryId = 2 },
            #endregion

            #region Feeder (SubCategoryId = 3)
                new ProductCategory { Id = 20, ProductCategoryName = "ჯოხი", SubCategoryId = 3 },
                new ProductCategory { Id = 21, ProductCategoryName = "კოჭა", SubCategoryId = 3 },
                new ProductCategory { Id = 22, ProductCategoryName = "ნემსკავი/სადავე", SubCategoryId = 3 },
                new ProductCategory { Id = 23, ProductCategoryName = "ძუა/წნული", SubCategoryId = 3 },
                new ProductCategory { Id = 24, ProductCategoryName = "დასაკვები/დანამატები", SubCategoryId = 3 },
                new ProductCategory { Id = 25, ProductCategoryName = "საკვებურა", SubCategoryId = 3 },
                new ProductCategory { Id = 26, ProductCategoryName = "აღჭურვილობა (სკამი/ბადე)", SubCategoryId = 3 },
                new ProductCategory { Id = 27, ProductCategoryName = "აქსესუარები", SubCategoryId = 3 },
            #endregion

            #region Floating (SubCategoryId = 4)
                new ProductCategory { Id = 28, ProductCategoryName = "ჯოხი", SubCategoryId = 4 },
                new ProductCategory { Id = 29, ProductCategoryName = "კოჭა", SubCategoryId = 4 },
                new ProductCategory { Id = 30, ProductCategoryName = "ტივტივა", SubCategoryId = 4 },
                new ProductCategory { Id = 31, ProductCategoryName = "ნემსკავი/ძუა/სიმძიმე", SubCategoryId = 4 },
                new ProductCategory { Id = 32, ProductCategoryName = "აქსესუარები", SubCategoryId = 4 }
                #endregion
);
        }
    }
}
