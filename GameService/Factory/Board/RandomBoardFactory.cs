using System;
using System.Collections.Generic;
using TicTacToe.Models.Game;

namespace TicTacToe.Factory.Board
{
    public class RandomBoardFactory : BoardFactory
    {
        private PieceType GetRandomPieceType()
        {
            var rand = new Random().NextDouble();
            if (rand < 1.0 / 3.0)
                return PieceType.Empty;
            if (rand < 2.0 / 3.0)
                return PieceType.X;
            return PieceType.O;
        }
        public override Models.Game.Board CreateBoard()
        {
            PieceType[][] tiles = new[]
            {
                new[]
                {
                    GetRandomPieceType(),
                    GetRandomPieceType(),
                    GetRandomPieceType()
                },
                new[]
                {
                    GetRandomPieceType(),
                    GetRandomPieceType(),
                    GetRandomPieceType()
                },
                new[]
                {
                    GetRandomPieceType(),
                    GetRandomPieceType(),
                    GetRandomPieceType()
                },
            };
            return new Models.Game.Board()
            {
                Tiles = tiles
            };
        }

        public override string Name { get; set; } = "random";
    }
}