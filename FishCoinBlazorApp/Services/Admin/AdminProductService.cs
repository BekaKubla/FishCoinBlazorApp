using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Services.Models.Admin;

namespace FishCoinBlazorApp.Services.Admin
{
    public class AdminProductService
    {
        private readonly FishCoinDbContext _dbContext;
        public AdminProductService(FishCoinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProduct(ProductModel request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PointsReward = request.PointsReward,
                StockQuantity = request.StockQuantity,
                ProductCategoryId = request.ProductCategoryId,
                ArrivalDate = DateTime.Now,
                CreateDate = DateTime.Now,
                DiscountPrecentage = (int?)request.DiscountPrecentage,
                CostPrice = request.CostPrice,
                IsRedeemable = request.IsRedeemable,
                PointsPrice = request.PointsPrice
                //ImageUrl = request.ImageUrl
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
