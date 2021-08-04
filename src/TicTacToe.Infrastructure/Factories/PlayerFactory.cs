using System;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly ICurrentUserService _currentUserService;

        public PlayerFactory(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public (Player playerOne, Player playerTwo) CreatePlayers(string gameTypeName, string firstPlayerType)
        {
            var gameTypeType = GameType.GetGameTypeByName(gameTypeName).GetType();
            Player playerOne = null;
            Player playerTwo = null;
            var playerOneType = (TileType) Enum.Parse(typeof(TileType), firstPlayerType, true);
            var playerTwoType = playerOneType == TileType.X ? TileType.O : TileType.X;
            
            if (gameTypeType == typeof(SinglePlayerGameType))
            {
                playerOne = new Player
                {
                    Type = playerOneType,
                    Id = _currentUserService.UserId
                };
                playerTwo = new Player
                {
                    Type = playerTwoType,
                    Id = _currentUserService.UserId
                };
            }
            else if (gameTypeType == typeof(AgainstComputerGameType.HardAIGameType) || gameTypeType == typeof(AgainstComputerGameType.RandomAIGameType))
            {
                playerOne = new Player
                {
                    Type = playerOneType,
                    Id = _currentUserService.UserId,
                };
                playerTwo = new Player
                {
                    Type = playerTwoType,
                    Id = null
                };
            }
            else if (gameTypeType == typeof(AgainstHumanGameType))
            {
                playerOne = new Player
                {
                    Type = playerOneType,
                    Id = _currentUserService.UserId,
                };
                playerTwo = null;
            }
            else
            {
                throw new ArgumentException();
            }

            return (playerOne, playerTwo);
        }
    }
}