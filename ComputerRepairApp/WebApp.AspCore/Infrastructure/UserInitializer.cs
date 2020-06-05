using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApp.AspCore.Enums;

namespace WebApp.AspCore.Infrastructure
{
    public static class UserInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            await RolesSeed(roleManager);

            await UsersSeed(userManager);
        }

        private static async Task RolesSeed(RoleManager<AppRole> roleManager)
        {
            var adminRole = Role.Admin.ToString();
            var userRole = Role.User.ToString();

            if (await roleManager.FindByNameAsync(adminRole) is null)
            {
                await roleManager.CreateAsync(new AppRole
                {
                    Name = adminRole,
                    Description = "Role management and database interaction",
                });
            }

            if (await roleManager.FindByNameAsync(userRole) is null)
            {
                await roleManager.CreateAsync(new AppRole
                {
                    Name = userRole,
                    Description = "Order management",
                });
            }
        }

        private static async Task UsersSeed(UserManager<AppUser> userManager)
        {
            var eventEmail = "admin@mail.ru";
            var userEmail = "user@mail.ru";
            var sharedPassword = "Password1_";

            if (await userManager.FindByEmailAsync(eventEmail) == null)
            {
                var user = new AppUser { Email = "admin@mail.ru", UserName = "admin@mail.ru", };
                var result = await userManager.CreateAsync(user, sharedPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Admin.ToString());
                }
            }

            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var user = new AppUser { Email = "user@mail.ru", UserName = "user@mail.ru", };
                var result = await userManager.CreateAsync(user, sharedPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.User.ToString());
                }
            }
        }
    }
}
