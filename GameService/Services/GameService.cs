using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Mongo.Repository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicTacToe.Factory;
using TicTacToe.Models.DTO.Request;
using TicTacToe.Models.Game;

namespace TicTacToe.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _repository;
        private readonly ILogger<GameService> _logger;
        private readonly IBoardFactory _boardFactory;

        public GameService(IRepository<Game> repository, ILogger<GameService> logger, IBoardFactory boardFactory)
        {
            _repository = repository;
            _logger = logger;
            _boardFactory = boardFactory;
        }

        public Guid CreateGame(CreateGameRequestDto dto, Guid userId)
        {
            var winConditionCheckers = dto.WinConditionCheckers
                .Select(WinConditionChecker.GetWinConditionCheckerByName).ToList();
            _logger.LogInformation(JsonConvert.SerializeObject(winConditionCheckers));
            var game = new Game()
            {
                CreatedBy = userId,
                States = new List<GameState>(),
                IsWon = false,
                WinConditionCheckers = winConditionCheckers
            };
            var createdGameId = _repository.InsertOne(game);
            return createdGameId;
        }

        public GameState MakeMove(Guid gameId, int x, int y, PieceType type)
        {
            var game = _repository.FindById(gameId);

            if (game == null)
                throw new Exception($"Game with id {gameId} not found");
            
            var prevState = game.States.LastOrDefault();
            if (prevState == null)
            {
                prevState = new GameState()
                {
                    Board = _boardFactory.CreateBoard(),
                    MoveNumber = 0
                };
            }
            var board = new Board()
            {
                Tiles = prevState.Board.Tiles.Select(a => a.ToArray()).ToArray()
            };
            board.Tiles[y][x] = type;

            var newState = new GameState()
            {
                Board = board,
                MoveNumber = prevState.MoveNumber + 1
            };

            var newStates = new List<GameState>();
            newStates.AddRange(game.States);
            newStates.Add(newState);

            var winner = game.Winner;
            var isWon = game.IsWon;
            foreach (var winChecker in game.WinConditionCheckers)
            {
                _logger.LogInformation($"Checking win condition: {winChecker.Name}");
                var pieceType = winChecker.CheckWinningPieceType(board);
                if (pieceType != PieceType.Empty)
                {
                    _logger.LogInformation($"Piece {pieceType} won due to {winChecker.Name} win condition");
                    isWon = true;
                    winner = pieceType;
                    break;
                }
            }
            
            _repository.UpdateOne(x => x.Id.Equals(gameId), 
                (game => game.States, newStates),
                (game => game.Winner, winner),
                (game => game.IsWon, isWon));

            return newState;
        }
    }
}