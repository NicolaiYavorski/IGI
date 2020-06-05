using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDto, ClientViewModel>().ReverseMap();
            }).CreateMapper();
        }

        [HttpGet]
        public IActionResult Index() => View(_mapper.Map<List<ClientViewModel>>(_clientService.GetAll()));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var clientDto = _mapper.Map<ClientDto>(model);
            _clientService.Add(clientDto);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int clientId)
        {
            try
            {
                var client = _mapper.Map<ClientViewModel>(_clientService.GetById(clientId));
                return View(client);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var clientDto = _mapper.Map<ClientDto>(model);
                    _clientService.Update(clientDto);
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
        public void Delete(int clientId)
        {
            try
            {
                _clientService.Delete(clientId);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }
    }
}