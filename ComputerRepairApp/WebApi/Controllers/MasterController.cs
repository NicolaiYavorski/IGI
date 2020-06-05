using System;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var masters = _masterService.GetAll();
            return Ok(masters);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var order = _masterService.GetById(id);
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Create([FromBody] MasterDto master)
        {
            try
            {
                _masterService.Add(master);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Edit([FromBody] MasterDto master)
        {
            try
            {
                _masterService.Update(master);
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
                _masterService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}