using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController: Controller
    {
        private readonly IProducerRepository _producerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public ProducerController(IProducerRepository producerRepository, 
            ICountryRepository countryRepository, 
            IMapper mapper)
        {
            _producerRepository = producerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
        public IActionResult GetProducers()
        {
            var producers = _mapper.Map<List<ProducerDto>>(_producerRepository.GetProducers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producers);
        }

        [HttpGet("{producerId}")]
        [ProducesResponseType(200, Type = typeof(Producer))]
        [ProducesResponseType(400)]
        public IActionResult getCountryById(int producerId)
        {
            if (!_producerRepository.ProducerExists(producerId))
                return NotFound();

            var producer = _mapper.Map<ProducerDto>(_producerRepository.GetProducerById(producerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producer);
        }

        [HttpGet("{producerId}/game")]
        [ProducesResponseType(200, Type = typeof(Producer))]
        [ProducesResponseType(400)]
        public IActionResult GetGameByProduer(int producerId)
        {
            if (!_producerRepository.ProducerExists(producerId))
                return NotFound();

            var producer = _mapper.Map<List<GameDto>>(_producerRepository.GetGameByProducer(producerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producer);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromQuery] int countryId, [FromBody] ProducerDto producerCreate)
        {
            //Aby przekazać odpowiednie parametry, należy stworzyć osobną klasę np. ProducerRequest i tam umieścić odpowienie rzeczy
            if (producerCreate == null)
                return BadRequest(ModelState);

            var producer = _producerRepository.GetProducers()
                .Where(c => c.LastName.Trim().ToUpper() == producerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (producer != null)
            {
                ModelState.AddModelError("", "Producer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producerMap = _mapper.Map<Producer>(producerCreate);

            producerMap.Country = _countryRepository.getCountryById(countryId);

            if (!_producerRepository.CreateProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }

        [HttpPut("{producerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int producerId, [FromBody] ProducerDto updatedproducer)
        {
            if (updatedproducer == null)
                return BadRequest(ModelState);

            if (producerId != updatedproducer.Id)
                return BadRequest(ModelState);

            if (!_producerRepository.ProducerExists(producerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var producerMap = _mapper.Map<Producer>(updatedproducer);

            if (!_producerRepository.UpdateProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating producer");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
