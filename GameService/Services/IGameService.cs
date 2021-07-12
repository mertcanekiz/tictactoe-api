using System;
using TicTacToe.Models;
using TicTacToe.Models.DTO.Request;
using TicTacToe.Models.Game;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        Guid CreateGame(CreateGameRequestDto dto, Guid userId);
        GameState MakeMove(Guid gameId, int x, int y, PieceType type);
    }
}