using System.Linq;

namespace TicTacToe.Domain.Game
{
    public class Move
    {
        public Board Board { get; set; }
        public int MoveNumber { get; set; }

        public Move DeepClone()
        {
            var clone = (Move)MemberwiseClone();
            clone.Board.Tiles = Board.Tiles.Select(a => a.ToArray()).ToArray();
            return clone;
        }
    }
}