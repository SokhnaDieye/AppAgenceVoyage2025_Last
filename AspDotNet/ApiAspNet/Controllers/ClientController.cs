using ApiAspNet.Models.Clients;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_clientService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_clientService.GetById(id));

        [HttpPost]
        public IActionResult Create(CreateRequestCl model)
        {
            _clientService.Create(model);
            return Ok(new { message = "Client created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestCl model)
        {
            _clientService.Update(id, model);
            return Ok(new { message = "Client updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clientService.Delete(id);
            return Ok(new { message = "Client deleted" });
        }
    }
}
