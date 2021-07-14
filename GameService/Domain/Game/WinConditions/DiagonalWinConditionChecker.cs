namespace TicTacToe.Domain.Game.WinConditions
{
    public class DiagonalWinConditionChecker : WinConditionChecker
    {
        public override string Name => "diagonal";

        public override WinCondition Check(Board board)
        {
            var winner = PieceType.Empty;
            if (board.Tiles[0][0].PieceType == PieceType.X &&
                board.Tiles[1][1].PieceType == PieceType.X &&
                board.Tiles[2][2].PieceType == PieceType.X ||
                board.Tiles[0][^1].PieceType == PieceType.X &&
                board.Tiles[1][^2].PieceType == PieceType.X &&
                board.Tiles[2][^3].PieceType == PieceType.X)
            {
                winner = PieceType.X;
            }

            if (board.Tiles[0][0].PieceType == PieceType.O &&
                board.Tiles[1][1].PieceType == PieceType.O &&
                board.Tiles[2][2].PieceType == PieceType.O ||
                board.Tiles[0][^1].PieceType == PieceType.O &&
                board.Tiles[1][^2].PieceType == PieceType.O &&
                board.Tiles[2][^3].PieceType == PieceType.O)
            {
                winner = PieceType.O;
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