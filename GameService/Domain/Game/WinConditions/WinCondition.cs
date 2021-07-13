namespace TicTacToe.Domain.Game.WinConditions
{
    public class WinCondition
    {
        public PieceType? Winner { get; set; }
        public bool IsTied { get; set; }
        public bool IsWon { get; set; }
    }
}