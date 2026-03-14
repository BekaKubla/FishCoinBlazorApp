namespace FishCoinBlazorApp.Entites.Product.Category
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string? ProductCategoryName { get; set; }

        public virtual IEnumerable<Product>? Products { get; set; }
        public int SubCategoryId { get; set; } // ახლა მიბმულია "სპინინგი"-ზე
        public virtual SubCategory SubCategory { get; set; }
    }
}
