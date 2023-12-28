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
    public class CountryController: Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper) 
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult getCountryById(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.getCountryById(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("/producers/{producerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfAProducer(int producerId)
        {
            var country = _mapper.Map<CountryDto>(
                _countryRepository.getCountryByProducer(producerId));

            return Ok(country);
        }
    }
}
