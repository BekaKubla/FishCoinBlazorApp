using FishCoinBlazorApp.Entites.Customer;

namespace FishCoinBlazorApp.Entites.Product
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // გადახდის დეტალები
        public decimal TotalAmountGEL { get; set; } // სულ რამდენი ლარი გადაიხადა
        public int TotalAmountPoints { get; set; }  // სულ რამდენი ქულა დახარჯა (თუ ქულით იყიდა)

        // ქულების ლოგიკა ამ შეკვეთისთვის
        public int PointsEarned { get; set; } // ამ კონკრეტულ შეკვეთაზე რამდენი ქულა დაერიცხა

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentMethod { get; set; } // Card, Cash, ან FishCoins

        // მიწოდების ინფორმაცია
        public string FirstNameAndLastName { get; set; }
        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public decimal DeliveryFee { get; set; }

        // კავშირი პროდუქტებთან
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string UserId { get; set; } // მომხმარებლის ID (IdentityUser-დან)
        public ApplicationUser User { get; set; }
    }

    public enum OrderStatus
    {
        Pending,    // მოლოდინში
        Paid,       // გადახდილი
        Shipped,    // გაგზავნილი
        Delivered,  // ჩაბარებული
        Cancelled   // გაუქმებული
    }

    public enum PaymentMethod
    {
        Card,
        CashOnDelivery,
        FishCoins // თუ მომხმარებელი ქულებით ყიდულობს
    }
}
