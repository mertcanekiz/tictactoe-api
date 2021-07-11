using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicTacToe.Models;
using TicTacToe.Models.Request;
using TicTacToe.Repository;

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

        public Guid CreateGame(CreateGameRequestModel model)
        {
            var winConditionCheckers = model.WinConditionCheckers
                .Select(WinConditionChecker.GetWinConditionCheckerByName).ToList();
            _logger.LogInformation(JsonConvert.SerializeObject(winConditionCheckers));
            var game = new Game()
            {
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
            
            // TODO: Deep clone
            var prevState = game.States.LastOrDefault();
            if (prevState == null)
            {
                PieceType[][] tiles = new[]
                {
                    new[]
                    {
                        PieceType.Empty,
                        PieceType.Empty,
                        PieceType.Empty
                    },
                    new[]
                    {
                        PieceType.Empty,
                        PieceType.Empty,
                        PieceType.Empty
                    },
                    new[]
                    {
                        PieceType.Empty,
                        PieceType.Empty,
                        PieceType.Empty
                    },
                };
                prevState = new GameState()
                {
                    Board = new Board()
                    {
                        Tiles = tiles
                    },
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