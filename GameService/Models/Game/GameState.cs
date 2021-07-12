using System.Linq;

namespace TicTacToe.Models.Game
{
    public class GameState
    {
        public Board Board { get; set; }
        public int MoveNumber { get; set; }

        public GameState DeepClone()
        {
            var clone = (GameState)MemberwiseClone();
            clone.Board.Tiles = Board.Tiles.Select(a => a.ToArray()).ToArray();
            return clone;
        }
    }
}