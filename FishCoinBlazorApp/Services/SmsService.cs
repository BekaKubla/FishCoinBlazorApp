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
            var redeemProductUrl = "https://tinyurl.com/yr84xuc3";
            var message = $"FishCoin: Tqvens {totalAmount}L shenadzenze dagericxat {points} qula! qulebis gadacvla shesadzlebelia magaziashi an onlain: {redeemProductUrl}";
            var settings = _config.GetSection("SmsSettings");
            string formattedPhone = phoneNumber.StartsWith("+995") ? phoneNumber :
                        (phoneNumber.StartsWith("995") ? "+" + phoneNumber : "+995" + phoneNumber.TrimStart('0'));

            var url = $"{settings["BaseUrl"]}?" +
                      $"username={settings["Username"]}&" +
                      $"password={settings["Password"]}&" +
                      $"client_id={settings["ClientId"]}&" +
                      $"service_id={settings["ServiceId"]}&" +
                      $"to={formattedPhone}&" +
                      $"text={message}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
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
