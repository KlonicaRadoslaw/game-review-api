using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
        Game getGameById(int id);
        Game getGameByName(string name);
        bool GameExists(int id);
        bool CreateGame(int producerId, int categoryId, Game game);
        bool UpdateGame(int producerId, int categoryId, Game game);
        bool DeleteGame(Game game);
        bool Save();
        

    }
}
