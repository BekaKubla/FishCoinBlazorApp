using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore;

namespace FishCoinBlazorApp.Services
{
    public class ProductCategoryService
    {
        private readonly FishCoinDbContext _context;

        public ProductCategoryService(FishCoinDbContext context)
        {
            _context = context;
        }

        // ყველა პროდუქტის წამოღება
        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync(int? categoryId = null)
        {
            var result = await _context.ProductCategories.ToListAsync();
            if (categoryId.HasValue)
            {
                result = result.Where(pc => pc.CategoryId == categoryId.Value).ToList();
            }
            return result;
        }
    }
}
