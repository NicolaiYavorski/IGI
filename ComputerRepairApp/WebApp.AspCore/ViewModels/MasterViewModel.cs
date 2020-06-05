using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.AspCore.ViewModels
{
    public class MasterViewModel
    {
        public int Id { get; set; }

        [DisplayName("First name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        [Required]
        public string Surname { get; set; }

        [DisplayName("Phone")]
        [Required]
        public string Phone { get; set; }

        [DisplayName("Passport id")]
        [Required]
        public string PassportId { get; set; }
    }
}
