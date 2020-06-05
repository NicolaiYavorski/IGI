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
    [Authorize(Roles = "Admin, User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService)
        {
            _userService = userService;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUser, UserViewModel>().ReverseMap();
            }).CreateMapper();
        }

        public IActionResult Index() => View(_mapper.Map<List<UserViewModel>>(_userService.GetAll()));

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            try
            {
                var result = await _userService.GetById(userId);
                return View(_mapper.Map<UserViewModel>(result));
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            await _userService.Update(model);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userService.Delete(userId);
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