using System;
using Core.Exceptions;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain.Game.States
{
    public class FinishedGameState : GameState
    {
        public override string Name => FinishedStateName;
        public override GameState NextState => null;
        public override Player Player => null;

        public FinishedGameState(Game game) : base(game)
        {
        }
    }
}