using System.Linq;

namespace TicTacToe.Domain.Game
{
    public class Board
    {
        public Tile[][] Tiles { get; set; }

        public Board Clone()
        {
            return new Board()
            {
                Tiles = Tiles.Select(x => x.ToArray()).ToArray()
            };
        }
    }

    public enum PieceType
    {
        Empty,
        X,
        O
    }
}