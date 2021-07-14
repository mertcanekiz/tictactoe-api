using System;
using TicTacToe.Domain.DTO.Request;

namespace TicTacToe.Domain.Game.Factory.Game
{
    public interface IGameFactory
    {
        Domain.Game.Game CreateGame(CreateGameRequestDto dto, Guid userId);
    }
}