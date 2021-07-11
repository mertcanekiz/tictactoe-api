#nullable enable
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace TicTacToe.Models
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(HorizontalWinConditionChecker))]
    [BsonKnownTypes(typeof(VerticalWinConditionChecker))]
    [BsonKnownTypes(typeof(DiagonalWinConditionChecker))]
    public abstract class WinConditionChecker
    {
        public static readonly WinConditionChecker Vertical = new VerticalWinConditionChecker();
        public static readonly WinConditionChecker Horizontal = new HorizontalWinConditionChecker();
        public static readonly WinConditionChecker Diagonal = new DiagonalWinConditionChecker();

        private static readonly Dictionary<string, WinConditionChecker> WinConditions = new();

        public abstract string Name { get; set; }

        static WinConditionChecker()
        {
            WinConditions.Add("vertical", Vertical);
            WinConditions.Add("horizontal", Horizontal);
            WinConditions.Add("diagonal", Diagonal);
        }

        public static WinConditionChecker GetWinConditionCheckerByName(string name)
        {
            bool exists = WinConditions.TryGetValue(name, out var winConditionChecker);
            if (!exists)
                throw new Exception($"Invalid win condition checker name: {name}");
            return winConditionChecker;
        }
        public abstract PieceType CheckWinningPieceType(Board board);
    }

    public class VerticalWinConditionChecker : WinConditionChecker
    {
        public override string Name { get; set; } = "vertical";

        public override PieceType CheckWinningPieceType(Board board)
        {
            var winner = PieceType.Empty;
            for (int i = 0; i < board.Tiles[0].Length; i++)
            {
                if (board.Tiles[0][i] == PieceType.X &&
                    board.Tiles[1][i] == PieceType.X &&
                    board.Tiles[2][i] == PieceType.X)
                {
                    winner = PieceType.X;
                    break;
                }

                if (board.Tiles[0][i] == PieceType.O &&
                    board.Tiles[1][i] == PieceType.O &&
                    board.Tiles[2][i] == PieceType.O)
                {
                    winner = PieceType.O;
                    break;
                }
            }

            return winner;
        }
    }

    public class HorizontalWinConditionChecker : WinConditionChecker
    {
        public override string Name { get; set; } = "horizontal";

        public override PieceType CheckWinningPieceType(Board board)
        {
            var winner = PieceType.Empty;
            for (int i = 0; i < board.Tiles.Length; i++)
            {
                if (board.Tiles[i][0] == PieceType.X &&
                    board.Tiles[i][1] == PieceType.X &&
                    board.Tiles[i][2] == PieceType.X)
                {
                    winner = PieceType.X;
                    break;
                }

                if (board.Tiles[0][i] == PieceType.O &&
                    board.Tiles[1][i] == PieceType.O &&
                    board.Tiles[2][i] == PieceType.O)
                {
                    winner = PieceType.O;
                    break;
                }
            }

            return winner;
        }
    }

    public class DiagonalWinConditionChecker : WinConditionChecker
    {
        public override string Name { get; set; } = "diagonal";

        public override PieceType CheckWinningPieceType(Board board)
        {
            var winner = PieceType.Empty;
            for (int i = 0; i < board.Tiles[0].Length; i++)
            {
                if (board.Tiles[i][i] == PieceType.X &&
                    board.Tiles[i][i] == PieceType.X &&
                    board.Tiles[i][i] == PieceType.X ||
                    board.Tiles[i][^(i+1)] == PieceType.X &&
                    board.Tiles[i][^(i+2)] == PieceType.X &&
                    board.Tiles[i][^(i+3)] == PieceType.X)
                {
                    winner = PieceType.X;
                    break;
                }

                if (board.Tiles[i][i] == PieceType.O &&
                    board.Tiles[i][i] == PieceType.O &&
                    board.Tiles[i][i] == PieceType.O ||
                    board.Tiles[^(i+1)][i] == PieceType.O &&
                    board.Tiles[^(i+2)][i] == PieceType.O &&
                    board.Tiles[^(i+3)][i] == PieceType.O)
                {
                    winner = PieceType.O;
                    break;
                }
            }

            return winner;
        }
    }
}