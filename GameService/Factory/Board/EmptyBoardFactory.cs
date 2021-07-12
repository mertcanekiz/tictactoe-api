using TicTacToe.Models.Game;

namespace TicTacToe.Factory.Board
{
    public class EmptyBoardFactory : BoardFactory
    {
        public override Models.Game.Board CreateBoard()
        {
            PieceType[][] tiles = new[]
            {
                new[]
                {
                    PieceType.Empty,
                    PieceType.Empty,
                    PieceType.Empty
                },
                new[]
                {
                    PieceType.Empty,
                    PieceType.Empty,
                    PieceType.Empty
                },
                new[]
                {
                    PieceType.Empty,
                    PieceType.Empty,
                    PieceType.Empty
                },
            };
            return new Models.Game.Board()
            {
                Tiles = tiles
            };
        }

        public override string Name { get; set; } = "empty";
    }
}