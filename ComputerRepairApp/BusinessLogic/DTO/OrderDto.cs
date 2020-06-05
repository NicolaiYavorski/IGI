using BusinessLogic.Enums;
using System;

namespace BusinessLogic.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }

        public Computer Computer { get; set; }

        public string Malfunction { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
