using TicTacToe.Domain.Entities;

namespace TicTacToe.Domain.Strategies.WinCondition
{
    public interface IWinChecker
    {
        IWinChecker Next { get; set; }
        ValueObjects.WinCondition Check(Game game);
    }
}