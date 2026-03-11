namespace FishCoinBlazorApp.Services.Models
{
    public class ProductDetailModel
    {
        public int Id { get; set; }
        public string Name { get;set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int PointsReward { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? TagNumber { get; set; }
        public bool IsNew { get; set; }

        public int? DiscountPrecentage { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
