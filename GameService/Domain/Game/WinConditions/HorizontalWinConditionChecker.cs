namespace TicTacToe.Domain.Game.WinConditions
{
    public class HorizontalWinConditionChecker : WinConditionChecker
    {
        public override string Name => "horizontal";

        public override WinCondition Check(Board board)
        {
            var winner = PieceType.Empty;
            for (int i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i][0].PieceType == PieceType.X &&
                    board.Tiles[i][1].PieceType == PieceType.X &&
                    board.Tiles[i][2].PieceType == PieceType.X)
                {
                    winner = PieceType.X;
                    break;
                }

                if (board.Tiles[i][0].PieceType == PieceType.O &&
                    board.Tiles[i][1].PieceType == PieceType.O &&
                    board.Tiles[i][2].PieceType == PieceType.O)
                {
                    winner = PieceType.O;
                    break;
                }
            }

            if (winner != PieceType.Empty)
            {
                return new WinCondition()
                {
                    Winner = winner,
                    IsTied = false,
                    IsWon = true
                };
            }

            return new WinCondition()
            {
                IsTied = false,
                IsWon = false
            };
        }
    }
}