namespace FishCoinBlazorApp.Entites.Product.Category
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }

        public virtual IEnumerable<ProductCategory>? ProductCategories { get; set; }
    }
}
