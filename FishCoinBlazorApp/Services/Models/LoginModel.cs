using System.ComponentModel.DataAnnotations;

namespace FishCoinBlazorApp.Services.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "ტელეფონის ნომერი აუცილებელია")]
        [Phone(ErrorMessage = "არასწორი ნომერი")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "პაროლი აუცილებელია")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
