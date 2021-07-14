using System;

namespace TicTacToe.Domain.Game.States
{
    public class PlayerTwoState : GameState
    {
        public override string Name => GameState.PlayerTwoStateName;
        public override GameState NextState => new PlayerOneState(Game);
        public override Player Player => Game.PlayerTwo;

        public PlayerTwoState(Game game) : base(game)
        {
        }
    }
}