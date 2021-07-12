using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Mongo.Repository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.States;
using TicTacToe.Exceptions;
using TicTacToe.Factory;
using TicTacToe.Factory.Board;

namespace TicTacToe.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _repository;
        private readonly ILogger<GameService> _logger;

        public GameService(IRepository<Game> repository, ILogger<GameService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Guid CreateGame(CreateGameRequestDto dto, Guid userId)
        {
            var winConditionCheckers = dto.WinConditionCheckers
                .Select(WinConditionChecker.GetWinConditionCheckerByName).ToList();
            var boardFactory = BoardFactory.GetBoardFactoryByName(dto.BoardFactory);
            var game = new Game()
            {
                CreatedBy = userId,
                Moves = new List<Move>()
                {
                    new()
                    {
                        Board = boardFactory.CreateBoard(),
                        MoveNumber = 0
                    }
                },
                IsWon = false,
                WinConditionCheckers = winConditionCheckers,
                State = GameState.GetGameStateByName(dto.InitialState)
            };
            var createdGameId = _repository.InsertOne(game);
            return createdGameId;
        }

        public Move MakeMove(Guid gameId, int x, int y, PieceType type)
        {
            var game = _repository.FindById(gameId);

            if (game == null)
                throw new Exception($"Game with id {gameId} not found");
            
            game.State.MakeMove(game, x, y);

            _repository.UpdateOne(g => g.Id.Equals(gameId), 
                (g => g.Moves, game.Moves),
                (g => g.Winner, game.Winner),
                (g => g.IsWon, game.IsWon),
                (g => g.State, game.State));

            return game.Moves.Last();
        }
    }
}