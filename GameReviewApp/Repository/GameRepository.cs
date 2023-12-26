using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class GameRepository: IGameRepository
    {
        public readonly DataContext _context;
        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(g => g.Id).ToList();
        }
    }
}
