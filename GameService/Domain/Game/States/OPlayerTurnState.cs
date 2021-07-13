﻿namespace TicTacToe.Domain.Game.States
{
    public class OPlayerTurnState : GameState
    {
        public override string Name => "o";
        protected override GameState NextState => XPlayerTurnState;
        protected override PieceType PieceType => PieceType.O;
    }
}