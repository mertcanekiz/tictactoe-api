using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Exceptions;
using Core.Mongo.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.Factory.Board;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;

namespace GameService.Tests
{
    public class Tests
    {
        private TicTacToe.Services.GameService _service;
        private Mock<IRepository<Game>> _repositoryMock;
        private readonly Mock<ILogger<TicTacToe.Services.GameService>> _loggerMock = new();
        
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new();
            _service = new TicTacToe.Services.GameService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public void Should_create_new_singleplayer_game()
        {
            var testUserId = Guid.NewGuid();
            const PieceType testUserPieceType = PieceType.X;
            var game = _service.CreateGame(new CreateGameRequestDto()
            {
                BoardFactory = BoardFactory.Empty.Name,
                GameType = GameType.Singleplayer.Name,
                WinConditionCheckers = new List<string>()
                {
                    WinConditionChecker.Vertical.Name,
                    WinConditionChecker.Horizontal.Name,
                    WinConditionChecker.Diagonal.Name,
                    WinConditionChecker.Tie.Name,
                },
                PlayerOnePieceType = testUserPieceType.ToString()
            }, testUserId);
            
            _repositoryMock.Verify(x => x.InsertOne(It.IsAny<Game>()), Times.Once);
            Assert.AreEqual(GameState.PlayerOneStateName, game.State.Name);
            Assert.AreEqual(GameType.Singleplayer.Name, game.GameType.Name);
            Assert.AreEqual(testUserId, game.PlayerOne.Id);
            Assert.AreEqual(testUserPieceType, game.PlayerOne.PieceType);
            Assert.IsNull(game.PlayerTwo);
            Assert.AreEqual(1, game.Moves.Count);
        }
        
        [Test]
        [TestCase("empty")]
        [TestCase("random")]
        public void Should_create_new_againsthuman_game(string boardFactory)
        {
            var testUserId = Guid.NewGuid();
            const PieceType testUserPieceType = PieceType.X;
            var game = _service.CreateGame(new CreateGameRequestDto()
            {
                BoardFactory = boardFactory,
                GameType = GameType.AgainstHuman.Name,
                WinConditionCheckers = new List<string>()
                {
                    WinConditionChecker.Vertical.Name,
                    WinConditionChecker.Horizontal.Name,
                    WinConditionChecker.Diagonal.Name,
                    WinConditionChecker.Tie.Name,
                },
                PlayerOnePieceType = testUserPieceType.ToString()
            }, testUserId);
            
            _repositoryMock.Verify(x => x.InsertOne(It.IsAny<Game>()), Times.Once);
            Assert.AreEqual(GameState.WaitingStateName, game.State.Name);
            Assert.AreEqual(GameType.AgainstHuman.Name, game.GameType.Name);
            Assert.AreEqual(testUserId, game.PlayerOne.Id);
            Assert.AreEqual(testUserPieceType, game.PlayerOne.PieceType);
            Assert.IsNull(game.PlayerTwo);
            Assert.AreEqual(1, game.Moves.Count);
        }

        [Test]
        public void Should_throw_error_when_trying_to_join_own_game()
        {
            var testGameId = Guid.NewGuid();
            var testUser1Id = Guid.NewGuid();
            const PieceType testUserPieceType = PieceType.X;
            
            _repositoryMock.Setup(x => x.InsertOne(It.IsAny<Game>())).Returns(testGameId);
            
            var game = _service.CreateGame(new CreateGameRequestDto()
            {
                BoardFactory = BoardFactory.Empty.Name,
                GameType = GameType.AgainstHuman.Name,
                WinConditionCheckers = new List<string>()
                {
                    WinConditionChecker.Vertical.Name,
                    WinConditionChecker.Horizontal.Name,
                    WinConditionChecker.Diagonal.Name,
                    WinConditionChecker.Tie.Name,
                },
                PlayerOnePieceType = testUserPieceType.ToString()
            }, testUser1Id);
            
            _repositoryMock.Setup(x => x.FindById(testGameId)).Returns(game);
            
            Assert.Throws(typeof(BadRequestException), () =>
            {
                _service.JoinGame(game.Id, testUser1Id);
            });
        }

        [Test]
        public void Should_join_multiplayer_game_as_other_user()
        {
            
        }
    }
}