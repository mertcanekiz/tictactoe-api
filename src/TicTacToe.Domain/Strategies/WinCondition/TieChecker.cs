using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Strategies.WinCondition
{
    public class TieChecker : IWinChecker
    {
        public IWinChecker Next { get; set; } = null;

        public ValueObjects.WinCondition Check(Game game)
        {
            var isBoardFull = game.Board.Tiles.All(x => !x.IsEmpty);
            if (game.WinCondition is {IsWon: true}) return null;
            return isBoardFull ? new ValueObjects.WinCondition
            {
                Winner = null,
                IsTied = true,
                IsWon = false,
                Tiles = new List<Tile>()
            } : null;
        }
    }
}