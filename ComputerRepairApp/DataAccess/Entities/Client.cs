using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Client : IEntity<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
