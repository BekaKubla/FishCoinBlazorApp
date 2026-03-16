using FishCoinBlazorApp.Entites.Product;
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
            string text = $"*🔍 ინფორმაცია პროდუქტზე*\n\n" +
              $"გამარჯობა, მაინტერესებს მოცემული პროდუქტი:\n\n" +
              $"*🔢 პროდუქტის #:* {productNumber}\n" +
              $"*📦 დასახელება:* {productName}\n" +
              $"*🏷️ საცალო ფასი:* {price} ლარი\n" +
              $"*🔢 რაოდენობა:* {quantity} ცალი\n" +
              $"--------------------------\n" +
              $"*💰 ჯამური ღირებულება:* {price * quantity} ლარი\n\n" +
              $"_გთხოვთ, დამიდასტუროთ არის თუ არა მარაგში._";

            // გამოიყენე HttpUtility.UrlEncode უფრო საიმედოა დაშორებებისთვის
            string encodedText = HttpUtility.UrlEncode(text);

            // საბოლოო URL
            return $"https://api.whatsapp.com/send?phone={_myPhoneNumber}&text={encodedText}";
        }

        public string GetSettlementOrderUrl(Order order)
        {
            // ტექსტი ავაწყოთ ერთ ცვლადად
            string text = $"*💳 შეკვეთის დადასტურება და გადარიცხვა*\n\n" +
              $"გამარჯობა, მსურს ამ შეკვეთის საფასურის *გადარიცხვით* გადახდა:\n\n" +
              $"*📦 შეკვეთის #:* {order.OrderNumber}\n" +
              $"*💰 გადასარიცხი თანხა:* {order.TotalAmountGEL} ლარი\n" +
              $"--------------------------\n\n" +
              $"*👤 მყიდველი:* {order.FirstNameAndLastName}\n" +
              $"*📞 ტელეფონი:* {order.PhoneNumber}\n\n" +
              $"_გთხოვთ გამომიგზავნოთ რეკვიზიტები, რომ მოვახდინო ანგარიშსწორება._";

            string encodedText = HttpUtility.UrlEncode(text);

            // საბოლოო URL
            return $"https://api.whatsapp.com/send?phone={_myPhoneNumber}&text={encodedText}";
        }
    }
}
