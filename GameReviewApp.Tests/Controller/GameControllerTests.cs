using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using GameReviewApp.Controllers;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewApp.Tests.Controller
{
    public class GameControllerTests
    {
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public GameControllerTests() 
        {
            _gameRepository = A.Fake<IGameRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void GameController_GetGames_ReturnOK()
        {
            //Arrange
            var games = A.Fake<IEnumerable<GameDto>>();
            var gamesList = A.Fake<List<GameDto>>();
            A.CallTo(() => _mapper.Map<List<GameDto>>(games)).Returns(gamesList);
            var controller = new GameController(_gameRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.getGames();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void GameController_GetGameById_ReturnOk()
        {
            //Arrange
            int gameId = 1;
            var game = A.Fake<Game>();
            var gameReturned = A.Fake<GameDto>();
            A.CallTo(() => _gameRepository.GameExists(gameId)).Returns(true);
            A.CallTo(() => _gameRepository.getGameById(gameId))
                .Returns(game);
            var controller = new GameController(_gameRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.getGameById(gameId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void GameController_CreateGame_ReturnOK()
        {
            //Arrange
            int producerId = 1;
            int categoryId = 2;
            var game = A.Fake<Game>();
            var gameCreate = A.Fake<GameDto>();
            var games = A.Fake<ICollection<GameDto>>();
            var gameList = A.Fake<List<GameDto>>();
            A.CallTo(() => _gameRepository.GetGameTrimToUpper(gameCreate)).Returns(game);
            A.CallTo(() => _mapper.Map<Game>(gameCreate)).Returns(game);
            A.CallTo(() => _gameRepository.CreateGame(producerId, categoryId, game)).Returns(true);
            var controller = new GameController(_gameRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.CreateGame(producerId, categoryId, gameCreate);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void GameController_UpdateGame_ReturnNoContent()
        {
            //Arrange
            int gameId = 1;
            int producerId = 1;
            int categoryId = 2;
            var updatedGame = A.Fake<GameDto>();
            var gameToBeUpdated = A.Fake<Game>();
            A.CallTo(() => _gameRepository.GamesHasSameId(gameId,updatedGame.Id)).Returns(true);
            A.CallTo(() => _gameRepository.GameExists(gameId)).Returns(true);
            A.CallTo(() => _mapper.Map<Game>(updatedGame)).Returns(gameToBeUpdated);
            A.CallTo(() => _gameRepository.UpdateGame(producerId,categoryId,gameToBeUpdated)).Returns(true);
            var controller = new GameController(_gameRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.UpdateGame(gameId, producerId, categoryId, updatedGame);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
