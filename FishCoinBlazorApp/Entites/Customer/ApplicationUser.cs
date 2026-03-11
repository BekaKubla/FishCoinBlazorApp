
using FishCoinBlazorApp.Entites.Product;
using Microsoft.AspNetCore.Identity;

namespace FishCoinBlazorApp.Entites.Customer
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }

        // კავშირი ლოიალობის ბარათთან (ერთი იუზერი - ერთი ბარათი)
        public virtual LoyaltyCard LoyaltyCard { get; set; }

        // კავშირი შეკვეთებთან (ერთი იუზერი - ბევრი შეკვეთა)
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
