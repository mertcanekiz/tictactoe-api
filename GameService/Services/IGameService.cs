using System;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        Game CreateGame(CreateGameRequestDto dto, Guid userId);
        Move MakeMove(Guid gameId, Guid userId, int x, int y);
        void JoinGame(Guid id, Guid userId);
    }
}