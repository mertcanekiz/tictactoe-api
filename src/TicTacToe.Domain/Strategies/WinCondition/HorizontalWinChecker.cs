using System.Linq;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Strategies.WinCondition
{
    public class HorizontalWinChecker : IWinChecker
    {
        public IWinChecker Next { get; set; } = new VerticalWinChecker();

        public ValueObjects.WinCondition Check(Game game)
        {
            var rows = game.Board.GetAllRows();

            foreach (var row in rows)
            {
                if (row.All(x => x.Type == TileType.X))
                {
                    return new ValueObjects.WinCondition
                    {
                        Winner = TileType.X,
                        IsTied = false,
                        IsWon = true,
                        Tiles = row
                    };
                }

                if (row.All(x => x.Type == TileType.O))
                {
                    return new ValueObjects.WinCondition
                    {
                        Winner = TileType.O,
                        IsTied = false,
                        IsWon = true,
                        Tiles = row
                    };
                }
            }

            return Next?.Check(game);
        }
    }
}