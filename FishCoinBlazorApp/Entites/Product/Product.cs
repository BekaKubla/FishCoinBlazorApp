using FishCoinBlazorApp.Entites.Product.Category;

namespace FishCoinBlazorApp.Entites.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int PointsReward { get; set; }
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

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

    }
}
