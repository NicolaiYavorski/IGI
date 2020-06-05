using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var client = _clientService.GetById(id);
            return Ok(client);
        }

        [HttpPost]
        public ActionResult Create([FromBody] ClientDto client)
        {
            try
            {
                _clientService.Add(client);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Edit([FromBody] ClientDto client)
        {
            try
            {
                _clientService.Update(client);
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
                _clientService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}