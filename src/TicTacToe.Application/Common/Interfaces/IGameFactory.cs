using System.Collections.Generic;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Common.Interfaces
{
    public interface IGameFactory
    {
        Game CreateGame(List<string> winCheckers, string gameType, string firstPlayerTileType);
    }
}