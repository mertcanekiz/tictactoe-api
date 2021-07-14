using System.Linq;

namespace TicTacToe.Domain.Game.WinConditions
{
    public class TieConditionChecker : WinConditionChecker
    {
        public override string Name => "tie";

        public override WinCondition Check(Board board)
        {
            var boardFull = board.Tiles.All(x => x.All(y => y.PieceType != PieceType.Empty));
            return new WinCondition()
            {
                IsTied = boardFull,
                IsWon = false
            };
        }
    }
}