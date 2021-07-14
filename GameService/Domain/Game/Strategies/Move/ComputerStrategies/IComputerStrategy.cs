namespace TicTacToe.Domain.Game.Strategies.Move.ComputerStrategies
{
    public interface IComputerStrategy
    {
        void MakeMove(Game game, PieceType pieceType);
    }
}