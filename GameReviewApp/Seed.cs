using GameReviewApp.Data;
using GameReviewApp.Models;

namespace GameReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.GamesProducers.Any())
            {
                var gameProducers = new List<GameProducer>()
                {
                    new GameProducer()
                    {
                        Game = new Game()
                        {
                            Title = "Pikachu",
                            PublishedAt = new DateTime(2000,1,1),
                            GameCategories = new List<GameCategory>()
                            {
                                new GameCategory { Category = new Category() { Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric",
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks",
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu",
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Producer = new Producer()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new GameProducer()
                    {
                        Game = new Game()
                        {
                            Title = "Squirtle",
                            PublishedAt = new DateTime (2000, 1, 1),
                            GameCategories = new List<GameCategory>()
                            {
                                new GameCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric",
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks",
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle",
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Producer = new Producer()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                                    new GameProducer()
                    {
                        Game = new Game()
                        {
                            Title = "Venasuar",
                            PublishedAt = new DateTime(1903,1,1),
                            GameCategories = new List<GameCategory>()
                            {
                                new GameCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric",
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks",
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar",
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Producer = new Producer()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.GamesProducers.AddRange(gameProducers);
                dataContext.SaveChanges();
            }
        }
    }
}
