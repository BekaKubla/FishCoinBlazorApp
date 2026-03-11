using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Generator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishCoinBlazorApp.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalAmountGEL)
                .HasPrecision(18, 2);

            builder.Property(o => o.OrderNumber)
                .HasMaxLength(30)
                .HasValueGenerator<OrderNumberGenerator>();

            // Enum-ების კონვერტაცია
            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(o => o.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(50);

            // კავშირი OrderItems-თან
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ამის დამატება გჭირდება OrderConfiguration-ის Configure მეთოდში:
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // იუზერის წაშლისას ორდერები არ წაიშალოს (ისტორიისთვის)
        }
    }
}
