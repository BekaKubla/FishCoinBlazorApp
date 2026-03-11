using System.ComponentModel.DataAnnotations;

namespace FishCoinBlazorApp.Services.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "სახელი და გვარი აუცილებელია")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "ტელეფონის ნომერი აუცილებელია")]
        [Phone(ErrorMessage = "არასწორი ნომერი")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "პაროლი აუცილებელია")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "პაროლები არ ემთხვევა")]
        public string ConfirmPassword { get; set; }
    }
}
