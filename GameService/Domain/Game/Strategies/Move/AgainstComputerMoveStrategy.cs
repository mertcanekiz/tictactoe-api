using System.Linq;
using TicTacToe.Domain.Game.Strategies.Move.ComputerStrategies;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain.Game.Strategies.Move
{
    public class AgainstComputerMoveStrategy : IMoveStrategy
    {
        private IComputerStrategy _computerStrategy;

        public AgainstComputerMoveStrategy(IComputerStrategy computerStrategy)
        {
            _computerStrategy = computerStrategy;
        }

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

            if (game.WinCondition != null)
                return;
            
            _computerStrategy.MakeMove(game, game.PlayerTwo.PieceType);

            newMove = new Domain.Game.Move()
            {
                Board = game.Moves.Last().Board.Clone(),
                MoveNumber = previousMove.MoveNumber + 2
            };

            game.Moves.Add(newMove);
            
            game.CheckWinConditions();
        }
    }
}