using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Strategies.MoveStrategy
{
    public class AgainstComputerMoveStrategy : IMoveStrategy
    {
        private readonly IAiMoveStrategy _moveStrategy;

        public AgainstComputerMoveStrategy(IAiMoveStrategy moveStrategy)
        {
            _moveStrategy = moveStrategy;
        }

        public void MakeMove(Game game, Position position)
        {
            var board = game.Board;
            if (!board.GetTileAt(position).IsEmpty)
                throw new TileNotEmptyException(position);
            
            board.SetTileAt(position, game.PlayerOne.Type);
            var winChecker = game.GetFirstWinChecker();
            game.WinCondition = winChecker.Check(game);
            if (game.WinCondition != null)
                return;
            game.GameStateName = game.GetGameState().NextStateName;
            
            _moveStrategy.MakeMove(game, game.PlayerTwo.Type);
            game.WinCondition = winChecker.Check(game);
            game.GameStateName = game.GetGameState().NextStateName;
        }
    }
}