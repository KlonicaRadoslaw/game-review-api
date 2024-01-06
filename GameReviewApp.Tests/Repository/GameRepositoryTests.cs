using FluentAssertions;
using GameReviewApp.Data;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewApp.Tests.Repository
{
    public class GameRepositoryTests
    {
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Games.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Games.Add(
                    new Game()
                    {
                        Title = "Pikachu",
                        PublishedAt = new DateTime(1903, 1, 1),
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
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void GameRepository_GetGameByName_ReturnsGame()
        {
            //Arrange
            var title = "Pikachu";
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.getGameByName(title);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Game>();
        }
    }
}
