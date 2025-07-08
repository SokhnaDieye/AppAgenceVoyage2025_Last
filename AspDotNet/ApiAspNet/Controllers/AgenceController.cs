using ApiAspNet.Models.Agences;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenceController : ControllerBase
    {
        private readonly IAgenceService _agenceService;
        private readonly IMapper _mapper;

        public AgenceController(IAgenceService agenceService, IMapper mapper)
        {
            _agenceService = agenceService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_agenceService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_agenceService.GetById(id));

        [HttpPost]
        public IActionResult Create(CreateRequestA model)
        {
            _agenceService.Create(model);
            return Ok(new { message = "Agence created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestA model)
        {
            _agenceService.Update(id, model);
            return Ok(new { message = "Agence updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agenceService.Delete(id);
            return Ok(new { message = "Agence deleted" });
        }

    }
}
