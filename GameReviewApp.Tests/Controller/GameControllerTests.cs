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
    }
}
