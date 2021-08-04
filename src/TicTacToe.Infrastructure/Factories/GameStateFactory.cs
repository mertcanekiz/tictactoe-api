using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.States;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Infrastructure.Factories
{
    public class GameStateFactory : IGameStateFactory
    {
        public string CreateGameState(GameType gameType)
        {
            if (gameType.GetType() == typeof(AgainstComputerGameType.HardAIGameType)
                || gameType.GetType() == typeof(AgainstComputerGameType.RandomAIGameType)
                || gameType.GetType() == typeof(SinglePlayerGameType))
            {
                return nameof(PlayerOneState);
            }

            return nameof(WaitingForPlayerTwoState);
        }
    }
}