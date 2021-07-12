using TicTacToe.Models.Game;

namespace TicTacToe.Factory
{
    public interface IBoardFactory
    {
        Board CreateBoard();
    }
}