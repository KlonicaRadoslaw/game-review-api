using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
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

        /*[HttpGet("{gameTitle}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult getGameByName(string gameTitle)
        {
            var game = _gameRepository.getGameByName(gameTitle);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(game);
        }*/
    }
}
