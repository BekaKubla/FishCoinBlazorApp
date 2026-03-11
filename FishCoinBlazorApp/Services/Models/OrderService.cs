using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using System;

namespace FishCoinBlazorApp.Services.Models
{
    public class OrderService
    {
        private readonly FishCoinDbContext _context;

        public OrderService(FishCoinDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PlaceOrderAsync(Order order, PaymentMethod paymentMethod)
        {
            // 1. ვიყენებთ ტრანზაქციას, რომ თუ რამე დაფეილდა, ბაზაში ნახევრად შენახული მონაცემები არ დარჩეს
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal totalGEL = 0;
                int totalPoints = 0;

                foreach (var item in order.OrderItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        throw new Exception($"პროდუქტი {product?.Name} არ არის საკმარისი რაოდენობით.");
                    }

                    // ფასების ფიქსაცია გაყიდვის მომენტში
                    item.PriceAtPurchase = product.DiscountPrice;
                    item.PointsPriceAtPurchase = product.PointsPrice;

                    // მარაგების შემცირება
                    product.StockQuantity -= item.Quantity;

                    totalGEL += item.PriceAtPurchase * item.Quantity;
                    totalPoints += item.PointsPriceAtPurchase * item.Quantity;
                }

                order.OrderDate = DateTime.Now;
                order.PaymentMethod = paymentMethod;

                // 2. ქულების ლოგიკა გადახდის მეთოდის მიხედვით
                if (paymentMethod == PaymentMethod.FishCoins)
                {
                    order.TotalAmountPoints = totalPoints;
                    order.TotalAmountGEL = 0;
                    order.PointsEarned = 0; // ქულებით ყიდვისას ახალი ქულა არ ირიცხება

                    // აქ უნდა დაემატოს ლოგიკა მომხმარებლის ბალანსის შესამოწმებლად და ჩამოსაჭრელად
                    // await UpdateUserPoints(order.UserId, -totalPoints);
                }
                else
                {
                    order.TotalAmountGEL = totalGEL;
                    order.TotalAmountPoints = 0;

                    // დარიცხვის ლოგიკა: თანხის 2% გადაყვანილი ქულებში
                    // (Total / 0.10) * 0.05 იგივეა რაც Total * 0.2
                    order.PointsEarned = (int)(totalGEL * 0.2m);

                    // დავუმატოთ ქულები მომხმარებლის ბალანსს
                    // await UpdateUserPoints(order.UserId, order.PointsEarned);
                } 

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return order;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("შეკვეთის გაფორმება ვერ მოხერხდა: " + ex.Message);
            }
        }
    }
}
