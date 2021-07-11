namespace TicTacToe.Models
{
    public class Player : Document
    {
        public string Name { get; set; }
        public PieceType Type { get; set; }
    }
}