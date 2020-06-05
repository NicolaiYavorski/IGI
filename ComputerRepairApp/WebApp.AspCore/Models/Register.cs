using System.ComponentModel.DataAnnotations;

namespace WebApp.AspCore.Models
{
    public class Register
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} character long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Different passwords entered")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
