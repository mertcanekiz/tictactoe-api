using TicTacToe.Domain.States;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Application.Common.Interfaces
{
    public interface IGameStateFactory
    {
        string CreateGameState(GameType gameType);
    }
}