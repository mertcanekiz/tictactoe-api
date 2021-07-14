using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Exceptions;
using Core.Mongo.Repository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.Factory.Board;
using TicTacToe.Domain.Game.Factory.Game;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;
using TicTacToe.Exceptions;

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

        public Game CreateGame(CreateGameRequestDto dto, Guid userId)
        {
            var gameType = GameType.GetGameTypeByName(dto.GameType);
            var game = gameType.GameFactory.CreateGame(dto, userId);
            var createdGameId = _repository.InsertOne(game);
            game.Id = createdGameId;
            return game;
        }

        public Move MakeMove(Guid gameId, Guid userId, int x, int y)
        {
            var game = _repository.FindById(gameId);

            if (game == null)
                throw new Exception($"Game with id {gameId} not found");

            if (!game.State.Player.Id.Equals(userId))
                throw new UnauthorizedAccessException();

            game.GameType.MoveStrategy.MakeMove(game, x, y);

            _repository.UpdateOne(g => g.Id.Equals(gameId), 
                (g => g.Moves, game.Moves),
                (g => g.WinCondition, game.WinCondition),
                (g => g.StateName, game.StateName));

            return game.Moves.Last();
        }

        public void JoinGame(Guid id, Guid userId)
        {
            var game = _repository.FindById(id);

            if (game == null)
                throw new Exception($"Game with id {id} not found");

            if (game.StateName != GameState.WaitingStateName)
            {
                throw new BadRequestException($"Game {id} is not waiting for players to join");
            }

            if (game.PlayerOne.Id.Equals(userId))
            {
                throw new BadRequestException($"You cannot join a game you created");
            }

            game.PlayerTwo = new Player()
            {
                Id = userId,
                PieceType = game.PlayerOne.PieceType == PieceType.X ? PieceType.O : PieceType.X
            };

            game.StateName = game.State.NextState.Name;

            _repository.UpdateOne(x => x.Id.Equals(id),
                (g => g.PlayerTwo, game.PlayerTwo),
                (g => g.StateName, game.StateName));
        }
    }
}