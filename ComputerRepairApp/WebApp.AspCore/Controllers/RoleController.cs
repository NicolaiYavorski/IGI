using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.AspCore.Infrastructure;
using WebApp.AspCore.Interfaces;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppRole, RoleViewModel>().ReverseMap();
            }).CreateMapper();
        }

        public IActionResult Index() => View(_mapper.Map<IEnumerable<RoleViewModel>>(_roleService.GetAll()));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _roleService.Create(model);
                    return Redirect(nameof(Index));
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {
            try
            {
                var result = await _roleService.GetById(roleId);
                return View(_mapper.Map<RoleViewModel>(result));
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            await _roleService.Update(model);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string roleId)
        {
            try
            {
                await _roleService.Delete(roleId);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest();
            }
        }
    }
}