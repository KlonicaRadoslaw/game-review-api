using FakeItEasy;
using FluentAssertions;
using GameReviewApp.Data;
using GameReviewApp.Dto;
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

        [Fact]
        public async void GameRepository_GetGameByName_ReturnsNull()
        {
            //Arrange
            var title = "ABCD";
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.getGameByName(title);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void GameRepository_GetGames_ReturnsListGame()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GetGames();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Game>>();
        }

        [Fact]
        public async void GameRepository_GetGameTrimToUpper_ReturnsGame()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var game = A.Fake<GameDto>();
            game.Title = "Pikachu";
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GetGameTrimToUpper(game);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Game>();
        }

        [Fact]
        public async void GameRepository_Save_ReturnFalse()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.Save();

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void GameRepository_UpdateGame_ReturnTrue()
        {
            //Arrange
            int producerId = 1;
            int categoryId = 1;
            var game = A.Fake<Game>();
            game.Title = "Pikachu";
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.UpdateGame(producerId,categoryId,game);

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public async void GameRepository_GetGameById_ReturnsGame()
        {
            //Arrange
            int gameId = 10;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.getGameById(gameId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Game>();
        }

        [Fact]
        public async void GameRepository_GetGameById_ReturnsNull()
        {
            //Arrange
            int gameId = 110;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.getGameById(gameId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void GameRepository_GamesHasSameId_ReturnsTrue()
        {
            //Arrange
            int gameId1 = 10;
            int gameId2 = 10;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GamesHasSameId(gameId1,gameId2);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void GameRepository_GamesHasSameId_ReturnsFalse()
        {
            //Arrange
            int gameId1 = 1;
            int gameId2 = 2;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GamesHasSameId(gameId1, gameId2);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void GameRepository_GameExists_ReturnsTrue()
        {
            //Arrange
            int gameId = 1;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GameExists(gameId);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void GameRepository_GameExists_ReturnsFalse()
        {
            //Arrange
            int gameId = 111;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GameExists(gameId);

            //Assert
            result.Should().BeFalse();
        }
    }
}
