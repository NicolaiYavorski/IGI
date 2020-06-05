using System.ComponentModel.DataAnnotations;

namespace WebApp.AspCore.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }
}
