using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.AspCore.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [DisplayName("First name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        [Required]
        public string Surname { get; set; }

        [DisplayName("Middle name")]
        [Required]
        public string MiddleName { get; set; }

        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }

        [DisplayName("Phone")]
        [Required]
        public string Phone { get; set; }
    }
}
