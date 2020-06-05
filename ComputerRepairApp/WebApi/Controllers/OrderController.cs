using System;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var order = _orderService.GetById(id);
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Create([FromBody] OrderDto order)
        {
            try
            {
                _orderService.Add(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Edit([FromBody] OrderDto order)
        {
            try
            {
                _orderService.Update(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _orderService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}