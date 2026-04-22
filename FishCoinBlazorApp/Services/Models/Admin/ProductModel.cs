using System.ComponentModel.DataAnnotations;

namespace FishCoinBlazorApp.Services.Models.Admin
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "სახელი აუცილებელია")]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int PointsReward { get; set; }
        public int StockQuantity { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal? DiscountPrecentage { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsRedeemable { get; set; }
        public int? PointsPrice { get; set; }
        public string? ImageUrl { get; set; }
    }
}
