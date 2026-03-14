namespace FishCoinBlazorApp.Entites.Product.Category
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; } // მიბმულია "სათევზაო"-ზე
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
