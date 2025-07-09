using ApiAspNet.Models.Offres;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OffreController : ControllerBase
    {
        private IOffreService _service;
        private IMapper _mapper;

        public OffreController(IOffreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));

        [HttpPost]
        public IActionResult Create(CreateRequestO model)
        {
            _service.Create(model);
            return Ok(new { message = "Offre created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestO model)
        {
            _service.Update(id, model);
            return Ok(new { message = "Offre updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok(new { message = "Offre deleted" });
        }
    }
}
