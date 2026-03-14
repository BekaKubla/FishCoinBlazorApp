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
        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync(int? subCategoryId = null)
        {
            var result = await _context.ProductCategories.Include(pc => pc.SubCategory).ToListAsync();
            if (subCategoryId.HasValue)
            {
                result = result.Where(pc => pc.SubCategoryId == subCategoryId.Value).ToList();
            }
            return result;
        }
    }
}
