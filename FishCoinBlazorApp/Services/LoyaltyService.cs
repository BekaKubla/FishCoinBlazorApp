using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Customer;
using Microsoft.EntityFrameworkCore;
using System;

namespace FishCoinBlazorApp.Services
{
    public class LoyaltyService
    {
        private readonly FishCoinDbContext _context;

        public LoyaltyService(FishCoinDbContext context)
        {
            _context = context;
        }

        // ახალი ბარათის შექმნა იუზერისთვის
        public async Task CreateCardForUserAsync(string userId)
        {
            var newCard = new LoyaltyCard
            {
                UserId = userId,
                CardNumber = GenerateCardNumber(),
                CurrentPoints = 0,
                TotalPointsEarned = 0,
                LastUsedDate = DateTime.Now
            };

            _context.LoyaltyCards.Add(newCard);
            await _context.SaveChangesAsync();
        }

        // უნიკალური ბარათის ნომრის გენერაცია (მაგ: FC-2026-X7A9)
        private string GenerateCardNumber()
        {
            var random = new Random();
            string suffix = random.Next(1000, 9999).ToString();
            return $"FC-{DateTime.Now.Year}-{suffix}";
        }

        // ქულების დამატება/ჩამოჭრა
        public async Task UpdatePointsAsync(string userId, int points)
        {
            var card = await _context.LoyaltyCards.FirstOrDefaultAsync(c => c.UserId == userId);
            if (card != null)
            {
                card.CurrentPoints += points;
                if (points > 0)
                {
                    card.TotalPointsEarned += points;
                }
                card.LastUsedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
