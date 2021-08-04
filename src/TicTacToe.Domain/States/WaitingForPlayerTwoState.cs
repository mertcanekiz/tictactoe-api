using System;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.States
{
    public class WaitingForPlayerTwoState : GameState
    {
        public WaitingForPlayerTwoState(Game game) : base(game)
        {
        }

        public override string NextStateName => nameof(PlayerOneState);

        public override Player Player => throw new Exception();
    }
}