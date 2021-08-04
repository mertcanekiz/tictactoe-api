using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Strategies.WinCondition
{
    public class DiagonalWinChecker : IWinChecker
    {
        public IWinChecker Next { get; set; } = new TieChecker();

        public ValueObjects.WinCondition Check(Game game)
        {
            var diagonals = game.Board.GetDiagonals();
            var tileTypes = new List<TileType> {TileType.X, TileType.O};

            var winCondition = (from diagonal in diagonals
                from tileType in tileTypes
                where diagonal.All(x => x.Type == tileType)
                select new ValueObjects.WinCondition
                {
                    Winner = tileType,
                    IsWon = true,
                    IsTied = false,
                    Tiles = diagonal
                }).FirstOrDefault();

            return winCondition ?? Next?.Check(game);
        }
    }
}