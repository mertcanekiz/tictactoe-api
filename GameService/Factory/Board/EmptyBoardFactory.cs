using TicTacToe.Domain.Game;

namespace TicTacToe.Factory.Board
{
    public class EmptyBoardFactory : BoardFactory
    {
        public override Domain.Game.Board CreateBoard()
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
            return new Domain.Game.Board()
            {
                Tiles = tiles
            };
        }

        public override string Name => "empty";
    }
}