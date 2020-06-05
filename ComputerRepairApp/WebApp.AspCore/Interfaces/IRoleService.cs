using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.AspCore.Infrastructure;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Interfaces
{
    public interface IRoleService
    {
        Task<int> Create(RoleViewModel model);

        Task Update(RoleViewModel model);

        Task Delete(string id);

        IEnumerable<AppRole> GetAll();

        Task<AppRole> GetById(string id);

        Task<AppRole> GetByName(string name);
    }
}
