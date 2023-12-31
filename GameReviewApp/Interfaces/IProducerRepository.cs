using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IProducerRepository
    {
        ICollection<Producer> GetProducers();
        Producer GetProducerById(int producerid);
        ICollection<Producer> GetProducerOfAGames(int gameId);
        ICollection<Game> GetGameByProducer(int producerid);
        bool ProducerExists(int producerid);
        bool CreateProducer(Producer producer);
        bool UpdateProducer(Producer producer);
        bool DeleteProducer(Producer producer);
        bool Save();
    }
}
