using BusinessLogic.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.AspCore.Infrastructure;
using WebApp.AspCore.Interfaces;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<AppUser> GetAll() => _userManager.Users;

        public async Task<AppUser> GetById(string id) => await _userManager.FindByIdAsync(id);

        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.FindValidation();
            await _userManager.DeleteAsync(user);
        }

        public async Task<AppUser> GetByEmail(string email) => await _userManager.FindByEmailAsync(email);

        public async Task Update(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.FindValidation();
            await _userManager.UpdateAsync(user);
        }
    }
}
