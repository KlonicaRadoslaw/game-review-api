namespace GameReviewApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedAt { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<GameProducer> GameProducers { get; set; }
        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
