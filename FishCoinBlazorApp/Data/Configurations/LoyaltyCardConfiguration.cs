using FishCoinBlazorApp.Entites.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishCoinBlazorApp.Data.Configurations
{
    public class LoyaltyCardConfiguration : IEntityTypeConfiguration<LoyaltyCard>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCard> builder)
        {
            builder.HasKey(lc => lc.Id);

            builder.Property(lc => lc.CardNumber)
                .IsRequired()
                .HasMaxLength(20);

            // ერთი-ერთზე კავშირი იუზერსა და ბარათს შორის
            builder.HasOne(lc => lc.User)
                .WithOne(u => u.LoyaltyCard)
                .HasForeignKey<LoyaltyCard>(lc => lc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
