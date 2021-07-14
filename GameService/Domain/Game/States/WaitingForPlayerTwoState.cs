using System;
using Core.Exceptions;

namespace TicTacToe.Domain.Game.States
{
    public class WaitingForPlayerTwoState : GameState
    {
        public override string Name => GameState.WaitingStateName;
        public override GameState NextState => new PlayerOneState(Game);
        public override Player Player => null;

        public WaitingForPlayerTwoState(Game game) : base(game)
        {
        }
    }
}