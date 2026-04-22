using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FishCoinBlazorApp.Services.Admin
{
    public class AdminOrderService
    {
        private readonly FishCoinDbContext _dbContext;
        public AdminOrderService(FishCoinDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<(List<Order> Orders, int TotalPages)> GetPagedOrdersAsync(int page, int pageSize, string searchTerm)
        {
            // 1. ვქმნით ბაზის ქვერის და ვრთავთ OrderItems-ს
            var query = _dbContext.Orders
                .Include(o => o.OrderItems)
                .AsQueryable();

            // 2. ფილტრაცია ძებნის მიხედვით (სახელით ან ორდერის ნომრით)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(o => o.OrderNumber.Contains(searchTerm) ||
                                         o.FirstNameAndLastName.Contains(searchTerm) ||
                                         o.PhoneNumber.Contains(searchTerm));
            }

            // 3. სულ რამდენი ჩანაწერია ფილტრის მერე
            var totalCount = await query.CountAsync();

            // 4. პაგინაცია და მონაცემების წამოღება (ახალი ორდერები ზემოთ)
            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, (int)Math.Ceiling(totalCount / (double)pageSize));

        }
    }
}
