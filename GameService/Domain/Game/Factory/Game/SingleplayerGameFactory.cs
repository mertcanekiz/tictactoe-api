using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game.Factory.Board;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;

namespace TicTacToe.Domain.Game.Factory.Game
{
    public class SingleplayerGameFactory : IGameFactory
    {
        public Domain.Game.Game CreateGame(CreateGameRequestDto dto, Guid userId)
        {
            var winConditionCheckers = dto.WinConditionCheckers
                .Select(WinConditionChecker.GetWinConditionCheckerByName).ToList();
            var boardFactory = BoardFactory.GetBoardFactoryByName(dto.BoardFactory);
            var playerOnePieceType = Enum.Parse<PieceType>(dto.PlayerOnePieceType, true);
            var game = new Domain.Game.Game()
            {
                PlayerOne = new Player
                {
                    Id = userId,
                    PieceType = playerOnePieceType
                },
                GameTypeName = dto.GameType,
                Moves = new List<Move>()
                {
                    new()
                    {
                        Board = boardFactory.CreateBoard(),
                        MoveNumber = 0
                    }
                },
                WinConditionCheckers = winConditionCheckers,
                StateName = GameState.PlayerOneStateName
            };
            game.CheckWinConditions();
            return game;
        }
    }
}