namespace FishCoinBlazorApp.Entites.Customer
{
    public class LoyaltyCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; } // მაგ: FC-2026-XXXX
        public int CurrentPoints { get; set; } // მიმდინარე FishCoin-ების ბალანსი
        public int TotalPointsEarned { get; set; } // სულ რამდენი დაუგროვებია ისტორიის მანძილზე
        public DateTime LastUsedDate { get; set; }

        // კავშირი იუზერთან
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
