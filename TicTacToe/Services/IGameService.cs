using System;
using TicTacToe.Models;
using TicTacToe.Models.Request;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        Guid CreateGame(CreateGameRequestModel model);
        GameState MakeMove(Guid gameId, int x, int y, PieceType type);
    }
}