using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EfContext
{
    public class ComputerContext : DbContext
    {
        public ComputerContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        public DbSet<Master> Masters { get; set; }
    }
}
