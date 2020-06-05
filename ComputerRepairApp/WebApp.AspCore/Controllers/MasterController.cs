using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IMapper _mapper;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterDto, MasterViewModel>().ReverseMap();
            }).CreateMapper();
        }

        [HttpGet]
        public IActionResult Index() => View(_mapper.Map<List<MasterViewModel>>(_masterService.GetAll()));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var masterDto = _mapper.Map<MasterDto>(model);
            _masterService.Add(masterDto);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int masterId)
        {
            try
            {
                var master = _mapper.Map<MasterViewModel>(_masterService.GetById(masterId));
                return View(master);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(MasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var masterDto = _mapper.Map<MasterDto>(model);
                    _masterService.Update(masterDto);
                    return Redirect(nameof(Index));
                }
                catch (ArgumentNullException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public void Delete(int masterId)
        {
            try
            {
                _masterService.Delete(masterId);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }
    }
}