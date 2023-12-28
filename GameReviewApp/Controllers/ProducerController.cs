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
        private readonly IMapper _mapper;
        public ProducerController(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
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
    }
}
