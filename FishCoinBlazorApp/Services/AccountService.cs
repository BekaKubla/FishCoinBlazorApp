using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Customer;
using FishCoinBlazorApp.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FishCoinBlazorApp.Services
{
    public interface IAccountService
    {
        Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(RegisterModel model);
        Task<(bool Succeeded, string Error)> LoginUserAsync(LoginModel model);
        Task<(bool Succeeded, string[] Errors)> PrepareRegistrationAsync(RegisterModel model);
        Task<(bool Succeeded, string[] Errors)> CompleteRegistrationAsync(RegisterModel model,string otpCode);
    }

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly LoyaltyService _loyaltyService;
        private readonly FishCoinDbContext dbContext;
        private readonly AuthService _authService;
        private readonly SmsService _smsService;

        public AccountService(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                LoyaltyService loyaltyService,
                FishCoinDbContext dbContext,
                AuthService authService,
                SmsService smsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loyaltyService = loyaltyService;
            this.dbContext = dbContext;
            _authService = authService;
            _smsService = smsService;
        }

        public async Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(RegisterModel model)
        {
            // 1. ვქმნით იუზერის ობიექტს ახალი მოდელის მიხედვით
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber && string.IsNullOrEmpty(u.PasswordHash));
            if (user != null)
            {
                try
                {
                    user.FirstName = model.FullName;
                    await _userManager.AddPasswordAsync(user, model.Password);
                    return (true, Array.Empty<string>());
                }
                catch
                {
                    return (false, new[] { "შეცდომა მომხარებლის რეგისტრაციისას." });
                }
            }
            user = new ApplicationUser
            {
                UserName = model.PhoneNumber,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FullName,
                LastName = ""
            };

            // 2. ვინახავთ ბაზაში Identity-ს მეშვეობით
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                try
                {
                    // 3. თუ იუზერი შეიქმნა, ავტომატურად ვუქმნით FishCoin ბარათს
                    await _loyaltyService.CreateCardForUserAsync(user.Id);
                    return (true, Array.Empty<string>());
                }
                catch
                {
                    // თუ ბარათის შექმნა დაფეილდა, აქ შეგიძლია ლოგირება ჩაამატო
                    return (false, new[] { "მომხმარებელი შეიქმნა, მაგრამ ლოიალობის ბარათის გენერირება ვერ მოხერხდა." });
                }
            }

            // თუ Identity-მ დააბრუნა ერორები (მაგ: პაროლი სუსტია)
            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string Error)> LoginUserAsync(LoginModel model)
        {
            // ვამოწმებთ მომხმარებლის მონაცემებს
            var result = await _signInManager.PasswordSignInAsync(
                model.PhoneNumber,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return (true, string.Empty);
            }

            if (result.IsLockedOut)
            {
                return (false, "ანგარიში დროებით დაბლოკილია მრავალი არასწორი მცდელობის გამო.");
            }

            return (false, "ელ-ფოსტა ან პაროლი არასწორია.");
        }

        public async Task<(bool Succeeded, string[] Errors)> PrepareRegistrationAsync(RegisterModel model)
        {
            var existingUser = dbContext.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
            if (existingUser != null)
            {
                return (false, new[] { "მომხმარებელი უკვე არსებობს." });
            }
            string otpCode = Random.Shared.Next(1000, 10000).ToString();
            var otpResult = _authService.SaveOtp(model.PhoneNumber, otpCode);
            if (!otpResult)
            {
                return (false, new[] { "შეცდომა კოდის დაგენერირებისას." });
            }
            var sendSms = await _smsService.SendRegistrationOtpSms(model.PhoneNumber, otpCode);
            if (!sendSms)
            {
                return (false, new[] { "შეცდომა კოდის გაგზავნისას." });
            }
            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> CompleteRegistrationAsync(RegisterModel model,string otpCode)
        {
            var verifyOtpResult = _authService.VerifyOtp(model.PhoneNumber, otpCode);
            if (verifyOtpResult)
            {
                var registerResult = await RegisterUserAsync(model);
                if (!registerResult.Succeeded)
                {
                    return (false, registerResult.Errors);
                }
                return (true, Array.Empty<string>());
            }
            return (false, new[] { "სარეგისტრაციო კოდი არასწორია." });
        }
    }
}
