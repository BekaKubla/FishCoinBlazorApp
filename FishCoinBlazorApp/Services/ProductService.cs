using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Entites.Product.Category;
using FishCoinBlazorApp.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FishCoinBlazorApp.Services
{
    public class ProductService
    {
        private readonly FishCoinDbContext _context;

        public ProductService(FishCoinDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Product> Products, int TotalCount)> GetPagedProductsAsync(int page, int pageSize, List<int?>? productCategoryIds = null, List<int?>? subCategoryIds = null, string? search = null, int? categoryId = null)
        {
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.ProductCategory.SubCategory.CategoryId == categoryId);
            }

            // თუ ქვეკატეგორიები არჩეულია, ვფილტრავთ ბაზის დონეზე
            if (subCategoryIds != null && subCategoryIds.Any())
            {
                query = query.Where(p => subCategoryIds.Contains(p.ProductCategory.SubCategoryId));
            }

            // თუ კატეგორიები არჩეულია, ვფილტრავთ ბაზის დონეზე
            if (productCategoryIds != null && productCategoryIds.Any())
            {
                query = query.Where(p => productCategoryIds.Contains(p.ProductCategory.Id));
            }

            // ძიების ფილტრი (ყოველ ასოზე აქ შემოვა)
            if (!string.IsNullOrWhiteSpace(search))
            {
                // ეძებს სახელსა და აღწერაში (Case-insensitive მუშაობს SQL-ზე ჩვეულებრივ)
                query = query.Where(p => p.Name.Contains(search) || (p.Description != null && p.Description.Contains(search)));
            }

            // 1. გავიგოთ ჯამური რაოდენობა
            var totalCount = await query.CountAsync();

            // 2. წამოვიღოთ მხოლოდ საჭირო ნაჭერი (Pagination)
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<ProductDetailModel?> GetProductById(int id)
        {
            var product = await _context.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                var resultmodel = new ProductDetailModel()
                {
                    StockQuantity = product.StockQuantity,
                    Description = product.Description,
                    Id = id,
                    ImageUrl = product.ImageUrl,
                    Name = product.Name,
                    PointsReward = product.PointsReward,
                    Price = product.Price,
                    TagNumber = product.TagNumber,
                    IsNew = product.ArrivalDate.AddDays(15) >= DateTime.Now,
                    DiscountPrecentage = product.DiscountPrecentage,
                    DiscountPrice = product.DiscountPrice
                };
                return resultmodel;
            }
            return null;
        }

        public async Task<List<ProductDetailModel>> GetProductByCategory(int id)
        {
            var result = new List<ProductDetailModel>();
            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .ThenInclude(pc => pc.Products)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                var productCategory = product.ProductCategory;
                if (productCategory != null && productCategory.Products != null)
                {
                    foreach (var item in product.ProductCategory.Products.Where(p => p.Id != id).Take(4))
                    {
                        result.Add(new ProductDetailModel
                        {
                            StockQuantity = item.StockQuantity,
                            Description = item.Description,
                            Id = item.Id,
                            ImageUrl = item.ImageUrl,
                            Name = item.Name,
                            PointsReward = item.PointsReward,
                            Price = item.Price,
                            TagNumber = item.TagNumber,
                            IsNew = item.ArrivalDate.AddDays(15) >= DateTime.Now,
                            DiscountPrecentage = item.DiscountPrecentage,
                            DiscountPrice = item.DiscountPrice
                        });
                    }
                }
            }
            return result;
        }
    }
}
