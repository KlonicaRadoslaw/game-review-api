using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using System.Xml.Linq;

namespace GameReviewApp.Repository
{
    public class GameRepository : IGameRepository
    {
        public readonly DataContext _context;
        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGame(int producerId, int categoryId, Game game)
        {
            var gameProducerEntity = _context.Producers
                .Where(p => p.Id == producerId)
                .FirstOrDefault();
            var category = _context.Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
            var gameProducer = new GameProducer()
            {
                Producer = gameProducerEntity,
                Game = game,
            };

            _context.Add(gameProducer);

            var gameCategory = new GameCategory()
            {
                Category = category,
                Game = game,
            };

            _context.Add(gameCategory);
            _context.Add(game);

            return Save();
        }

        public bool DeleteGame(Game game)
        {
            _context.Remove(game);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGame(int producerId, int categoryId, Game game)
        {
            _context.Update(game);
            return Save();
        }
    }
}
