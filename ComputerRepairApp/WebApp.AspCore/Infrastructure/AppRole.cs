using Microsoft.AspNetCore.Identity;

namespace WebApp.AspCore.Infrastructure
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
