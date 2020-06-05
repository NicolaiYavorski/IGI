using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Master : IEntity<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string PasspordId { get; set; }
    }
}
