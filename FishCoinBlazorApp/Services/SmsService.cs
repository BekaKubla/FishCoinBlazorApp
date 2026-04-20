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
            var message = $"FishCoin: Tqvens {totalAmount}L shenadzenze dagericxat {points} quala! Gadacvalet magaziashi an onlain: FishCoin.Ge";
            var settings = _config.GetSection("SmsSettings");
            string formattedPhone = phoneNumber.StartsWith("+995") ? phoneNumber :
                        (phoneNumber.StartsWith("995") ? "+" + phoneNumber : "+995" + phoneNumber.TrimStart('0'));
            // 1. ვამზადებთ პარამეტრებს
            var url = $"{settings["BaseUrl"]}?" +
          $"username={settings["Username"]}&" +
          $"password={settings["Password"]}&" +
          $"client_id={settings["ClientId"]}&" +
          $"service_id={settings["ServiceId"]}&" +
          $"to={formattedPhone}&" +
          $"text={message}";

            // 2. ვაწყობთ სრულ URL-ს
            //var url = QueryHelpers.AddQueryString(settings["BaseUrl"]!, queryParams);

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
