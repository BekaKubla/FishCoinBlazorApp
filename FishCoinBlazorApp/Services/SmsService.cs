using Microsoft.AspNetCore.WebUtilities;

namespace FishCoinBlazorApp.Services
{
    public class SmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SmsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<bool> SendBuySms(string phoneNumber, decimal totalAmount, decimal points)
        {

            var message = $"FishCoin: თქვენს {totalAmount}₾ შენაძენზე დაგერიცხათ {points} ქულა! გადაცვალეთ მაღაზიაში ან ონლაინ: FishCoin.Ge";
            var settings = _config.GetSection("SmsSettings");

            // 1. ვამზადებთ პარამეტრებს
            var queryParams = new Dictionary<string, string?>
        {
            { "username", settings["Username"] },
            { "password", settings["Password"] },
            { "client_id", settings["ClientId"] },
            { "service_id", settings["ServiceId"] },
            { "to", phoneNumber.StartsWith("+") ? phoneNumber : $"+{phoneNumber}" },
            { "text", message }
        };

            // 2. ვაწყობთ სრულ URL-ს
            var url = QueryHelpers.AddQueryString(settings["BaseUrl"]!, queryParams);

            try
            {
                // 3. ვაგზავნით მოთხოვნას
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // აქ შეგიძლია შეამოწმო Msg.ge-ს პასუხი (ხშირად აბრუნებენ "ok" ან ID-ს)
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
