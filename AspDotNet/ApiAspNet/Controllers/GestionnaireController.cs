using ApiAspNet.Models.Gestionnaires;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GestionnaireController : ControllerBase
    {
        private IGestionnaireService _service;
        private IMapper _mapper;

        public GestionnaireController(IGestionnaireService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_service.GetById(id));

        [HttpPost]
        public IActionResult Create(CreateRequestG model)
        {
            _service.Create(model);
            return Ok(new { message = "Gestionnaire created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestG model)
        {
            _service.Update(id, model);
            return Ok(new { message = "Gestionnaire updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok(new { message = "Gestionnaire deleted" });
        }
    }

}
