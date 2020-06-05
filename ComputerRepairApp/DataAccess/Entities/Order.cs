using DataAccess.Enums;
using DataAccess.Interfaces;
using System;

namespace DataAccess.Entities
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }

        public Computer Computer { get; set; }

        public string Malfunction { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
