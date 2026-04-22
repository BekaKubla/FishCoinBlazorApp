using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Entites.Product.Category;
using FishCoinBlazorApp.Services.Models.Admin;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

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

        public async Task<(List<Product> Products, int TotalPages)> GetPagedProductsAsync(int page, int pageSize, string searchTerm)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) ||
                                         p.TagNumber.Contains(searchTerm) ||
                                         p.Description.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var products = await query
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new(products, totalPages);
        }
        public async Task<ProductModel> GetProductById(int productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return null;

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PointsReward = product.PointsReward,
                StockQuantity = product.StockQuantity,
                ProductCategoryId = product.ProductCategoryId,
                DiscountPrecentage = product.DiscountPrecentage,
                CostPrice = product.CostPrice,
                IsRedeemable = product.IsRedeemable,
                PointsPrice = product.PointsPrice,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task UpdateProduct(int id, ProductModel model, IBrowserFile? file)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return;
            var photoPath = product.ImageUrl;
            if (file != null)
            {
                photoPath = await SaveImage(file);
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.PointsReward = model.PointsReward;
            product.StockQuantity = model.StockQuantity;
            product.ProductCategoryId = model.ProductCategoryId;
            product.DiscountPrecentage = (int?)model.DiscountPrecentage;
            product.CostPrice = model.CostPrice;
            product.IsRedeemable = model.IsRedeemable;
            product.PointsPrice = model.PointsPrice;
            product.ImageUrl = photoPath;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            return _dbContext.SubCategories.ToList();
        }

        public async Task<int> GetSubCategoryIdByProductCategory(int id)
        {
            return _dbContext.SubCategories.FirstOrDefault(sc => sc.Id == id).Id;
        }
        public async Task<List<ProductCategory>> GetProductCategoriesBySubCategory(int id)
        {
            return _dbContext.ProductCategories.Where(pc => pc.SubCategoryId == id).ToList();
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
