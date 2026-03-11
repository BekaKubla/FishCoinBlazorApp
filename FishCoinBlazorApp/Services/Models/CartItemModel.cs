namespace FishCoinBlazorApp.Services.Models
{
    public class CartItemModel
    {
        public ProductDetailModel Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
