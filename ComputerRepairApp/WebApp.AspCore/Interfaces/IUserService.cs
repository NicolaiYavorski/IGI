using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.AspCore.Infrastructure;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Interfaces
{
    public interface IUserService
    {
        IEnumerable<AppUser> GetAll();

        Task<AppUser> GetById(string id);

        Task Delete(string id);

        Task Update(UserViewModel model);

        Task<AppUser> GetByEmail(string email);
    }
}
