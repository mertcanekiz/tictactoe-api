using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Factories.BoardFactory
{
    public interface IBoardFactory
    {
        Board CreateEmpty();
    }
}