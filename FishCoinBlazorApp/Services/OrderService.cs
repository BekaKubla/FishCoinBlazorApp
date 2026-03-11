using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using FishCoinBlazorApp.Services.Models;
using Microsoft.EntityFrameworkCore;
using static FishCoinBlazorApp.Components.Pages.ECommerce.Orders.Checkout;

namespace FishCoinBlazorApp.Services
{
    public class OrderService
    {
        private readonly IDbContextFactory<FishCoinDbContext> _contextFactory;
        public OrderService(IDbContextFactory<FishCoinDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> PlaceOrderAsync(CheckoutModel model, List<CartItemModel> items, decimal deliveryFee, decimal totalPrice)
        {
            using var context = _contextFactory.CreateDbContext();
            using var transaction = await context.Database.BeginTransactionAsync();

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
                    Status = OrderStatus.Pending
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();

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
