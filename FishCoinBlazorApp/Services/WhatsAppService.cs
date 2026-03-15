using System.Web;

namespace FishCoinBlazorApp.Services
{
    public class WhatsAppService
    {
        // მნიშვნელოვანია: ნომერი უნდა იყოს მხოლოდ ციფრები, + ნიშნის გარეშე
        private readonly string _myPhoneNumber = "995551082944";

        public string GetProductOrderUrl(string productName, decimal price, string? productNumber, int? quantity)
        {
            // ტექსტი ავაწყოთ ერთ ცვლადად
            string text = $"გამარჯობა, მაინტერესებს ეს პროდუქტი ({productNumber}):\n" +
                          $"დასახელება: {productName}\n" +
                          $"საცალო ფასი: {price} ლარი\n" +
                          $"რაოდენობა: {quantity} ცალი\n" +
                          $"ჯამში: {price * quantity} ლარი\n";

            // გამოიყენე HttpUtility.UrlEncode უფრო საიმედოა დაშორებებისთვის
            string encodedText = HttpUtility.UrlEncode(text);

            // საბოლოო URL
            return $"https://api.whatsapp.com/send?phone={_myPhoneNumber}&text={encodedText}";
        }
    }
}
