using System;

namespace TicTacToe.Domain.Game.WinConditions
{
    public class VerticalWinConditionChecker : WinConditionChecker
    {
        public override string Name => "vertical";

        public override WinCondition Check(Board board)
        {
            var winner = PieceType.Empty;
            for (int i = 0; i < board.Tiles[0].Length; i++)
            {
                if (board.Tiles[0][i].PieceType == PieceType.X &&
                    board.Tiles[1][i].PieceType == PieceType.X &&
                    board.Tiles[2][i].PieceType == PieceType.X)
                {
                    winner = PieceType.X;
                    break;
                }

                if (board.Tiles[0][i].PieceType == PieceType.O &&
                    board.Tiles[1][i].PieceType == PieceType.O &&
                    board.Tiles[2][i].PieceType == PieceType.O)
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