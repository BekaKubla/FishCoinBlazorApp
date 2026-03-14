using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore;

namespace FishCoinBlazorApp.Services
{
    public class CategoryService
    {
        private readonly IDbContextFactory<FishCoinDbContext> _factory;
        public CategoryService(IDbContextFactory<FishCoinDbContext> factory)
        {
            _factory = factory;
        }

        // ყველა კატეგორიის წამოღება
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            // ყოველ ჯერზე ვქმნით ახალ კონტექსტს 'using'-ით
            using var context = await _factory.CreateDbContextAsync();
            return await context.Categories
                .Include(c => c.SubCategories!)
                .ThenInclude(sc => sc.ProductCategories)
                .ToListAsync();
        }
    }
}
