using TicTacToe.Models.Game;

namespace TicTacToe.Factory
{
    public class EmptyBoardFactory : IBoardFactory
    {
        public Board CreateBoard()
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
            return new Board()
            {
                Tiles = tiles
            };
        }
    }
}