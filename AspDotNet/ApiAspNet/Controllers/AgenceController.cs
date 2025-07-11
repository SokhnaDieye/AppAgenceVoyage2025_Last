using ApiAspNet.Models.Agences;
using ApiAspNet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;  
using Newtonsoft.Json;  
using MetierAgenceVoyage.Models;  

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenceController : ControllerBase
    {
        private readonly IAgenceService _agenceService;
        private readonly IMapper _mapper;
        private readonly IProducer<string, string> _producer;  
        private const string Topic = "agence-events"; 

        public AgenceController(IAgenceService agenceService, IMapper mapper, IProducer<string, string> producer)  
        {
            _agenceService = agenceService;
            _mapper = mapper;
            _producer = producer;  
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_agenceService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_agenceService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestA model) 
        {
            try
            {
               
                _agenceService.Create(model);

                // Récupérer l'agence créée pour l'envoyer à Kafka
                var agences = _agenceService.GetAll();
                var lastAgence = agences.LastOrDefault(); // Récupère la dernière agence créée

                if (lastAgence != null)
                {
                    // Publier l'événement dans Kafka
                    var kafkaMessage = new Message<string, string>
                    {
                        Value = JsonConvert.SerializeObject(new
                        {
                            EventType = "AgenceCreated",
                            Timestamp = DateTime.UtcNow,
                            Data = lastAgence
                        })
                    };

                    await _producer.ProduceAsync(Topic, kafkaMessage);
                }

                return Ok(new { message = "Agence created successfully" });
            }
            catch (ProduceException<string, string> ex)
            {
                // Erreur Kafka - mais l'agence est quand même créée
                return Ok(new
                {
                    message = "Agence created but Kafka notification failed",
                    kafkaError = ex.Error.Reason
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating agence", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRequestA model)  
        {
            try
            {
                
                _agenceService.Update(id, model);

                // Récupérer l'agence mise à jour
                var updatedAgence = _agenceService.GetById(id);

                if (updatedAgence != null)
                {
                    // Publier l'événement dans Kafka
                    var kafkaMessage = new Message<string, string>
                    {
                        Value = JsonConvert.SerializeObject(new
                        {
                            EventType = "AgenceUpdated",
                            Timestamp = DateTime.UtcNow,
                            Data = updatedAgence
                        })
                    };

                    await _producer.ProduceAsync(Topic, kafkaMessage);
                }

                return Ok(new { message = "Agence updated successfully" });
            }
            catch (ProduceException<string, string> ex)
            {
                return Ok(new
                {
                    message = "Agence updated but Kafka notification failed",
                    kafkaError = ex.Error.Reason
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating agence", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)  
        {
            try
            {
                
                var agenceToDelete = _agenceService.GetById(id);

               
                _agenceService.Delete(id);

                // Publier l'événement dans Kafka
                var kafkaMessage = new Message<string, string>
                {
                    Value = JsonConvert.SerializeObject(new
                    {
                        EventType = "AgenceDeleted",
                        Timestamp = DateTime.UtcNow,
                        Data = new
                        {
                            AgenceId = id,
                            DeletedAgence = agenceToDelete  // Informations de l'agence supprimée
                        }
                    })
                };

                await _producer.ProduceAsync(Topic, kafkaMessage);

                return Ok(new { message = "Agence deleted successfully" });
            }
            catch (ProduceException<string, string> ex)
            {
                return Ok(new
                {
                    message = "Agence deleted but Kafka notification failed",
                    kafkaError = ex.Error.Reason
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting agence", error = ex.Message });
            }
        }

        //  Endpoint Kafka directement
        [HttpPost("test-kafka")]
        public async Task<IActionResult> TestKafka([FromBody] object testData)
        {
            try
            {
                var kafkaMessage = new Message<string, string>
                {
                    Value = JsonConvert.SerializeObject(new
                    {
                        EventType = "TestEvent",
                        Timestamp = DateTime.UtcNow,
                        Data = testData
                    })
                };

                await _producer.ProduceAsync(Topic, kafkaMessage);

                return Ok(new { message = "Test message sent to Kafka successfully" });
            }
            catch (ProduceException<string, string> ex)
            {
                return BadRequest(new { message = "Kafka test failed", error = ex.Error.Reason });
            }
        }
    }
}