using Microsoft.Extensions.Caching.Memory;

namespace FishCoinBlazorApp.Services
{
    public class AuthService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _otpExpiry = TimeSpan.FromMinutes(2);

        public AuthService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool SaveOtp(string phoneNumber, string code)
        {
            try
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(_otpExpiry)
                    .SetSize(1);

                _cache.Set(phoneNumber, code, cacheOptions);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool VerifyOtp(string phoneNumber, string userCode)
        {
            if (_cache.TryGetValue(phoneNumber, out string? correctCode))
            {
                if (correctCode == userCode)
                {
                    _cache.Remove(phoneNumber);
                    return true;
                }
            }
            return false;
        }
    }
}
