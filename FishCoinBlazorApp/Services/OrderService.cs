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

        public async Task<int> PlaceOrderAsync(CheckoutModel model, List<CartItemModel> items, decimal deliveryFee, decimal totalPrice)
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
                // 2. შეკვეთის ნივთები და მარაგის შემცირება
                foreach (var item in items)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        PriceAtPurchase = item.Product.DiscountPrice ?? item.Product.Price
                    };
                    context.OrderItems.Add(orderItem);

                    // მარაგის გამოკლება
                    var product = await context.Products.FindAsync(item.Product.Id);
                    if (product != null)
                    {
                        product.StockQuantity -= item.Quantity;
                    }
                    pointsEarned += item.Quantity * item.Product.PointsReward;
                }
                order.PointsEarned = pointsEarned;
                var currentUser = await context.Users.Include(u => u.LoyaltyCard).FirstOrDefaultAsync(x => x.Id == userId);
                if (currentUser != null)
                {
                    var loyalityCard = currentUser.LoyaltyCard;
                    if (loyalityCard != null)
                    {
                        loyalityCard.CurrentPoints += pointsEarned;
                        loyalityCard.TotalPointsEarned += pointsEarned;
                        context.LoyaltyCards.Update(loyalityCard);
                        await context.SaveChangesAsync();
                    }
                }
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return order.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
