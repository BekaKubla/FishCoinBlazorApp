using FishCoinBlazorApp.Entites.Product.Category;

namespace FishCoinBlazorApp.Entites.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ArrivalDate { get; set; }
        public string? Description { get; set; }

        // ფასწარმოქმნა
        public decimal CostPrice { get; set; } // თვითღირებულება (რაშიც იყიდე)
        public decimal Price { get; set; }     // გასაყიდი ფასი

        // ქულების სისტემა
        public int PointsReward { get; set; } // რამდენი ქულა დაერიცხება ყიდვისას (მაგ: 5%)

        // ნივთის ფასი ქულებში (რამდენ ქულად შეუძლია მომხმარებელს მისი წაღება)
        // ავტომატური Property, რომელიც აბრუნებს ფასი * 10
        public int PointsPrice => (int)(Price * 10);

        public bool IsRedeemable { get; set; } // შეიძლება თუ არა ქულებით ყიდვა

        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? TagNumber { get; set; }
        public int? DiscountPrecentage { get; set; }

        public decimal DiscountPrice
        {
            get
            {
                if (DiscountPrecentage.HasValue && DiscountPrecentage > 0)
                {
                    return Price - (Price * DiscountPrecentage.Value / 100m);
                }
                return Price;
            }
        }

        // მოგების კალკულაცია (Admins-თვის გამოსაჩენად)
        public decimal NetProfit => DiscountPrice - CostPrice;

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
