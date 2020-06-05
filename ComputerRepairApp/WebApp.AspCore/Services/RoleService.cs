using AutoMapper;
using BusinessLogic.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.AspCore.Infrastructure;
using WebApp.AspCore.Interfaces;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;

            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleViewModel, AppRole>()
            .ReverseMap().ForMember(d => d.Id, (options) => options.Ignore())).CreateMapper();
        }

        public async Task<int> Create(RoleViewModel model)
        {
            model.CreateValidation();
            var result = await _roleManager.CreateAsync(_mapper.Map<RoleViewModel, AppRole>(model));
            if (!result.Succeeded)
            {
                throw new ArgumentException("Error creating role");
            }

            var createdRole = await _roleManager.FindByNameAsync(model.Name);
            return createdRole.Id;
        }

        public async Task Update(RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());
            role.FindValidation();
            await _roleManager.UpdateAsync(_mapper.Map<AppRole>(role));
        }

        public async Task Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.FindValidation();
            await _roleManager.DeleteAsync(role);
        }

        public IEnumerable<AppRole> GetAll() => _roleManager.Roles;

        public async Task<AppRole> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.FindValidation();
            return role;
        }

        public async Task<AppRole> GetByName(string name) => await _roleManager.FindByNameAsync(name);
    }
}
