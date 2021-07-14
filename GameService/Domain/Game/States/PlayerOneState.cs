using System;

namespace TicTacToe.Domain.Game.States
{
    public class PlayerOneState : GameState
    {
        public override string Name => GameState.PlayerOneStateName;
        public override GameState NextState => new PlayerTwoState(Game);
        public override Player Player => Game.PlayerOne;

        public PlayerOneState(Game game) : base(game)
        {
            
        }
    }
}