using System.Web;

namespace FishCoinBlazorApp.Services
{
    public class WhatsAppService
    {
        // მნიშვნელოვანია: ნომერი უნდა იყოს მხოლოდ ციფრები, + ნიშნის გარეშე
        private readonly string _myPhoneNumber = "995551082944";

        public string GetProductOrderUrl(string productName, decimal price, string? productNumber)
        {
            // ტექსტი ავაწყოთ ერთ ცვლადად
            string text = $"გამარჯობა, მაინტერესებს ეს პროდუქტი:\n" +
                          $"დასახელება: {productName}\n" +
                          $"ფასი: {price} ლარი\n" +
                          $"პროდუქტის ნომერი: {productNumber}";

            // გამოიყენე HttpUtility.UrlEncode უფრო საიმედოა დაშორებებისთვის
            string encodedText = HttpUtility.UrlEncode(text);

            // საბოლოო URL
            return $"https://api.whatsapp.com/send?phone={_myPhoneNumber}&text={encodedText}";
        }
    }
}
