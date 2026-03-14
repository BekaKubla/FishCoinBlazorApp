using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product.Category;
using Microsoft.EntityFrameworkCore;

namespace FishCoinBlazorApp.Services
{
    public class SubCategoryService
    {
        private readonly FishCoinDbContext _context;
        public SubCategoryService(FishCoinDbContext context) => _context = context;

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
        {
            return await _context.SubCategories
                .Include(s => s.Category)
                .ToListAsync();
        }
    }
}
