using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController: Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, IMapper mapper) 
        {
            _gameRepository= gameRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Game>))]
        public IActionResult getGames()
        {
            var games = _mapper.Map<List<GameDto>>(_gameRepository.GetGames());
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(games);
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult getGameById(int gameId) 
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var game = _mapper.Map<GameDto>(_gameRepository.getGameById(gameId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(game);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGame([FromQuery] int producerId, [FromQuery] int categoryId, [FromBody] GameDto gameCreate)
        {
            //Aby przekazać odpowiednie parametry, należy stworzyć osobną klasę np. ProducerRequest i tam umieścić odpowienie rzeczy
            if (gameCreate == null)
                return BadRequest(ModelState);

            var games = _gameRepository.GetGames()
                .Where(c => c.Title.Trim().ToUpper() == gameCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (games != null)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gameMap = _mapper.Map<Game>(gameCreate);

            if (!_gameRepository.CreateGame(producerId, categoryId, gameMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }
    }
}
