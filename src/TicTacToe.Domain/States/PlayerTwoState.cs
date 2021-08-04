using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.States
{
    public class PlayerTwoState : GameState
    {
        public PlayerTwoState(Game game) : base(game)
        {
        }

        public override string NextStateName => nameof(PlayerOneState);
        public override Player Player => Game.PlayerTwo;
    }
}