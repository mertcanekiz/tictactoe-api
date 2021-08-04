using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Strategies.MoveStrategy
{
    public interface IAiMoveStrategy
    {
        void MakeMove(Game game, TileType tileType);
    }
}