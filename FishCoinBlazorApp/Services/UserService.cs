using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Customer;
using FishCoinBlazorApp.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FishCoinBlazorApp.Services
{
    public class UserService
    {
        private readonly FishCoinDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(FishCoinDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // იღებს მიმდინარე იუზერის ID-ს
        private string? GetUserId() =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // ბარათის წამოღება პირდაპირ ბაზიდან
        public async Task<LoyaltyCard?> GetUserLoyaltyCardAsync()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return null;

            return await _context.LoyaltyCards
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        // პროფილის განახლება ბაზაში
        public async Task<bool> UpdateUserProfileAsync(string type, string value)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            if (type == "name")
            {
                user.FirstName = value; // დავუშვათ ბაზაში ასე გქვია ველი
            }
            else if (type == "address")
            {
                user.Address = value;
            }

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var userId = GetUserId(); // წინა მესიჯში დაწერილი დამხმარე მეთოდი
            if (string.IsNullOrEmpty(userId)) return null;

            return await _context.Users.Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserModel?> GetCurrentUserPaidOrders(int productPointPrice)
        {
            var result = new UserModel();
            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return null;
            }
            result.PhoneNumber = currentUser.PhoneNumber;
            result.Address = currentUser.PhoneNumber;
            result.Name = currentUser.FirstName;
            var loyaltyCard = await GetUserLoyaltyCardAsync();
            if (loyaltyCard == null)
            {
                result.CurrentPoints = 0;
            }
            result.CurrentPoints = loyaltyCard!.CurrentPoints;
            var orders = currentUser.Orders.Where(x => x.Status != Entites.Product.OrderStatus.Pending && 
            x.Status != Entites.Product.OrderStatus.Cancelled).ToList();
            if (orders != null && orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    result.TotalAmountGEL += order.TotalAmountGEL;
                }
            }

            return result;
        }
    }
}
