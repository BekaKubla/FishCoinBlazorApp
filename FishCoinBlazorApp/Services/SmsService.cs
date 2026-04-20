using FishCoinBlazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FishCoinBlazorApp.Services
{
    public class SmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly FishCoinDbContext _dbContext;

        public SmsService(HttpClient httpClient, IConfiguration config, FishCoinDbContext dbContext)
        {
            _httpClient = httpClient;
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<bool> SendBuySms(string phoneNumber, decimal totalAmount, decimal points)
        {
            var currentUser = await _dbContext.Users.Include(x => x.LoyaltyCard).FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (currentUser == null)
            {
                return false;
            }
            var currentPoints = currentUser.LoyaltyCard.CurrentPoints;
            var redeemProductUrl = "https://tinyurl.com/yr84xuc3";
            var message = $"{totalAmount:f2}L shenadzenze dagericxat {points:f2} qula. Balansia: {currentPoints:f2} qula. " +
                          $"Qulebis gadacvla: {redeemProductUrl}";
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


        public async Task<bool> SendRegistrationOtpSms(string phoneNumber, string code)
        {
            var settings = _config.GetSection("SmsSettings");
            string formattedPhone = phoneNumber.StartsWith("+995") ? phoneNumber :
                        (phoneNumber.StartsWith("995") ? "+" + phoneNumber : "+995" + phoneNumber.TrimStart('0'));
            var message = $"Tqveni saregistracio kodi aris {code}.";
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

        public async Task<bool> SendPasswordResetOtpSms(string phoneNumber, string code)
        {
            var settings = _config.GetSection("SmsSettings");
            string formattedPhone = phoneNumber.StartsWith("+995") ? phoneNumber :
                        (phoneNumber.StartsWith("995") ? "+" + phoneNumber : "+995" + phoneNumber.TrimStart('0'));
            var message = $"Parolis agdgenis kodi: {code}";
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
