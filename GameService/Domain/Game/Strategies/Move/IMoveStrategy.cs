namespace TicTacToe.Domain.Game.Strategies.Move
{
    public interface IMoveStrategy
    {
        void MakeMove(Game game, int x, int y);
    }
}