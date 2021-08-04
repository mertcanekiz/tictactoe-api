using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.States
{
    public class PlayerOneState : GameState
    {
        public PlayerOneState(Game game) : base(game)
        {
        }

        public override string NextStateName => nameof(PlayerTwoState);
        public override Player Player => Game.PlayerOne;
    }
}