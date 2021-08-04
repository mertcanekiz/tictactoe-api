using System.Collections.Generic;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Factories.BoardFactory;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IBoardFactory _boardFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IGameStateFactory _gameStateFactory;

        public GameFactory(IBoardFactory boardFactory, IPlayerFactory playerFactory, IGameStateFactory gameStateFactory)
        {
            _boardFactory = boardFactory;
            _playerFactory = playerFactory;
            _gameStateFactory = gameStateFactory;
        }

        public Game CreateGame(List<string> winCheckers, string gameTypeName, string firstPlayerType)
        {
            var (playerOne, playerTwo) = _playerFactory.CreatePlayers(gameTypeName, firstPlayerType);
            var game = new Game
            {
                Board = _boardFactory.CreateEmpty(),
                GameTypeName = gameTypeName,
                WinCheckerNames = winCheckers,
                GameStateName = _gameStateFactory.CreateGameState(GameType.GetGameTypeByName(gameTypeName)),
                PlayerOne = playerOne,
                PlayerTwo = playerTwo
            };
            return game;
        }
    }
}