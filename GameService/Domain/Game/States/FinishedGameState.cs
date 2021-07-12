using Core.Exceptions;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain.Game.States
{
    public class FinishedGameState : GameState
    {
        public override string Name => "finished";
        protected override GameState NextState => null;
        protected override PieceType PieceType => PieceType.Empty;

        public override void MakeMove(Game game, int x, int y)
        {
            throw new GameFinishedException();
        }
    }
}