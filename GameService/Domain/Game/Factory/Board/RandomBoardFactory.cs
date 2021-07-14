using System;

namespace TicTacToe.Domain.Game.Factory.Board
{
    public class RandomBoardFactory : BoardFactory
    {
        private PieceType GetRandomPieceType()
        {
            var rand = new Random().NextDouble();
            if (rand < 1.0 / 3.0)
                return GetRandomPieceType();
            if (rand < 2.0 / 3.0)
                return PieceType.X;
            return PieceType.O;
        }
        public override Domain.Game.Board CreateBoard()
        {
            Tile[][] tiles = new[]
            {
                new[]
                {
                    new Tile{ Position = new Position { X = 0, Y = 0 }, PieceType = GetRandomPieceType()},
                    new Tile{ Position = new Position { X = 1, Y = 0 }, PieceType = GetRandomPieceType()},
                    new Tile{ Position = new Position { X = 2, Y = 0 }, PieceType = GetRandomPieceType()}
                },
                new[]
                {
                    new Tile{ Position = new Position { X = 0, Y = 1 }, PieceType = GetRandomPieceType()},
                    new Tile{ Position = new Position { X = 1, Y = 1 }, PieceType = GetRandomPieceType()},
                    new Tile{ Position = new Position { X = 2, Y = 1 }, PieceType = GetRandomPieceType()}
                },
                new[]
                {
                    new Tile{ Position = new Position { X = 0, Y = 2 }, PieceType = GetRandomPieceType()},
                    new Tile{ Position = new Position { X = 1, Y = 2 }, PieceType = GetRandomPieceType()},
                    new Tile{ Position = new Position { X = 2, Y = 2 }, PieceType = GetRandomPieceType()}
                }
            };
            return new Domain.Game.Board()
            {
                Tiles = tiles
            };
        }

        public override string Name => "random";
    }
}