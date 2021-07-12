namespace TicTacToe.Models.Game
{
    public class Board
    {
        public PieceType[][] Tiles { get; set; }
    }

    public class Piece
    {
        public PieceType Type { get; set; }
    }

    public enum PieceType
    {
        Empty,
        X,
        O
    }
}