using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Application.Common.Interfaces
{
    public interface IPlayerFactory
    {
        (Player playerOne, Player playerTwo) CreatePlayers(string gameTypeName, string firstPlayerType);
    }
}