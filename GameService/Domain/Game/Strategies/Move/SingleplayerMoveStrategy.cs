using System.Linq;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain.Game.Strategies.Move
{
    public class SingleplayerMoveStrategy : IMoveStrategy
    {
        public void MakeMove(Game game, int x, int y)
        {
            var previousMove = game.Moves.Last();

            var board = previousMove.Board.Clone();

            if (board.Tiles[y][x].PieceType != PieceType.Empty)
                throw new TileNotEmptyException(x, y);

            board.Tiles[y][x].PieceType = game.PlayerOne.PieceType;

            var newMove = new Domain.Game.Move()
            {
                Board = board,
                MoveNumber = previousMove.MoveNumber + 1
            };
            
            game.Moves.Add(newMove);
            
            game.CheckWinConditions();
        }
    }
}