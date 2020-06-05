using BusinessLogic.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.AspCore.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Computer")]
        [Required]
        public Computer Computer { get; set; }

        [DisplayName("Malfunction")]
        [Required]
        public string Malfunction { get; set; }

        [DisplayName("Price")]
        [Required]
        [Range(typeof(decimal), "5,0", "100000,0", ErrorMessage = "The lowest price is 5$, a comma is used a separator of fractional and integer parts")]
        public decimal Price { get; set; }

        [DisplayName("Date time")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset DateTime { get; set; }
    }
}   
