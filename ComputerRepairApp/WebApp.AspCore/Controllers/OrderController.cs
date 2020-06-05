using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using WebApp.AspCore.ViewModels;

namespace WebApp.AspCore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDto, OrderViewModel>().ReverseMap();
            }).CreateMapper();
        }

        [HttpGet]
        public IActionResult Index() => View(_mapper.Map<List<OrderViewModel>>(_orderService.GetAll()));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var orderDto = _mapper.Map<OrderDto>(model);
            _orderService.Add(orderDto);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int orderId)
        {
            try
            {
                var order = _mapper.Map<OrderViewModel>(_orderService.GetById(orderId));
                //order.Price = order.Price.ToString(CultureInfo.CurrentCulture);
                return View(order);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderDto = _mapper.Map<OrderDto>(model);
                    _orderService.Update(orderDto);
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
        public void Delete(int orderId)
        {
            try
            {
                _orderService.Delete(orderId);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }
    }
}