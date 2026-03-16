using FishCoinBlazorApp.Components.Pages.ECommerce.Orders;
using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static FishCoinBlazorApp.Components.Pages.ECommerce.Orders.Checkout;

namespace FishCoinBlazorApp.Services
{
    public class OrderService
    {
        private readonly IDbContextFactory<FishCoinDbContext> _contextFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(IDbContextFactory<FishCoinDbContext> contextFactory, IHttpContextAccessor httpContextAccessor)
        {
            _contextFactory = contextFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> PlaceOrderAsync(CheckoutModel model, List<CartItemModel> items, decimal deliveryFee, decimal totalPrice)
        {
            using var context = _contextFactory.CreateDbContext();
            using var transaction = await context.Database.BeginTransactionAsync();
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                // 1. შეკვეთის მთავარი ჩანაწერი
                var order = new Order
                {
                    FirstNameAndLastName = model.FullName,
                    PhoneNumber = model.Phone,
                    ShippingAddress = model.Address,
                    PaymentMethod = model.PaymentMethod,
                    TotalAmountGEL = totalPrice,
                    DeliveryFee = deliveryFee,
                    Status = OrderStatus.Pending,
                    UserId = userId
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();
                var pointsEarned = 0;
                // 2. შეკვეთის ნივთები
                foreach (var item in items)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        PriceAtPurchase = item.Product.DiscountPrice ?? item.Product.Price,
                    };
                    context.OrderItems.Add(orderItem);
                    pointsEarned += item.Quantity * item.Product.PointsReward;
                }
                order.PointsEarned = pointsEarned;

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return order.OrderNumber;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task PlaceRedeemOrder(RedeemCheckout.CheckoutModel checkoutModel, ProductDetailModel redeemProduct)
        {
            using var context = _contextFactory.CreateDbContext();
            using var transaction = await context.Database.BeginTransactionAsync();
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await context.Users.Where(x => x.Id == userId).Include(u => u.LoyaltyCard).FirstOrDefaultAsync();
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == redeemProduct.Id && p.IsRedeemable && p.PointsPrice.HasValue);
            if (product == null)
            {
                return;
            }
            try
            {
                if (user == null)
                {
                    return;
                }
                var userLoyaltyCard = user.LoyaltyCard;
                if (userLoyaltyCard == null)
                {
                    return;
                }
                if (userLoyaltyCard.CurrentPoints < product.PointsPrice)
                {
                    return;
                }

                // 1. შეკვეთის მთავარი ჩანაწერი
                var order = new Order
                {
                    FirstNameAndLastName = checkoutModel.FullName,
                    PhoneNumber = checkoutModel.Phone,
                    ShippingAddress = checkoutModel.Address,
                    PaymentMethod = "FishCoins",
                    TotalAmountGEL = 0,
                    DeliveryFee = 0,
                    Status = OrderStatus.Paid,
                    UserId = userId
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    PointsPriceAtPurchase = redeemProduct.PointsPrice!.Value,
                };
                context.OrderItems.Add(orderItem);

                userLoyaltyCard.CurrentPoints -= redeemProduct.PointsPrice.Value;
                product.StockQuantity -= 1;

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Order?> GetOrderByIdAsync(string orderNumber)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }
        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Orders
                .Include(o => o.OrderItems) // აი ეს აუცილებელია რაოდენობისთვის
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
        public async Task<Order?> GetOrderByNumberAsync(string orderNumber)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)// აუცილებელია პროდუქტების რაოდენობისა და სიისთვის
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }
    }
}
