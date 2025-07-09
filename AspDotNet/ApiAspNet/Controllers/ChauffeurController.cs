using ApiAspNet.Models.Chauffeurs;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChauffeurController : ControllerBase
    {
        private IChauffeurService _chauffeurService;
        private IMapper _mapper;

        public ChauffeurController(IChauffeurService chauffeurService, IMapper mapper)
        {
            _chauffeurService = chauffeurService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_chauffeurService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_chauffeurService.GetById(id));

        [HttpPost]
        public IActionResult Create(CreateRequestC model)
        {
            _chauffeurService.Create(model);
            return Ok(new { message = "Chauffeur created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestC model)
        {
            _chauffeurService.Update(id, model);
            return Ok(new { message = "Chauffeur updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _chauffeurService.Delete(id);
            return Ok(new { message = "Chauffeur deleted" });
        }
    }
}
