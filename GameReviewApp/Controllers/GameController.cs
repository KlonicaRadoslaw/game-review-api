using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController: Controller
    {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository) 
        {
            _gameRepository= gameRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Game>))]
        public IActionResult getGames()
        {
            var games = _gameRepository.GetGames();
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(games);
        }
    }
}
