using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using System.Xml.Linq;

namespace GameReviewApp.Repository
{
    public class GameRepository: IGameRepository
    {
        public readonly DataContext _context;
        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public bool GameExists(int gameId)
        {
            return _context.Games.Any(g => g.Id == gameId);
        }


        public Game getGameById(int gameId)
        {
            return _context.Games.Where(g => g.Id == gameId).FirstOrDefault();
        }

        public Game getGameByName(string gamename)
        {
            return _context.Games.Where(g => g.Title == gamename).FirstOrDefault();
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(g => g.Id).ToList();
        }
    }
}
