namespace TicTacToe.Domain.Game.States
{
    public class XPlayerTurnState : GameState
    {
        public override string Name => "x";
        protected override GameState NextState => OPlayerTurnState;
        protected override PieceType PieceType => PieceType.X;
    }
}