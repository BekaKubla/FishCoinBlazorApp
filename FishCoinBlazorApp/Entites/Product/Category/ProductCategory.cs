namespace FishCoinBlazorApp.Entites.Product.Category
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string? ProductCategoryName { get; set; }

        public virtual IEnumerable<Product>? Products { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
