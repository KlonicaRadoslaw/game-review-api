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
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository,
            IReviewRepository reviewRepository,
            IMapper mapper) 
        {
            _gameRepository= gameRepository;
            _reviewRepository = reviewRepository;
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
        [ProducesResponseType(200,Type=typeof(Game))]
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

        [HttpPut("{gameId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGame(int gameId,
            [FromQuery] int producerId, 
            [FromQuery] int categoryId, 
            [FromBody] GameDto updatedGame)
        {
            if (updatedGame == null)
                return BadRequest(ModelState);

            if (!_gameRepository.GamesHasSameId(gameId, updatedGame.Id))
                return BadRequest(ModelState);

            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var gameMap = _mapper.Map<Game>(updatedGame);

            if (!_gameRepository.UpdateGame(producerId, categoryId, gameMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating game");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{gameId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var reviewsToDelete = _reviewRepository.GetReviewsOfAGame(gameId);
            var gameToDelete = _gameRepository.getGameById(gameId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
                ModelState.AddModelError("", "Something went wrong while deleting reviews");

            if (!_gameRepository.DeleteGame(gameToDelete))
                ModelState.AddModelError("", "Something went wrong while deleting game");

            return NoContent();
        }
    }
}
