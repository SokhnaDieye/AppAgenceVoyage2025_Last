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
        private readonly ILogger<AgenceController> _logger;

        public AgenceController(
            IAgenceService agenceService,
            IMapper mapper,
            ILogger<AgenceController> logger)
        {
            _agenceService = agenceService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var agences = _agenceService.GetAll();
            _logger.LogInformation("Liste de toutes les agences récupérée avec succès.");
            return Ok(agences);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var agence = _agenceService.GetById(id);
            if (agence == null)
            {
                _logger.LogWarning($"Agence avec l'identifiant {id} introuvable.");
                return NotFound(new { message = "Agence introuvable" });
            }

            _logger.LogInformation($"Agence avec l'identifiant {id} récupérée avec succès.");
            return Ok(agence);
        }

        [HttpPost]
        public IActionResult Create(CreateRequestA model)
        {
            _agenceService.Create(model);
            _logger.LogInformation("Nouvelle agence créée avec succès.");
            return Ok(new { message = "Agence créée avec succès" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestA model)
        {
            var existing = _agenceService.GetById(id);
            if (existing == null)
            {
                _logger.LogWarning($"Agence avec l'identifiant {id} introuvable pour la mise à jour.");
                return NotFound(new { message = "Agence introuvable" });
            }

            _agenceService.Update(id, model);
            _logger.LogInformation($"Agence avec l'identifiant {id} mise à jour avec succès.");
            return Ok(new { message = "Agence mise à jour avec succès" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _agenceService.GetById(id);
            if (existing == null)
            {
                _logger.LogWarning($"Agence avec l'identifiant {id} introuvable pour la suppression.");
                return NotFound(new { message = "Agence introuvable" });
            }

            _agenceService.Delete(id);
            _logger.LogInformation($"Agence avec l'identifiant {id} supprimée avec succès.");
            return Ok(new { message = "Agence supprimée avec succès" });
        }
    }
}
