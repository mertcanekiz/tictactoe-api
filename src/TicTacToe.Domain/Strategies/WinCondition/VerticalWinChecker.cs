using System.Linq;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Strategies.WinCondition
{
    public class VerticalWinChecker : IWinChecker
    {
        public IWinChecker Next { get; set; } = new DiagonalWinChecker();

        public ValueObjects.WinCondition Check(Game game)
        {
            var cols = game.Board.GetAllColumns();
            
            foreach (var col in cols)
            {
                if (col.All(x => x.Type == TileType.X))
                {
                    return new ValueObjects.WinCondition
                    {
                        Winner = TileType.X,
                        IsTied = false,
                        IsWon = true,
                        Tiles = col
                    };
                }

                if (col.All(x => x.Type == TileType.O))
                {
                    return new ValueObjects.WinCondition
                    {
                        Winner = TileType.O,
                        IsTied = false,
                        IsWon = true,
                        Tiles = col
                    };
                }
            }

            return Next?.Check(game);
        }
    }
}