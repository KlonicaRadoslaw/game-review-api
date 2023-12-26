namespace GameReviewApp.Models
{
    public class GameProducer
    {
        public int GameId { get; set; }
        public int ProducerId { get; set; }
        public Game Game { get; set; }
        public Producer Producer { get; set; }
    }
}
