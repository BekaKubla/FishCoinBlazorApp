using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace FishCoinBlazorApp.Generator
{
    public class OrderNumberGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            return $"FC-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}
