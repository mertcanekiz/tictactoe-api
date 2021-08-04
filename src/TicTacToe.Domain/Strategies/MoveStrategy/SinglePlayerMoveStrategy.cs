using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.States;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Strategies.MoveStrategy
{
    public class SinglePlayerMoveStrategy : IMoveStrategy
    {
        public void MakeMove(Game game, Position position)
        {
            var board = game.Board;
            if (!board.GetTileAt(position).IsEmpty)
                throw new TileNotEmptyException(position);
            var gameState = game.GetGameState();
            var currentPlayer = gameState.Player;
            var tileType = currentPlayer.Type;
            
            board.SetTileAt(position, tileType);

            var winChecker = game.GetFirstWinChecker();
            game.WinCondition = winChecker.Check(game);
            game.GameStateName = game.GetGameState().NextStateName;
        }
    }
}