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

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Property(p => p.CostPrice)
                .HasPrecision(18, 2);

            // ავტომატური TagNumber-ის გენერაცია (SQL Server-ისთვის)
            // RIGHT('0000' + CAST(Id AS VARCHAR), 4) ნიშნავს:
            // აიღე Id, გადააქციე ტექსტად, წინ მიუწერე ნულები და ბოლოდან დატოვე 4 სიმბოლო.
            builder.Property(p => p.TagNumber)
                .HasComputedColumnSql("RIGHT('0000' + CAST(Id AS AS VARCHAR(10)), 4)");

            // კავშირი კატეგორიასთან
            builder.HasOne(p => p.ProductCategory)
                .WithMany(p=>p.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
