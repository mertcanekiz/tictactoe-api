namespace TicTacToe.Domain.Game.Factory.Board
{
    public class EmptyBoardFactory : BoardFactory
    {
        public override Domain.Game.Board CreateBoard()
        {
            Tile[][] tiles = new[]
            {
                new[]
                {
                    new Tile{ Position = new Position { X = 0, Y = 0 }, PieceType = PieceType.Empty},
                    new Tile{ Position = new Position { X = 1, Y = 0 }, PieceType = PieceType.Empty},
                    new Tile{ Position = new Position { X = 2, Y = 0 }, PieceType = PieceType.Empty}
                },
                new[]
                {
                    new Tile{ Position = new Position { X = 0, Y = 1 }, PieceType = PieceType.Empty},
                    new Tile{ Position = new Position { X = 1, Y = 1 }, PieceType = PieceType.Empty},
                    new Tile{ Position = new Position { X = 2, Y = 1 }, PieceType = PieceType.Empty}
                },
                new[]
                {
                    new Tile{ Position = new Position { X = 0, Y = 2 }, PieceType = PieceType.Empty},
                    new Tile{ Position = new Position { X = 1, Y = 2 }, PieceType = PieceType.Empty},
                    new Tile{ Position = new Position { X = 2, Y = 2 }, PieceType = PieceType.Empty}
                }
            };
            return new Domain.Game.Board()
            {
                Tiles = tiles
            };
        }

        public override string Name => "empty";
    }
}