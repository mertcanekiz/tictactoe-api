using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Strategies.MoveStrategy
{
    public interface IMoveStrategy
    {
        void MakeMove(Game game, Position position);
    }
}