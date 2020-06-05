using Microsoft.AspNetCore.Identity;

namespace WebApp.AspCore.Infrastructure
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }
    }
}
