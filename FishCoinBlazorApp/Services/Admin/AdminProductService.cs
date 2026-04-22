using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Services.Models.Admin;
using Microsoft.AspNetCore.Components.Forms;

namespace FishCoinBlazorApp.Services.Admin
{
    public class AdminProductService
    {
        private readonly FishCoinDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminProductService(FishCoinDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task CreateProduct(ProductModel request, IBrowserFile? file)
        {
            var photoPath = "";
            if (file != null)
            {
               photoPath = await SaveImage(file);
            }
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
                PointsPrice = request.PointsPrice,
                ImageUrl = photoPath
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        #region Save Image
        public async Task<string?> SaveImage(IBrowserFile? file)
        {
            if (file == null) return null;

            try
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    using (var inputStream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5))
                    {
                        await inputStream.CopyToAsync(fileStream);
                    }
                }

                return $"/images/products/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ფაილის შენახვისას მოხდა შეცდომა: {ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
